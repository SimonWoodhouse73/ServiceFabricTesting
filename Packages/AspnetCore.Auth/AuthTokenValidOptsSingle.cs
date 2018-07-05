namespace Callcredit.AspNetCore.Authorization
{
    /// <summary>
    /// Options used when validating a token with a single audience.
    /// </summary>
    public class AuthorizationTokenValidationOptionsSingleAudience : AuthorizationTokenValidationOptions
    {
        /// <summary>
        /// Gets or sets the audience to be validated
        /// </summary>
        /// <value>
        /// The audience string that is contained in the "aud" claim in the access token
        /// </value>
        public string Audience { get; set; }
    }
}
