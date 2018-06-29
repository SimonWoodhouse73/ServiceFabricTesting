// ***********************************************************************
// Assembly         : Api
// Author           : markco
// Created          : 05-18-2018
//
// Last Modified By : markco
// Last Modified On : 05-22-2018
// ***********************************************************************
// <copyright file="InsolvencyRestrictionsTypeEntity.cs" company="Callcredit Information Group.">
//     Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>InsolvencyRestrictionsTypeEntity class</summary>
// ***********************************************************************
using System.Collections.Generic;

namespace Api.EntityFramework.Entities
{
    /// <summary>
    /// Represents the details of an InsolvencyRestrictionsTypeEntity.
    /// </summary>
    public partial class InsolvencyRestrictionsTypeEntity
    {
        /// <summary>
        /// Gets or sets the unique Restrictions Type identifier.
        /// </summary>
        /// <value>The restrictions type identifier.</value>
        public short RestrictionsTypeId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The string description to be used.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the 4 letter restriction type code.
        /// </summary>
        /// <value>The code.</value>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the insolvency order entities. (Entity Framework navigation property).
        /// </summary>
        /// <value>The insolvency order entities.<see cref="InsolvencyOrderEntity" /></value>
        public ICollection<InsolvencyOrderEntity> InsolvencyOrderEntities { get; set; }
    }
}
