using System.Collections.Generic;
using System.Linq;
using Callcredit.RESTful.Services.Readers;
using Microsoft.AspNetCore.Http;
using Validation;

namespace Callcredit.AspNetCore.RESTful.Services
{
    public class ResponseHeadersReader : IResponseHeadersReader
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ResponseHeadersReader(IHttpContextAccessor httpContextAccessor)
        {
            Requires.Argument(httpContextAccessor != null, nameof(httpContextAccessor), ExceptionMessages.HttpContextAccessorNull);

            this.httpContextAccessor = httpContextAccessor;
        }

        public IDictionary<string, string> GetHeaders()
        {
            var headersDictionary = httpContextAccessor.HttpContext.Response.Headers.ToDictionary(header => header.Key, header => header.Value.ToString());

            return headersDictionary;
        }
    }
}
