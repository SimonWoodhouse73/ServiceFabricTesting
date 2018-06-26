// ***********************************************************************
// Assembly         : Api
// Author           : Team 7
// Created          : 03-02-2018
//
// Last Modified By : Team 7
// Last Modified On : 05-11-2018
// ***********************************************************************
// <copyright file="EventSources.cs" company="Callcredit Information Group.">
//     Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>Defines the Event Sources used in the Insolvency Service</summary>
// ***********************************************************************
using System.Diagnostics.CodeAnalysis;
using Api.Logging;
using Callcredit.AspNetCore.RESTful.Services.Endpoints;
using Callcredit.EntityFramework.Secrets.KeyVault;
using Callcredit.RESTful.DataAssets;
using Callcredit.RESTful.Services.Events;
using Callcredit.RESTful.Services.ServiceFabric;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.BootStrapping
{
    /// <summary>
    /// Class EventSources.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class EventSources
    {
        /// <summary>
        /// The event source key
        /// </summary>
        private const string EventSourceKey = "EventSources";

        /// <summary>
        /// Adds the event sources.
        /// </summary>
        /// <param name="services">The services collection used to configure the Insolvency Service.</param>
        /// <param name="configuration">The configuration settings used in the Insolvency Service.</param>
        public static void AddEventSources(this IServiceCollection services, IConfiguration configuration)
        {
            var eventSources = GetEventSourceConfiguration(configuration);

            RESTServiceEventSource.Initialise(eventSources.RESTServiceEventSource);
            DataAssetEventSource.Initialise(eventSources.DataAssetEventSource);
            PlatformEventSource.Initialise(eventSources.PlatformEventSource);
            DatabaseContextEventSource.Initialise(eventSources.DatabaseContextEventSource);

            var eventSourceForRESTfulServices = RESTServiceEventSource.GetInstance();
            var eventSourceForRESTfulDataAssets = DataAssetEventSource.GetInstance();
            var eventSourceForPlatform = PlatformEventSource.GetInstance();
            var eventSourceForDatabase = DatabaseContextEventSource.GetInstance();
            var endpointConfigurationsConfigSection = configuration.GetSection("EndpointConfigurations");

            services.Configure<RouteEndpointConfigurations>(endpointConfigurationsConfigSection);
            services.AddSingleton<IRESTServiceEventSource>(eventSourceForRESTfulServices);
            services.AddSingleton<IDataAssetEventSource>(eventSourceForRESTfulDataAssets);
            services.AddSingleton<IPlatformEventSource>(eventSourceForPlatform);
            services.AddSingleton<IApiEventSource>(ApiEventSource.Instance);
            services.AddSingleton<IDatabaseContextEventSource>(eventSourceForDatabase);
        }

        /// <summary>
        /// Gets the event source configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns>EventSourceConfiguration.</returns>
        private static EventSourceConfiguration GetEventSourceConfiguration(IConfiguration configuration)
        {
            var eventSourceConfig = configuration.GetSection(EventSourceKey)
                .Get<EventSourceConfiguration>();

            return eventSourceConfig;
        }
    }
}
