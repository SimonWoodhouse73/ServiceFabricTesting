using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Callcredit.RESTful.Services.Endpoints;
using Microsoft.Extensions.Options;
using Validation;

namespace Callcredit.AspNetCore.RESTful.Services.Endpoints
{
    public class EndpointConfigurationLoader : IEndpointConfigurationLoader
    {
        private readonly Dictionary<string, EndpointConfiguration> configurations;

        public EndpointConfigurationLoader(IOptions<RouteEndpointConfigurations> routeEndpointConfigurationsOptions)
        {
            Requires.NotNull(routeEndpointConfigurationsOptions, nameof(routeEndpointConfigurationsOptions));
            Requires.NotNull(routeEndpointConfigurationsOptions.Value, "RouteEndpointConfigurations");

            configurations = routeEndpointConfigurationsOptions
                .Value
                .ToDictionary(
                    routeEndpointConfiguration => routeEndpointConfiguration.RouteTemplate,
                    routeEndpointConfiguration => routeEndpointConfiguration.EndpointConfiguration);
        }

        /// <inheritdoc/>
        public IReadOnlyDictionary<string, EndpointConfiguration> Load()
        {
            return new ReadOnlyDictionary<string, EndpointConfiguration>(configurations);
        }
    }
}
