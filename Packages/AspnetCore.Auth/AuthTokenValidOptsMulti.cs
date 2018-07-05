using System.Collections.Generic;

namespace Callcredit.AspNetCore.Authorization
{
    /// <summary>
    /// Options used when validating an access token with multiple audiences.
    /// </summary>
    public class AuthorizationTokenValidationOptionsMultiAudience : AuthorizationTokenValidationOptions
    {
        /// <summary>
        /// Gets or sets the audiences to be validated
        /// </summary>
        /// <value>
        /// The audience string that is contained in the "aud" claim in the access token
        /// </value>
        public IEnumerable<string> Audiences { get; set; }
    }
}
