using Microsoft.AspNetCore.Authorization;

namespace Callcredit.AspNetCore.Authorization
{
    /// <summary>
    /// A model to store the <see cref="Authorization.Scope"/> object as a raw string which can then be handled by the <see cref="ScopeHandler"/> class.
    /// Implements <see cref="IAuthorizationRequirement" />
    /// </summary>
    public class ScopeRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// Gets the scope object as a raw string.
        /// </summary>
        /// <value>
        /// The string value of the required scope for validation.
        /// </value>
        public string Scope { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScopeRequirement"/> class with the required scope.
        /// </summary>
        /// <param name="scope">The required scope for validation.</param>
        public ScopeRequirement(string scope)
        {
            Scope = scope;
        }
    }
}
