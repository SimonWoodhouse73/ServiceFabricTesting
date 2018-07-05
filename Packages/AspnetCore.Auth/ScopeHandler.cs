using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Callcredit.AspNetCore.Authorization
{
    /// <summary>
    /// A custom <see cref="AuthorizationHandler{ TRequirement }"/> for Scope claim that validates that the required scope is contained in the authenticated users claims.
    /// </summary>
    public class ScopeHandler : AuthorizationHandler<ScopeRequirement>
    {
        /// <summary>
        /// Validate that the required scope is contained in the authenticated users claims
        /// </summary>
        /// <param name="context">The <see cref="AuthorizationHandlerContext" /> context containing the authenticated User.</param>
        /// <param name="requirement">The <see cref="ScopeRequirement" /> containing the required <see cref="Scope"/>.</param>
        /// <returns>Succeed when the User has the required <see cref="Scope"/>, or context. Fail if not. Returns <see cref="Task.CompletedTask"/>.</returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ScopeRequirement requirement)
        {
            var scopes = context.User.Claims.FirstOrDefault(claim => claim.Type == OpenIdConnectParameterNames.Scope);

            if (string.IsNullOrEmpty(scopes?.Value))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            if (scopes.Value.Split(' ').Contains(requirement.Scope))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
