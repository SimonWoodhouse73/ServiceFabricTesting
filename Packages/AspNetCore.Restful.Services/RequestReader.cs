using System;
using System.Collections.Generic;
using Callcredit.RESTful.Services;
using Callcredit.RESTful.Services.Readers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Validation;

namespace Callcredit.AspNetCore.RESTful.Services
{
    public class RequestReader : IRequestReader
    {
        private readonly IQueryStringReader queryStringReader;
        private readonly IRequestHeadersReader requestHeadersReader;
        private readonly IActionContextAccessor actionContextAccessor;
        private readonly IHttpContextAccessor httpContextAccessor;

        private HttpRequest HttpRequest => httpContextAccessor.HttpContext.Request;

        public RequestReader(
            IQueryStringReader queryStringReader,
            IRequestHeadersReader requestHeadersReader,
            IActionContextAccessor actionContextAccessor,
            IHttpContextAccessor httpContextAccessor)
        {
            Requires.NotNull(requestHeadersReader, nameof(requestHeadersReader));
            Requires.NotNull(queryStringReader, nameof(queryStringReader));
            Requires.Argument(actionContextAccessor != null, nameof(actionContextAccessor), ExceptionMessages.ActionContextAccessor);
            Requires.Argument(httpContextAccessor != null, nameof(httpContextAccessor), ExceptionMessages.HttpContextAccessorNull);

            this.requestHeadersReader = requestHeadersReader;
            this.queryStringReader = queryStringReader;
            this.actionContextAccessor = actionContextAccessor;
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc/>
        public string GetRouteTemplate()
            => actionContextAccessor
                .ActionContext
                .ActionDescriptor
                .AttributeRouteInfo
                ?.Template;

        /// <inheritdoc/>
        public IDictionary<string, string> GetRequestHeaders()
            => requestHeadersReader.GetHeaders();

        /// <inheritdoc/>
        public string GetRequestMethod()
            => HttpRequest.Method;

        /// <inheritdoc/>
        public Uri GetRequestUri()
        {
            var displayUri = HttpRequest.GetDisplayUrl();

            return new Uri(displayUri);
        }

        /// <inheritdoc/>
        public IDictionary<string, string> GetQueryStrings()
        {
            var requestUri = GetRequestUri();

            var queryStrings = queryStringReader.GetQueryStrings(requestUri);

            return queryStrings;
        }

        /// <inheritdoc/>
        public string GetRouteName()
            => actionContextAccessor
                .ActionContext
                .ActionDescriptor
                .AttributeRouteInfo?
                .Name;

        /// <inheritdoc/>
        public string GetIpAddress()
        {
            const string forwardedForHeader = "X-Forwarded-For";
            var requestHeaders = GetRequestHeaders();

            if (requestHeaders.ContainsKey(forwardedForHeader))
            {
                return requestHeaders[forwardedForHeader];
            }

            if (httpContextAccessor.HttpContext.Connection.RemoteIpAddress == null)
            {
                return string.Empty;
            }

            return httpContextAccessor
                .HttpContext
                .Connection
                .RemoteIpAddress
                .ToString();
        }
    }
}
