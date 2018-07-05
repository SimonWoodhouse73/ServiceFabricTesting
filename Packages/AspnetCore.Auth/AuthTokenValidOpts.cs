namespace Callcredit.AspNetCore.Authorization
{
    /// <summary>
    /// Options used when validating an access token
    /// </summary>
    public class AuthorizationTokenValidationOptions
    {
        /// <summary>
        /// Gets or sets the discovery endpoint url for the issuing identity server.
        /// </summary>
        /// <value>
        /// Fully qualified url string, usually has the form https://example.com/basepath/.well-known/openid-configuration
        /// </value>
        public string DiscoveryEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the issuer to be validated
        /// </summary>
        /// <value>
        /// The issuer string that is contained in the "iss" claim in the access token
        /// </value>
        public string Issuer { get; set; }
    }
}
