using System.Collections.Generic;

namespace Callcredit.AspNetCore.Authorization
{
    /// <summary>
    /// <see cref="IScopeReader"/> interface that defines methods to be used to read scopes from the claims string.
    /// </summary>
    public interface IScopeReader
    {
        /// <summary>
        /// Returns the first valid scope found in the scope claims string.
        /// </summary>
        /// <returns><see cref="Scope"/></returns>
        Scope GetScope();

        /// <summary>
        /// Returns a list of valid scopes from a scope claims string.
        /// </summary>
        /// <returns><see cref="List{Scope}"/></returns>
        List<Scope> GetScopes();

        /// <summary>
        /// Returns a scope that matches the realization pattern of the service coupled with the intended action.
        /// </summary>
        /// <param name="realization">The service realization pattern to match against</param>
        /// <param name="action">The action that the claim should support</param>
        /// <returns><see cref="Scope"/></returns>
        Scope GetScopeByAction(string realization, string action);
    }
}
