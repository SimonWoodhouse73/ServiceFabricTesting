using System.Collections.Generic;

namespace Api.EntityFramework.Entities
{
    /// <summary>
    /// Represents a Court
    /// </summary>
    public partial class CourtEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for a Court
        /// </summary>
        /// <value>The court identifier.</value>
        public int CourtId { get; set; }

        /// <summary>
        /// Gets or sets the name of the court.
        /// </summary>
        /// <value>The name of the court.</value>
        public string CourtName { get; set; }

        /// <summary>
        /// Gets or sets court ID Code
        /// </summary>
        /// <value>Court ID Code</value>
        public string CourtCode { get; set; }

        /// <summary>
        /// Gets or sets court District
        /// </summary>
        /// <value>District Name</value>
        public string District { get; set; }

        /// <summary>
        /// Gets or sets a collection of insolvency order histories.
        /// </summary>
        /// <value>The insolvency order histories, <see cref="InsolvencyOrderHistoryEntity" />.</value>
        public ICollection<InsolvencyOrderHistoryEntity> InsolvencyOrderHistories { get; set; }
    }
}
