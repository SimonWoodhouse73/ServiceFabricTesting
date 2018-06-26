using System.Diagnostics.CodeAnalysis;
using Callcredit.AspNetCore.RESTful.Services.Endpoints;
using Callcredit.RESTful.Services.Endpoints;
using Microsoft.Extensions.DependencyInjection;

namespace Api.BootStrapping
{
    /// <summary>
    /// Class ServiceConfiguration.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ServiceConfiguration
    {
        /// <summary>
        /// Adds the endpoints configuration.
        /// </summary>
        /// <param name="services">The services collection used to configure the Insolvency Service.</param>
        public static void AddEndpointsConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IEndpointConfigurationLoader, EndpointConfigurationLoader>();
            services.AddScoped<IEndpointConfigurationProvider, EndpointConfigurationProvider>();
        }
    }
}
