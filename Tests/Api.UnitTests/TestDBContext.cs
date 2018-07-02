// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 03-02-2018
//
// Last Modified By : MartinG
// Last Modified On : 05-18-2018
// ***********************************************************************
// <copyright file="TestDbContext.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>Defines an Entity Framework DB Context for use with the unit tests.</summary>
// ***********************************************************************
using System.Collections.Generic;
using Api.EntityFramework;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Api.UnitTests
{
    /// <summary>
    /// Class TestDbContext.
    /// </summary>
    /// <seealso cref="Api.EntityFramework.DatabaseContext" />
    public class TestDbContext : DatabaseContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestDbContext"/> class.
        /// </summary>
        internal TestDbContext()
            : base(loggerFactory: null) => Database.EnsureCreated();

        /// <summary>
        /// Creates the context with seeded data.
        /// </summary>
        /// <typeparam name="T">The generic type</typeparam>
        /// <param name="data">The data.</param>
        /// <returns>TestDbContext.</returns>
        public static TestDbContext CreateContextWithSeededData<T>(IEnumerable<T> data)
            where T : class => new TestDbContext().Seed(data);

        /// <summary>
        /// Seeds the specified data.
        /// </summary>
        /// <typeparam name="T">The generic type</typeparam>
        /// <param name="data">The data.</param>
        /// <returns>TestDbContext.</returns>
        internal TestDbContext Seed<T>(IEnumerable<T> data)
            where T : class
        {
            AddRange(data);

            SaveChanges();

            return this;
        }

        /// <summary>
        /// Called when [configuring].
        /// </summary>
        /// <param name="optionsBuilder">The options builder.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var sqlConnection = new SqliteConnection("Data Source=:memory:");
            sqlConnection.Open();
            optionsBuilder.UseSqlite(sqlConnection, k => k.SuppressForeignKeyEnforcement());
        }
    }
}
