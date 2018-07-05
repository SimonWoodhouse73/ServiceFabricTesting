using System.Collections.Generic;
using System.Net.Http;
using Callcredit.RESTful.Services;
using Callcredit.RESTful.Standards.Permitted;
using Validation;

namespace Callcredit.AspNetCore.RESTful.Services
{
    public class QueryStringCreator : IBaseQueryStringCreator
    {
        public string CreateBaseQueryString(IDictionary<string,string> requestQueryStrings, IEnumerable<IBaseQueryStringWriter> queryStringWriters)
        {
            Requires.NotNull(requestQueryStrings, nameof(requestQueryStrings));
            Requires.NotNull(queryStringWriters, nameof(queryStringWriters));

            var baseValues = new Dictionary<string, string>();

            requestQueryStrings.TryGetValue(ValidQueryStrings.ApiVersionKey, out var apiVersion);
            if (!string.IsNullOrWhiteSpace(apiVersion))
            {
                baseValues.Add(ValidQueryStrings.ApiVersionKey, apiVersion);
            }

            foreach (var baseQueryStringItem in queryStringWriters)
            {
                baseQueryStringItem.AddBaseQueryStrings(requestQueryStrings, baseValues);
            }

            return new FormUrlEncodedContent(baseValues).ReadAsStringAsync().Result;
        }
    }
}
