using System.Collections.Generic;
using System.Linq;
using Callcredit.RESTful.Services.Readers;
using Microsoft.AspNetCore.Http;
using Validation;

namespace Callcredit.AspNetCore.RESTful.Services
{
    public class RequestHeadersReader : IRequestHeadersReader
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public RequestHeadersReader(IHttpContextAccessor httpContextAccessor)
        {
            Requires.That(httpContextAccessor != null, nameof(httpContextAccessor), ExceptionMessages.HttpContextAccessorNull);

            this.httpContextAccessor = httpContextAccessor;
        }

        public IDictionary<string, string> GetHeaders()
        {
            var headersDictionary = httpContextAccessor.HttpContext.Request.Headers.ToDictionary(header => header.Key, header => header.Value.ToString());

            return headersDictionary;
        }
    }
}
