// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 04-20-2018
//
// Last Modified By : MartinG
// Last Modified On : 05-18-2018
// ***********************************************************************
// <copyright file="InsolvenciesDataCreationFixture.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>InsolvenciesDataCreationFixture</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Api.EntityFramework.Entities;
using AutoFixture;

namespace Api.UnitTests.TestFixtures
{
    /// <summary>
    /// Class InsolvenciesDataCreationFixture.
    /// </summary>
    public static class InsolvenciesDataCreationFixture
    {
        /// <summary>
        /// The fixture
        /// </summary>
        private static readonly Fixture Fixture = new Fixture();

        /// <summary>
        /// Initializes static members of the <see cref="InsolvenciesDataCreationFixture"/> class.
        /// </summary>
        static InsolvenciesDataCreationFixture()
        {
            Fixture.Customize<InsolvencyOrderEntity>(
                insolvencyOrderEntity =>
                insolvencyOrderEntity
                .With(x => x.InsolvencyOrderId)
                .With(x => x.ResidenceId, 1)
                .With(x => x.OrderDate, DateTime.Now.AddMonths(-2))
                .With(x => x.OnlineSuppressed, false)
                .With(x => x.InsolvencyOrderAddresses)
                .With(x => x.InsolvencyOrderDisputes)
                .With(x => x.InsolvencyOrderHistory)
                .With(x => x.InsolvencyOrderPersons)
                .With(x => x.InsolvencyOrderType)
                .With(x => x.InsolvencyRestrictionsType)
                .With(x => x.InsolvencyTradingDetails));

            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        /// <summary>
        /// Creates the many.
        /// </summary>
        /// <param name="numberOfRecords">The number of records.</param>
        /// <returns>IEnumerable&lt;InsolvencyOrderEntity&gt;.</returns>
        internal static IEnumerable<InsolvencyOrderEntity> CreateMany(int numberOfRecords) =>
            Fixture.CreateMany<InsolvencyOrderEntity>(numberOfRecords);
    }
}
