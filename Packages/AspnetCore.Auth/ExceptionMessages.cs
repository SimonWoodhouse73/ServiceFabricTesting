using Microsoft.AspNetCore.Http;

namespace Callcredit.AspNetCore.Authorization
{
    /// <summary>
    /// A collection of messages to be used with Scope-based exceptions.
    /// </summary>
    public static class ExceptionMessages
    {
        /// <summary>
        /// Exception message pertaining to a supplied JWT scope not meeting the set naming conventions.
        /// </summary>
        public const string InvalidScopeFormat =
            "Supplied Scope does not match expected format this must follow the convention Region/Pattern/Realization.Action Or Region/Pattern/Realization.Action.View";

        /// <summary>
        /// Exception message pertaining to a supplied JWT Realization not meeting the set naming conventions.
        /// </summary>
        public const string InvalidRealizationFormat =
            "Supplied Realization does not match expected format.";

        /// <summary>
        /// Exception message pertaining to a supplied JWT Action not meeting the set naming conventions.
        /// </summary>
        public const string InvalidActionFormat =
            "Supplied Action does not match expected format.";

        /// <summary>
        /// Exception message pertaining to the realization value not matching any scope claim.
        /// </summary>
        public const string ScopeNotFound =
            "Requested realization did not match any claim scope";

        /// <summary>
        /// Exception message stating that realization and action values within the Scope claims are duplicates and must be unique.
        /// </summary>
        public const string DuplicateScopes =
            "Scope claims contains duplicate scope realization and action values.";

        /// <summary>
        /// Exception message stating that the <see cref="IHttpContextAccessor"/> is null and that an instance must be registered in the services container.
        /// </summary>
        public const string HttpContextAccessorNull =
            "IHttpContextAccessor is null, register an IHttpContext instance in the services container with 'services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();'";
    }
}
