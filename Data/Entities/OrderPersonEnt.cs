// ***********************************************************************
// Assembly         : Api
// Author           : markco
// Created          : 05-18-2018
//
// Last Modified By : markco
// Last Modified On : 05-22-2018
// ***********************************************************************
// <copyright file="InsolvencyOrderPersonEntity.cs" company="Callcredit Information Group.">
//     Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>InsolvencyOrderPersonEntity class</summary>
// ***********************************************************************
using System;

namespace Api.EntityFramework.Entities
{
    /// <summary>
    /// Represents an InsolvencyOrderPerson.
    /// </summary>
    public partial class InsolvencyOrderPersonEntity
    {
        /// <summary>
        /// Gets or sets the unique insolvency order person identifier.
        /// </summary>
        /// <value>The insolvency order person identifier.</value>
        public int InsolvencyOrderPersonId { get; set; }

        /// <summary>
        /// Gets or sets the unique InsolvencyOrder identifier.
        /// </summary>
        /// <value>The insolvency order identifier.</value>
        public int InsolvencyOrderId { get; set; }

        /// <summary>
        /// Gets or sets the insolvency order entity.
        /// </summary>
        /// <value>The insolvency order entity, <see cref="InsolvencyOrderEntity" />. (Entity Framework navigation property).</value>
        public InsolvencyOrderEntity InsolvencyOrderEntity { get; set; }

        /// <summary>
        /// Gets or sets the person title.
        /// </summary>
        /// <value>The title to be used.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the person forename.
        /// </summary>
        /// <value>The forename to be used.</value>
        public string Forename { get; set; }

        /// <summary>
        /// Gets or sets the person surname.
        /// </summary>
        /// <value>The surname to be used.</value>
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>The date of birth.</value>
        public DateTime? DateOfBirth { get; set; }
    }
}
