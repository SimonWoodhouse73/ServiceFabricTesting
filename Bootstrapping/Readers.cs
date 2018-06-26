using System.Diagnostics.CodeAnalysis;
using Callcredit.AspNetCore.RESTful.Services;
using Callcredit.RESTful.Services;
using Callcredit.RESTful.Services.Includes;
using Callcredit.RESTful.Services.Readers;
using Callcredit.RESTful.Services.Tokens;
using Callcredit.RESTful.Standards.Headers;
using Microsoft.Extensions.DependencyInjection;

namespace Api.BootStrapping
{
    /// <summary>
    /// Class Readers.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Readers
    {
        /// <summary>
        /// Configures the internal reader classes for use with Dependency Injection.
        /// </summary>
        /// <param name="services">The services collection used to configure the Insolvency Service.</param>
        public static void AddReaders(this IServiceCollection services)
        {
            services.AddScoped<IClaimsReader, ClaimsReader>();
            services.AddScoped<IQueryStringReader, UriQueryStringReader>();
            services.AddScoped<IRequestHeadersReader, RequestHeadersReader>();
            services.AddScoped<IResponseHeadersReader, ResponseHeadersReader>();
            services.AddScoped<IResponseReader, ResponseReader>();
            services.AddScoped<IRequestReader, RequestReader>();
            services.AddScoped<IIncludeReader, IncludeReader>();
            services.AddScoped<IPageReader, PageReader>();
            services.AddScoped<IAuthorizationHeaderReader, AuthorizationHeaderReader>();
        }
    }
}
