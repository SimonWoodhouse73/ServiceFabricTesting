using System.Collections.Generic;
using Callcredit.RESTful.Services.Endpoints;
using Validation;

namespace Callcredit.AspNetCore.RESTful.Services.Endpoints
{
    public class EndpointConfigurationProvider : IEndpointConfigurationProvider
    {
        private readonly IReadOnlyDictionary<string, EndpointConfiguration> configurations;

        public EndpointConfigurationProvider(IEndpointConfigurationLoader endpointConfigurationLoader)
        {
            Requires.NotNull(endpointConfigurationLoader, nameof(endpointConfigurationLoader));

            this.configurations = endpointConfigurationLoader.Load();
        }

        public EndpointConfiguration GetConfiguration(string route)
        {
            Requires.NotNullOrEmpty(route, nameof(route));
            var trimmedRoute = route.TrimEnd('/');
            Requires.Argument(configurations.ContainsKey(trimmedRoute), nameof(route), ExceptionMessages.RouteEndpointConfigurationNotFound);

            return configurations[trimmedRoute];
        }
    }
}
