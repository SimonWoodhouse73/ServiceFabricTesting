using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using Callcredit.RESTful.Services.Tokens;
using Microsoft.Extensions.DependencyInjection;

namespace Api.BootStrapping
{
    /// <summary>
    /// Class SecurityComponents.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class SecurityComponents
    {
        /// <summary>
        /// Adds the JWT handlers.
        /// </summary>
        /// <param name="services">The services collection used to configure the Insolvency Service.</param>
        public static void AddJwtHandlers(this IServiceCollection services)
        {
            services.AddScoped<JwtSecurityTokenHandler>();
            services.AddScoped<IClaimsStringFormatter, ClaimsStringFormatter>();
        }
    }
}
