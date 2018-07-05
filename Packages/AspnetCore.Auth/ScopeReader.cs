using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Validation;

namespace Callcredit.AspNetCore.Authorization
{
    /// <summary>
    /// An implementation of <see cref="IScopeReader"/> used to read scopes from the claims string.
    /// </summary>
    public class ScopeReader : IScopeReader
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScopeReader"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The mechanism for accessing the <see cref="HttpContext"/></param>
        public ScopeReader(IHttpContextAccessor httpContextAccessor)
        {
            Requires.Argument(httpContextAccessor != null, nameof(httpContextAccessor), ExceptionMessages.HttpContextAccessorNull);

            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Returns the first valid scope found in the scope claims string
        /// </summary>
        /// <returns>Scope</returns>
        public Scope GetScope()
        {
            var currentScope = GetClaimsScope().Trim().Split(' ').FirstOrDefault();

            Requires.That(HasValidScopeFormat(currentScope), nameof(currentScope), ExceptionMessages.InvalidScopeFormat);

            return GetScope(currentScope);
        }

        /// <summary>
        /// Returns a list of valid scopes from a scope claims string
        /// </summary>
        /// <returns>List{Scope}</returns>
        public List<Scope> GetScopes()
        {
            var scopes = new List<Scope>();
            string[] scopeDelimiters = { " " };
            var claimScopes = GetClaimsScope().Trim().Split(scopeDelimiters, StringSplitOptions.None);

            foreach (var scope in claimScopes)
            {
                Requires.That(HasValidScopeFormat(scope), nameof(scope), ExceptionMessages.InvalidScopeFormat);
                scopes.Add(GetScope(scope));
            }

            Requires.That(
                !scopes.GroupBy(scope => new { scope.Realization, scope.Action }).Any(result => result.Count() > 1),
                nameof(scopes),
                ExceptionMessages.DuplicateScopes);

            return scopes;
        }

        /// <summary>
        /// Returns a scope that matches the realization pattern of the service coupled with the intended action.
        /// </summary>
        /// <param name="realization">The service realization pattern to match against</param>
        /// <param name="action">The action that the claim should support</param>
        /// <returns>Scope</returns>
        public Scope GetScopeByAction(string realization, string action)
        {
            Requires.NotNullOrEmpty(realization, nameof(realization));
            Requires.NotNullOrEmpty(action, nameof(action));
            Requires.That(HasValidRealization(realization), nameof(realization), ExceptionMessages.InvalidRealizationFormat);
            Requires.That(HasValidAction(action), nameof(action), ExceptionMessages.InvalidActionFormat);

            var returnedScope = GetScopes().FirstOrDefault(scope => scope.Realization == realization && scope.Action == action);

            if (returnedScope == null)
            {
                throw new ArgumentException(ExceptionMessages.ScopeNotFound);
            }

            return returnedScope;
        }

        private string GetClaimsScope()
        => httpContextAccessor.HttpContext.User.Claims
           .FirstOrDefault(claim => claim.Type == "scope")
           .Value;

        private Scope GetScope(string claimsScopeItem)
        {
            var scope = new Scope();
            string[] scopeDelimiters = { @"/", "." };
            var scopeItems = claimsScopeItem.Split(scopeDelimiters, StringSplitOptions.None);

            scope.Region = scopeItems[0];
            scope.Pattern = scopeItems[1];
            scope.Realization = scopeItems[2];
            scope.Action = scopeItems[3];

            if (scopeItems.Length == 5)
            {
                scope.View = scopeItems[4];
            }

            return scope;
        }

        private bool HasValidRealization(string realization)
        {
            const string realizationPattern = @"^[a-z-]+";

            var realizationRegularExpression = new Regex(realizationPattern);
            return realizationRegularExpression.IsMatch(realization);
        }

        private bool HasValidAction(string action)
        {
            const string actionPattern = @"^[a-z]+";

            var actionRegularExpression = new Regex(actionPattern);
            return actionRegularExpression.IsMatch(action);
        }

        /// <summary>
        /// Verifies that the scope follows the patterns:
        /// Region/Pattern/Realization.Action Or
        /// Region/Pattern/Realization.Action.View
        /// </summary>
        /// <param name="scope">The scope to verify</param>
        /// <returns>boolean value</returns>
        private bool HasValidScopeFormat(string scope)
        {
            const string scopePattern = @"^[a-z]+\/[a-z-]+\/[a-z-]+.[a-z]+$|^[a-z]+\/[a-z-]+\/[a-z-]+.[a-z]+.[a-z]+$";

            var scopeRegularExpression = new Regex(scopePattern);
            return scopeRegularExpression.IsMatch(scope);
        }
    }
}
