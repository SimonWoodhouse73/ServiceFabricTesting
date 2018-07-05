namespace Callcredit.Authorization.OnPremise.Issue
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using Configuration;
    using Validation;
    using Validation.Providers;

    public class OnPremiseTokenIssuer
    {
        private readonly ISigningCredentialsProvider signingCredentialsProvider;

        public OnPremiseTokenIssuer()
        {
            var certificateProvider = new X509StoreCertificateProvider();
            var storePorvider = new X509StoreProvider();
            this.signingCredentialsProvider = new X509SigningCredentialsProvider(certificateProvider, storePorvider);
        }

        public string Issue(SecurityConfiguration configuration)
        {
            var now = DateTimeOffset.UtcNow;
            var expires = now.AddMinutes(configuration.ExpiryMinutes);

            var claims = new List<Claim>();
            if (configuration.CustomClaims != null)
            {
                claims
                    .AddRange(
                        configuration
                            .CustomClaims
                            .Where(x => x != null)
                            .Select(x => new Claim(x.ClaimType, x.Value)).ToList());
            }

            claims.Add(new Claim("iat", now.ToUnixTimeSeconds().ToString()));

            var signingCredentials =
                this.signingCredentialsProvider
                    .SigningCredentials(configuration.CertificateOptions);

            var tokenObject =
                new JwtSecurityToken(
                    configuration.Issuer,
                    configuration.Audience,
                    claims,
                    now.UtcDateTime,
                    expires.UtcDateTime,
                    signingCredentials);

            return
                new JwtSecurityTokenHandler()
                    .WriteToken(tokenObject);
        }
    }
}
