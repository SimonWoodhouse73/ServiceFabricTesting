namespace Callcredit.AspNetCore.Authorization
{
    /// <summary>
    /// Represents a scope claim that controls access and priveleges to certain resources.
    /// </summary>
    public class Scope
    {
        /// <summary>
        /// Gets or sets the geographical region the scope represents.
        /// </summary>
        /// <value>
        /// The string value of the Region
        /// </value>
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the domain of information the scope represents.
        /// </summary>
        /// <value>
        /// The string value of the Pattern
        /// </value>
        public string Pattern { get; set; }

        /// <summary>
        /// Gets or sets the name of the service or data asset.
        /// </summary>
        /// <value>
        /// The string value of the Realization
        /// </value>
        public string Realization { get; set; }

        /// <summary>
        /// Gets or sets the action performed against the service or data asset.
        /// </summary>
        /// <value>
        /// The string value of action
        /// </value>
        public string Action { get; set; }

        /// <summary>
        /// Gets or sets a particular view of the returned data. This can be a subset of the original.
        /// </summary>
        /// <value>
        /// The string value of view
        /// </value>
        public string View { get; set; }
    }
}
