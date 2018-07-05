using System;
using System.Net;
using Callcredit.RESTful.Services.Events;
using Callcredit.RESTful.Services.Headers;
using Callcredit.RESTful.Services.Readers;
using Callcredit.RESTful.Services.Tokens;
using Microsoft.AspNetCore.Mvc.Filters;
using Validation;

namespace Callcredit.AspNetCore.ProblemJson
{
    /// <summary>
    /// The exception filter for problems.
    /// </summary>
    public class ProblemExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        private readonly ProblemLogger problemLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProblemExceptionFilterAttribute"/> class.
        /// </summary>
        /// <param name="restServiceEventSource">The <see cref="IRESTServiceEventSource"/> used to log problems.</param>
        /// <param name="requestReader">The <see cref="IRequestReader"/> used to read HTTP request information.</param>
        /// <param name="headersStringFormatter">
        /// The <see cref="IHeadersStringFormatter"/> used to retrieve the HTTP header key/value pairs and format them as Json.
        /// </param>
        /// <param name="claimsStringFormatter">
        /// The <see cref="IClaimsStringFormatter"/> used to retrieve the claims from the HTTP header and format them as Json.
        /// </param>
        public ProblemExceptionFilterAttribute(
            IRESTServiceEventSource restServiceEventSource,
            IRequestReader requestReader,
            IHeadersStringFormatter headersStringFormatter,
            IClaimsStringFormatter claimsStringFormatter)
        {
            Requires.NotNull(restServiceEventSource, nameof(restServiceEventSource));
            Requires.NotNull(requestReader, nameof(requestReader));
            Requires.NotNull(headersStringFormatter, nameof(headersStringFormatter));
            Requires.NotNull(claimsStringFormatter, nameof(claimsStringFormatter));

            problemLogger = new ProblemLogger(restServiceEventSource, requestReader, headersStringFormatter, claimsStringFormatter);
        }

        /// <summary>
        /// Executes when an exception has occurred and assigns the context result to a <see cref="ProblemActionResult"/> containing the <seealso cref="Problem"/>.
        /// </summary>
        /// <param name="context">The <see cref="ExceptionContext"/> containing the response exception details.</param>
        public void OnException(ExceptionContext context)
        {
            Requires.NotNull(context, nameof(context));

            const HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

            var problem = new Problem(statusCode)
            {
                Title = HttpStatusDescription.Get(statusCode),
                Detail = context.Exception.Message
            };

            problemLogger.LogProblem(problem, context.Exception);

            // Convert the unhandled exception into a problem+json response.
            context.Result = new ProblemActionResult(problem);
        }
    }
}
