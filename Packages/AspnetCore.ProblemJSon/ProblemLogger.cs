using System;
using System.Collections.Generic;
using System.Net;
using Callcredit.RESTful.Services.Events;
using Callcredit.RESTful.Services.Headers;
using Callcredit.RESTful.Services.Readers;
using Callcredit.RESTful.Services.Tokens;
using Callcredit.RESTful.Standards.Exceptions;

namespace Callcredit.AspNetCore.ProblemJson
{
    internal class ProblemLogger
    {
        private readonly IRESTServiceEventSource eventSource;
        private readonly IRequestReader requestReader;
        private readonly IHeadersStringFormatter headersStringFormatter;
        private readonly IClaimsStringFormatter claimsStringFormatter;

        internal ProblemLogger(
            IRESTServiceEventSource eventSource,
            IRequestReader requestReader,
            IHeadersStringFormatter headersStringFormatter,
            IClaimsStringFormatter claimsStringFormatter)
        {
            this.eventSource = eventSource;
            this.requestReader = requestReader;
            this.headersStringFormatter = headersStringFormatter;
            this.claimsStringFormatter = claimsStringFormatter;
        }

        internal void LogProblem(Problem problem, Exception exception)
        {
            var loggerMethods = GetLoggerMethods(exception);

            if (loggerMethods.ContainsKey(problem.Status))
            {
                loggerMethods[problem.Status].Invoke();
            }
        }

        private Dictionary<HttpStatusCode, Action> GetLoggerMethods(Exception exception)
        {
            var url = requestReader.GetRequestUri().AbsolutePath;
            var headers = headersStringFormatter.GetHeadersAsJsonString();
            var claims = claimsStringFormatter.GetClaimsAsJsonString();
            var method = requestReader.GetRequestMethod();
            var ipAddress = requestReader.GetIpAddress();

            return new Dictionary<HttpStatusCode, Action>
            {
                [HttpStatusCode.BadRequest] = () => eventSource.BadRequest(url, headers, claims),
                [HttpStatusCode.NotFound] = () => eventSource.NotFound(url, headers, claims),
                [HttpStatusCode.Unauthorized] = () => eventSource.Unauthorized(url, ipAddress, headers, claims),
                [HttpStatusCode.Forbidden] = () => eventSource.Forbidden(url, ipAddress, headers, claims),
                [HttpStatusCode.MethodNotAllowed] = () => eventSource.MethodNotAllowed(url, method, headers, claims),
                [HttpStatusCode.InternalServerError] = () => eventSource.InternalServerError(url, exception.Message, headers, claims),
                [HttpStatusCode.RequestEntityTooLarge] = () => LogRequestEntityTooLarge(url, headers, claims, exception),
                [HttpStatusCode.NotAcceptable] = () => eventSource.NotAcceptable(url, exception.Message, headers, claims),
                [HttpStatusCode.UnsupportedMediaType] = () => LogUnsupportedMediaType(url, headers, claims, exception),
            };
        }

        private void LogRequestEntityTooLarge(string url, string headers, string claims, Exception exception)
        {
            if (exception is IncludesTooLargeException includesTooLargeException)
            {
                var inclusionSizeIssue = includesTooLargeException.InclusionSizeIssue;
                var inclusion = inclusionSizeIssue.Inclusion;

                eventSource.RequestEntityTooLarge(url, inclusion, headers, claims);
            }
        }

        private void LogUnsupportedMediaType(string url, string headers, string claims, Exception exception)
        {
            if (exception is HeaderInvalidException headerInvalidException)
            {
                var unsupportedMediaType = headerInvalidException.HeaderValue;
                eventSource.UnsupportedMediaType(url, unsupportedMediaType, headers, claims);
            }
        }
    }
}
