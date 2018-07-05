using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Callcredit.AspNetCore.ProblemJson
{
    /// <summary>
    /// The output formatter for problems.
    /// </summary>
    public class ProblemJsonOutputFormatter : IOutputFormatter, IApiResponseTypeMetadataProvider
    {
        private const string UrlTypeAboutBlank = "about:blank";
        private readonly IEnumerable<string> supportedJsonMediaTypes;
        private readonly JsonOutputFormatter jsonFormatter;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProblemJsonOutputFormatter"/> class.
        /// </summary>
        public ProblemJsonOutputFormatter()
        {
            var serializerSettings = JsonSerializerSettingsProvider.CreateSerializerSettings();
            jsonFormatter = new JsonOutputFormatter(serializerSettings, ArrayPool<char>.Create());
            supportedJsonMediaTypes = new[] { Problem.ContentType };
        }

        /// <summary>
        /// Determines whether the result can be written by the <see cref="ProblemJsonOutputFormatter"/>.
        /// </summary>
        /// <param name="context">The <see cref="OutputFormatterCanWriteContext"/> used to determine whether the result can be written.</param>
        /// <returns><see cref="bool"/></returns>
        public bool CanWriteResult(OutputFormatterCanWriteContext context)
            => context.ObjectType == typeof(Problem);

        /// <summary>
        /// Formats the output <see cref="Problem"/> object to a JSON object.
        /// </summary>
        /// <param name="context">The <see cref="OutputFormatter"/>.</param>
        /// <returns>A <see cref="Task"/> representing the Output formatters work.</returns>
        public Task WriteAsync(OutputFormatterWriteContext context)
        {
            var problem = context.Object as Problem;
            if (problem == null)
            {
                return jsonFormatter.WriteAsync(context);
            }

            problem.Title = GetTitle(problem.Type, problem.Status, problem.Title);
            var jObject = JObject.FromObject(problem, new JsonSerializer { NullValueHandling = NullValueHandling.Ignore });

            if (problem.Extensions != null)
            {
                AppendExtensions(problem, jObject);
            }

            var outputFormatter =
                new OutputFormatterWriteContext(context.HttpContext, context.WriterFactory, jObject.GetType(), jObject);

            outputFormatter.ContentType = new StringSegment(Problem.ContentType);

            return jsonFormatter.WriteAsync(outputFormatter);
        }

        /// <summary>
        /// Retrieves supported JSON content types.
        /// </summary>
        /// <param name="contentType">The content type used in retrieval.</param>
        /// <param name="objectType">The type of object used in retrieval.</param>
        /// <returns>An IReadOnlyList of <see cref="string"/></returns>
        public IReadOnlyList<string> GetSupportedContentTypes(string contentType, Type objectType)
        {
            var jsonTypes = jsonFormatter.GetSupportedContentTypes(contentType, objectType);

            if (contentType != null)
            {
                return jsonTypes;
            }

            var allTypes = supportedJsonMediaTypes.ToList();
            allTypes.AddRange(jsonTypes);
            return allTypes;
        }

        private static string GetTitle(Uri problemType, HttpStatusCode httpStatusCode, string title)
        {
            if (IfUriIsValid(problemType))
            {
                return string.IsNullOrWhiteSpace(title) ? HttpStatusDescription.Get(httpStatusCode) : title;
            }

            return title;
        }

        private static void AppendExtensions(Problem problem, JObject problemJObject)
        {
            foreach (var extension in GetValidExtensions(problem))
            {
                problemJObject.Add(extension.Key, JToken.FromObject(extension.Value));
            }
        }

        private static bool IfUriIsValid(Uri problemType)
            => problemType == null || problemType.AbsoluteUri == UrlTypeAboutBlank;

        private static IEnumerable<KeyValuePair<string, object>> GetValidExtensions(Problem problem)
            => problem.Extensions.Where(extension => !string.IsNullOrWhiteSpace(extension.Value?.ToString()));
    }
}
