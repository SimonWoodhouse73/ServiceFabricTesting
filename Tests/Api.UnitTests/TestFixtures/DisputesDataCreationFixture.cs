// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 04-20-2018
//
// Last Modified By : MartinG
// Last Modified On : 05-18-2018
// ***********************************************************************
// <copyright file="DisputesDataCreationFixture.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>DisputesDataCreationFixture</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Api.EntityFramework.Entities;
using AutoFixture;

namespace Api.UnitTests.TestFixtures
{
    /// <summary>
    /// Class DisputesDataCreationFixture.
    /// </summary>
    public static class DisputesDataCreationFixture
    {
        /// <summary>
        /// The fixture
        /// </summary>
        private static readonly Fixture Fixture = new Fixture();

        /// <summary>
        /// Initializes static members of the <see cref="DisputesDataCreationFixture"/> class.
        /// </summary>
        static DisputesDataCreationFixture()
        {
            Fixture.Customize<DisputeEntity>(
                disputeEntity =>
                disputeEntity
                .With(x => x.DisputeId)
                .With(x => x.DateRaised, DateTime.Now.AddMonths(-2))
                .With(x => x.Displayed, true)
                .With(x => x.Deleted, false));

            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        /// <summary>
        /// Creates the many.
        /// </summary>
        /// <param name="numberOfRecords">The number of records.</param>
        /// <returns>IEnumerable&lt;DisputeEntity&gt;.</returns>
        internal static IEnumerable<DisputeEntity> CreateMany(int numberOfRecords) =>
            Fixture.CreateMany<DisputeEntity>(numberOfRecords);
    }
}
