// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 03-02-2018
//
// Last Modified By : MartinG
// Last Modified On : 04-20-2018
// ***********************************************************************
// <copyright file="TestFixture.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>Defines a generic test fixture for use with unit tests.</summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using AutoFixture;

namespace Api.UnitTests
{
    /// <summary>
    /// Class TestFixture.
    /// </summary>
    internal static class TestFixture
    {
        /// <summary>
        /// The fixture
        /// </summary>
        private static Fixture fixture = new Fixture();

        /// <summary>
        /// Initializes static members of the <see cref="TestFixture"/> class.
        /// </summary>
        static TestFixture()
        {
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <typeparam name="T">The generic type</typeparam>
        /// <returns>T.</returns>
        internal static T Create<T>() => fixture.Create<T>();

        /// <summary>
        /// Creates the many.
        /// </summary>
        /// <typeparam name="T">The generic type</typeparam>
        /// <param name="numberOfRecords">The number of records.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        internal static IEnumerable<T> CreateMany<T>(int numberOfRecords)
            =>
            fixture.CreateMany<T>(numberOfRecords);
    }
}
