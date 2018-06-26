using System.Diagnostics.CodeAnalysis;
using Api.Telemetry;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.BootStrapping
{
    /// <summary>
    /// Class Logging.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Logging
    {
        /// <summary>
        /// Adds the application insights telemetry components.
        /// </summary>
        /// <param name="services">The services collection used to configure the Insolvency Service.</param>
        /// <param name="configuration">The configuration settings used in the Insolvency Service.</param>
        public static void AddApplicationInsights(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationInsightsTelemetry(configuration);

            services.AddScoped<ITelemetryClient, TelemetryClientWrapper>();
        }
    }
}
