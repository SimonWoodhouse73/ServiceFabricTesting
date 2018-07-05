using Callcredit.RESTful.Services.Endpoints;
using System.Collections.Generic;

namespace Callcredit.AspNetCore.RESTful.Services.Endpoints
{
    public class RouteEndpointConfiguration
    {
        public string RouteTemplate { get; set; }

        public EndpointConfiguration EndpointConfiguration { get; set; }
    }
}

public class RouteEndpointConfigurations : List<RouteEndpointConfiguration>
{
}

