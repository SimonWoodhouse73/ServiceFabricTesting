// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 03-02-2018
//
// Last Modified By : MartinG
// Last Modified On : 04-23-2018
// ***********************************************************************
// <copyright file="TestHelpers.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>TestHelpers</summary>
// ***********************************************************************
using System;
using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using Callcredit.Domain.Insolvencies.Models;

namespace Api.UnitTests.Helpers
{
    /// <summary>
    /// Class TestHelpers.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class TestHelpers
    {
        /// <summary>
        /// The fixture
        /// </summary>
        private static Fixture fixture = new Fixture();

        /// <summary>
        /// Creates the dispute.
        /// </summary>
        /// <returns>DisputeModel.</returns>
        public static DisputeModel CreateDispute()
        {
            return fixture.Create<DisputeModel>();
        }

        /// <summary>
        /// Creates the insolvency address.
        /// </summary>
        /// <returns>InsolvencyOrderAddressModel.</returns>
        public static InsolvencyOrderAddressModel CreateInsolvencyAddress()
        {
            return fixture.Create<InsolvencyOrderAddressModel>();
        }

        /// <summary>
        /// Creates the insolvency history.
        /// </summary>
        /// <returns>InsolvencyOrderHistoryModel.</returns>
        public static InsolvencyOrderHistoryModel CreateInsolvencyHistory()
        {
            return fixture.Create<InsolvencyOrderHistoryModel>();
        }

        /// <summary>
        /// Creates the insolvency.
        /// </summary>
        /// <returns>InsolvencyOrderModel.</returns>
        public static InsolvencyOrderModel CreateInsolvency()
        {
            return fixture.Create<InsolvencyOrderModel>();
        }

        /// <summary>
        /// Creates the trade detail.
        /// </summary>
        /// <returns>InsolvencyOrderTradingDetailsModel.</returns>
        internal static InsolvencyOrderTradingDetailsModel CreateTradeDetail()
        {
            return fixture.Create<InsolvencyOrderTradingDetailsModel>();
        }

        /// <summary>
        /// Creates the person.
        /// </summary>
        /// <returns>InsolvencyOrderPersonModel.</returns>
        internal static InsolvencyOrderPersonModel CreatePerson()
        {
            return fixture.Create<InsolvencyOrderPersonModel>();
        }
    }
}
