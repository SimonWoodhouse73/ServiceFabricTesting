using Callcredit.RESTful.Services.Endpoints;

namespace Callcredit.AspNetCore.RESTful.Services.Endpoints
{
    public class RouteEndpointConfiguration
    {
        public string RouteTemplate { get; set; }

        public EndpointConfiguration EndpointConfiguration { get; set; }
    }
}
