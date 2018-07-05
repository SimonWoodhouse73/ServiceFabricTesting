using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using Validation;

namespace Callcredit.AspNetCore.Authorization
{
    /// <summary>
    /// Factory class for creating <see cref="JwtBearerOptions" />
    /// </summary>
    public class JwtBearerOptionsFactory
    {
        private const string LocalEnvironmentPrefix = "http://localhost:";

        /// <summary>
        /// Create <see cref="JwtBearerOptions" /> from <see cref="AuthorizationTokenValidationOptions" />, setting defaults for some options
        /// </summary>
        /// <param name="options">The options to use when creating the <see cref="JwtBearerOptions" /></param>
        /// <returns>A new fully configured <see cref="JwtBearerOptions" /></returns>
        public static JwtBearerOptions CreateSingleAudienceJwtBearerOptions(AuthorizationTokenValidationOptionsSingleAudience options)
        {
            Requires.NotNull(options, nameof(options));
            Requires.NotNull(options.Issuer, nameof(options.Issuer));
            Requires.NotNull(options.DiscoveryEndpoint, nameof(options.DiscoveryEndpoint));
            Requires.NotNull(options.Audience, nameof(options.Audience));

            var jwtBearerOptions = CreateJwtBearerOptions(options);
            jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidAudience = options.Audience,

                ValidateIssuer = true,
                ValidIssuer = options.Issuer,

                ValidateLifetime = true
            };
            return jwtBearerOptions;
        }

        /// <summary>
        /// Create <see cref="JwtBearerOptions" /> from <see cref="AuthorizationTokenValidationOptions" />, setting defaults for some options
        /// </summary>
        /// <param name="options">The options to use when creating the <see cref="JwtBearerOptions" /></param>
        /// <returns>A new fully configured <see cref="JwtBearerOptions" /></returns>
        public static JwtBearerOptions CreateMultiAudienceJwtBearerOptions(AuthorizationTokenValidationOptionsMultiAudience options)
        {
            Requires.NotNull(options, nameof(options));
            Requires.NotNull(options.Issuer, nameof(options.Issuer));
            Requires.NotNull(options.DiscoveryEndpoint, nameof(options.DiscoveryEndpoint));
            Requires.NotNull(options.Audiences, nameof(options.Audiences));

            var jwtBearerOptions = CreateJwtBearerOptions(options);
            jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidAudiences = options.Audiences,

                ValidateIssuer = true,
                ValidIssuer = options.Issuer,

                ValidateLifetime = true
            };

            return jwtBearerOptions;
        }

        private static JwtBearerOptions CreateJwtBearerOptions(AuthorizationTokenValidationOptions options)
        {
            var isHostedEnvironment = !options.DiscoveryEndpoint.StartsWith(LocalEnvironmentPrefix, StringComparison.OrdinalIgnoreCase);

            return new JwtBearerOptions
            {
                RequireHttpsMetadata = isHostedEnvironment,
                MetadataAddress = options.DiscoveryEndpoint,
                IncludeErrorDetails = false,
                AutomaticAuthenticate = true,
                AutomaticChallenge = false
            };
        }
    }
}
