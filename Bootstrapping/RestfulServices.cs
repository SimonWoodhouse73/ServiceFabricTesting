using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Callcredit.AspNetCore.RESTful.Services;
using Callcredit.RESTful.DataAssets;
using Callcredit.RESTful.Services;
using Callcredit.RESTful.Services.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.BootStrapping
{
    /// <summary>
    /// Class RestfulServices.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class RestfulServices
    {
        /// <summary>
        /// Adds the restful services.
        /// </summary>
        /// <param name="services">The services collection used to configure the Insolvency Service.</param>
        /// <param name="configuration">The configuration settings used in the Insolvency Service.</param>
        public static void AddRestfulServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBaseQueryStringCreator, QueryStringCreator>();
            services.AddScoped<IList<IBaseQueryStringWriter>>(provider => new List<IBaseQueryStringWriter>());
            services.AddScoped<IAddressResolver, AddressResolver>();
            services.AddScoped<IPagingLinks, PagingLinks>();
            services.AddScoped<IPageInformationProvider, PageInformationProvider>();
            services.AddScoped<IHeadersStringFormatter, HeadersStringFormatter>();
            services.AddScoped<IClock, SystemClock>();
        }
    }
}
