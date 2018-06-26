using System.Diagnostics.CodeAnalysis;
using Callcredit.AspNetCore.RESTful.Services;
using Callcredit.RESTful.Standards.Permitted;
using Halcyon.Web.HAL.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Api.BootStrapping
{
    /// <summary>
    /// Configures the Insolvency Service to use Mvc middleware components
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Mvc
    {
        /// <summary>
        /// Adds the MVC components.
        /// </summary>
        /// <param name="services">The services collection used to configure the Insolvency Service.</param>
        public static void AddMvcComponents(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddScoped<IActionDescriptorCollectionProvider, ActionDescriptorCollectionProvider>();

            services
                .AddMvc()
                .AddMvcOptions(c =>
                {
                    c.Filters.Add(typeof(ValidateHeadersFilter));
                    c.Filters.Add(typeof(AddMandatoryResponseHeadersFilter));
                    c.OutputFormatters.RemoveType<JsonOutputFormatter>();
                    c.OutputFormatters.Add(
                        new JsonHalOutputFormatter(
                            new[] { ValidContentTypes.HalContentType }));
                });
        }
    }
}
