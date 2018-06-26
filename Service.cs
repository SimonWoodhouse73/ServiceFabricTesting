using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Fabric;
using System.IO;
using Callcredit.RESTful.Services.ServiceFabric;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.ServiceFabric;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Communication.AspNetCore;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace Api
{
    /// <summary>
    /// The FabricRuntime creates an instance of class for each service type instance.
    /// </summary>
    /// <seealso cref="Microsoft.ServiceFabric.Services.Runtime.StatelessService" />
    [ExcludeFromCodeCoverage]
    internal sealed class Service : StatelessService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public Service(StatelessServiceContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Optional override to create listeners (like tcp, http) for service instance.
        /// </summary>
        /// <returns>The collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new[]
            {
                new ServiceInstanceListener(serviceContext =>
                    new KestrelCommunicationListener(serviceContext, "ServiceEndpoint", (url, listener) =>
                    {
                        PlatformEventSource.GetInstance().ServiceMessage(serviceContext, $"Starting WebListener on {url}");

                        return
                            new WebHostBuilder()
                            .UseKestrel(opt => opt.AddServerHeader = false)
                            .ConfigureServices(services =>
                                    services
                                        .AddSingleton(serviceContext.CodePackageActivationContext)
                                        .AddSingleton(serviceContext.ServiceName)
                                        .AddSingleton<ITelemetryInitializer>(provider =>
                                            FabricTelemetryInitializerExtension.CreateFabricTelemetryInitializer(serviceContext)))
                            .UseContentRoot(Directory.GetCurrentDirectory())
                            .UseStartup<Startup>()
                            .UseApplicationInsights()
                            .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.None)
                            .UseUrls(url)
                            .Build();
                    }))
            };
        }
    }
}
