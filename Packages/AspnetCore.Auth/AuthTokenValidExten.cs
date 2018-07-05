using Microsoft.AspNetCore.Builder;
using Validation;

namespace Callcredit.AspNetCore.Authorization
{
    /// <summary>
    /// Extensions class for the Authorization Token Validation middleware
    /// </summary>
    public static class AuthorizationTokenValidationExtensions
    {
        /// <summary>
        /// Extend the IApplicationBuilder to use the Authorization Token Validation middleware with the given options
        /// </summary>
        /// <param name="builder">Instance of an <see cref="IApplicationBuilder" /></param>
        /// <param name="options">The <see cref="AuthenticationOptions" /> to use for validating the token</param>
        /// <returns>The given <see cref="IApplicationBuilder" /></returns>
        public static IApplicationBuilder UseAuthorizationTokenValidation(this IApplicationBuilder builder, AuthorizationTokenValidationOptionsSingleAudience options)
        {
            Requires.NotNull(options, nameof(options));

            return builder.UseJwtBearerAuthentication(JwtBearerOptionsFactory.CreateSingleAudienceJwtBearerOptions(options));
        }

        /// <summary>
        /// Extend the IApplicationBuilder to use the Authorization Token Validation middleware with the given options
        /// </summary>
        /// <param name="builder">Instance of an <see cref="IApplicationBuilder" /></param>
        /// <param name="options">The <see cref="AuthenticationOptions" /> to use for validating the token</param>
        /// <returns>The given <see cref="IApplicationBuilder" /></returns>
        public static IApplicationBuilder UseAuthorizationTokenValidation(this IApplicationBuilder builder, AuthorizationTokenValidationOptionsMultiAudience options)
        {
            Requires.NotNull(options, nameof(options));

            return builder.UseJwtBearerAuthentication(JwtBearerOptionsFactory.CreateMultiAudienceJwtBearerOptions(options));
        }
    }
}
