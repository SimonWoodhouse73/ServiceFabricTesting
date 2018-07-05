namespace Callcredit.AspNetCore.RESTful.Services
{
    public static class ExceptionMessages
    {
        public const string RouteNotFound = "Requested route did not match any registered routes";
        public const string RouteEndpointConfigurationNotFound = "Requested route did not match any registered endpoint configurations";
        public const string HttpContextAccessorNull =
            "IHttpContextAccessor is null, register an IHttpContext instance in the services container with 'services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();'";
        public const string ActionContextAccessor =
            "IActionContextAccessor is null, register an IActionContext instance in the services container with 'services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();'";
    }
}
