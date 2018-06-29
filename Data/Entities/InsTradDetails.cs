// ***********************************************************************
// Assembly         : Api
// Author           : markco
// Created          : 05-18-2018
//
// Last Modified By : markco
// Last Modified On : 05-22-2018
// ***********************************************************************
// <copyright file="InsolvencyTradingDetailsEntity.cs" company="Callcredit Information Group.">
//     Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>InsolvencyTradingDetailsEntity class</summary>
// ***********************************************************************
namespace Api.EntityFramework.Entities
{
    /// <summary>
    /// Class InsolvencyTradingDetailsEntity.
    /// </summary>
    public partial class InsolvencyTradingDetailsEntity
    {
        /// <summary>
        /// Gets or sets the insolvency trading identifier.
        /// </summary>
        /// <value>The insolvency trading identifier.</value>
        public int InsolvencyTradingId { get; set; }

        /// <summary>
        /// Gets or sets the insolvency order identifier.
        /// </summary>
        /// <value>The insolvency order identifier.</value>
        public int InsolvencyOrderId { get; set; }

        /// <summary>
        /// Gets or sets the insolvency order entity.
        /// </summary>
        /// <value>The insolvency order entity. <see cref="InsolvencyOrderEntity" /></value>
        public InsolvencyOrderEntity InsolvencyOrderEntity { get; set; }

        /// <summary>
        /// Gets or sets the name of the trading.
        /// </summary>
        /// <value>The name of the trading.</value>
        public string TradingName { get; set; }

        /// <summary>
        /// Gets or sets the trading address.
        /// </summary>
        /// <value>The trading address.</value>
        public string TradingAddress { get; set; }
    }
}
