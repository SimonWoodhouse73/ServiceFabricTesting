// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 04-20-2018
//
// Last Modified By : MartinG
// Last Modified On : 05-18-2018
// ***********************************************************************
// <copyright file="AddressesDataCreationFixture.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>AddressesDataCreationFixture</summary>
// ***********************************************************************
using System.Collections.Generic;
using Api.EntityFramework.Entities;
using AutoFixture;

namespace Api.UnitTests.TestFixtures
{
    /// <summary>
    /// Class AddressesDataCreationFixture.
    /// </summary>
    public static class AddressesDataCreationFixture
    {
        /// <summary>
        /// The fixture
        /// </summary>
        private static readonly Fixture Fixture = new Fixture();

        /// <summary>
        /// Initializes static members of the <see cref="AddressesDataCreationFixture"/> class.
        /// </summary>
        static AddressesDataCreationFixture()
        {
            Fixture.Customize<InsolvencyOrderAddressEntity>(
                address =>
                address
                .With(x => x.InsolvencyOrderId, 1));

            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        /// <summary>
        /// Creates the many.
        /// </summary>
        /// <param name="numberOfRecords">The number of records.</param>
        /// <returns>IEnumerable&lt;InsolvencyOrderAddressEntity&gt;.</returns>
        internal static IEnumerable<InsolvencyOrderAddressEntity> CreateMany(int numberOfRecords) =>
            Fixture.CreateMany<InsolvencyOrderAddressEntity>(numberOfRecords);
    }
}
