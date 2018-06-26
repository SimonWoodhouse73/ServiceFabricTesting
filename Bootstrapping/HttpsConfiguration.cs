using System.Diagnostics.CodeAnalysis;
using System.Fabric;
using System.Fabric.Description;
using System.Linq;
using Callcredit.AspNetCore.Kestrel.Https;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.BootStrapping
{
    /// <summary>
    /// Class HttpsConfiguration.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class HttpsConfiguration
    {
        /// <summary>
        /// Adds the HTTPS.
        /// </summary>
        /// <param name="services">The services collection used to configure the Insolvency Service.</param>
        /// <param name="codePackageActivationContext">The code package activation context.</param>
        /// <param name="configuration">The configuration settings used in the Insolvency Service.</param>
        public static void AddHttps(
         this IServiceCollection services,
         ICodePackageActivationContext codePackageActivationContext,
         IConfiguration configuration)
        {
            if (codePackageActivationContext == null)
            {
                return;
            }

            var endpoints = codePackageActivationContext.GetEndpoints();

            if (endpoints.Count == 0)
            {
                return;
            }

            EndpointResourceDescription endpointResourceDescription =
                endpoints.FirstOrDefault(endpoint => endpoint.Protocol == EndpointProtocol.Https);

            if (endpointResourceDescription == null)
            {
                return;
            }

            using (var x509Store = new X509StoreWrapper())
            {
                services.AddHttpsWithCertificate(x509Store, configuration, endpointResourceDescription.Port);
            }
        }
    }
}
