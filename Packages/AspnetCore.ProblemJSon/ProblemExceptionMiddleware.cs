using System;
using System.Net;
using System.Threading.Tasks;
using Callcredit.AspNetCore.Middleware;
using Callcredit.RESTful.Services.Events;
using Callcredit.RESTful.Services.Headers;
using Callcredit.RESTful.Services.Readers;
using Callcredit.RESTful.Services.Tokens;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Validation;

namespace Callcredit.AspNetCore.ProblemJson
{
    /// <summary>
    /// Middleware that handles requests and responses for problems and exceptions.
    /// </summary>
    public class ProblemExceptionMiddleware : IMiddleware
    {
        private readonly IProblemProvider problemProvider;
        private readonly ProblemLogger problemLogger;

        /// <summary>
        /// A delegate pointing to the next middleware object in the pipeline.
        /// </summary>
        public RequestDelegate NextMiddlewareDelegate { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProblemExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next Middleware pipeline operation.</param>
        /// <param name="problemProvider">The <see cref="IProblemProvider"/> used to get problems for exceptions.</param>
        /// <param name="restServiceEventSource">The <see cref="IRESTServiceEventSource"/> used to log problems.</param>
        /// <param name="requestReader">The <see cref="IRequestReader"/> used to read HTTP request information.</param>
        /// <param name="headersStringFormatter">
        /// The <see cref="IHeadersStringFormatter"/> used to retrieve the HTTP header key/value pairs and format them as Json.
        /// </param>
        /// <param name="claimsStringFormatter">
        /// The <see cref="IClaimsStringFormatter"/> used to retrieve the claims from the HTTP header and format them as Json.
        /// </param>
        public ProblemExceptionMiddleware(
            RequestDelegate next,
            IProblemProvider problemProvider,
            IRESTServiceEventSource restServiceEventSource,
            IRequestReader requestReader,
            IHeadersStringFormatter headersStringFormatter,
            IClaimsStringFormatter claimsStringFormatter)
        {
            Requires.NotNull(problemProvider, nameof(problemProvider));
            Requires.NotNull(restServiceEventSource, nameof(restServiceEventSource));
            Requires.NotNull(requestReader, nameof(requestReader));
            Requires.NotNull(headersStringFormatter, nameof(headersStringFormatter));
            Requires.NotNull(claimsStringFormatter, nameof(claimsStringFormatter));

            NextMiddlewareDelegate = next;

            this.problemProvider = problemProvider;

            problemLogger = new ProblemLogger(restServiceEventSource, requestReader, headersStringFormatter, claimsStringFormatter);
        }

        /// <summary>
        /// Handles exceptions thrown in the ASP.NET Core pipeline and writes the ProblemException back.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> injected by ASP.NET Core.</param>
        /// <returns>A <see cref="Task"/> representing the invokers work.</returns>
        /// <remarks>This will only catch exceptions from Middleware which are executed after this.</remarks>
        public async Task Invoke(HttpContext context)
        {
            Problem jsonProblem = null;
            Exception exception = null;

            try
            {
                await NextMiddlewareDelegate(context);
            }
            catch (ProblemException ex)
            {
                exception = ex;
                jsonProblem = ex.ProblemDetails;
            }
            catch (Exception ex)
            {
                exception = ex;
                jsonProblem = problemProvider.ProvideProblem(ex) ??
                    new Problem(HttpStatusCode.InternalServerError) { Detail = ex.Message };
            }

            if (jsonProblem == null)
            {
                return;
            }

            problemLogger.LogProblem(jsonProblem, exception);

            var jObject = JObject.FromObject(jsonProblem);

            context.Response.StatusCode = (int)jsonProblem.Status;
            context.Response.ContentType = Problem.ContentType;

            await context.Response.WriteAsync(jObject.ToString());
        }
    }
}
