using System;
using System.Collections.Generic;
using System.Linq;
using Callcredit.RESTful.Services;
using Microsoft.AspNetCore.WebUtilities;
using Validation;

namespace Callcredit.AspNetCore.RESTful.Services
{
    public class UriQueryStringReader : IQueryStringReader
    {
        public IDictionary<string, string> GetQueryStrings(Uri uri)
        {
            Requires.NotNull(uri, nameof(uri));

            var stringValuesQueries = QueryHelpers.ParseQuery(uri.Query);
            return stringValuesQueries.ToDictionary(valuePair => valuePair.Key.ToLowerInvariant(), valuePair => valuePair.Value.ToString(), StringComparer.OrdinalIgnoreCase);
        }
    }
}
