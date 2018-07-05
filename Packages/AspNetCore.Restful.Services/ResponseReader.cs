using System.Collections.Generic;
using Callcredit.RESTful.Services.Readers;
using Microsoft.AspNetCore.Http;
using Validation;

namespace Callcredit.AspNetCore.RESTful.Services
{
    public class ResponseReader : IResponseReader
    {
        private readonly IResponseHeadersReader responseHeadersReader;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ResponseReader(IResponseHeadersReader responseHeadersReader, IHttpContextAccessor httpContextAccessor)
        {
            Requires.NotNull(responseHeadersReader, nameof(responseHeadersReader));
            Requires.Argument(httpContextAccessor != null, nameof(httpContextAccessor), ExceptionMessages.HttpContextAccessorNull);

            this.responseHeadersReader = responseHeadersReader;
            this.httpContextAccessor = httpContextAccessor;
        }

        public IDictionary<string, string> GetResponseHeaders()
            => responseHeadersReader.GetHeaders();

        public int GetStatusCode()
            => httpContextAccessor.HttpContext.Response.StatusCode;
    }
}
