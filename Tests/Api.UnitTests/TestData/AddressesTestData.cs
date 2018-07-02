// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 04-20-2018
//
// Last Modified By : MartinG
// Last Modified On : 05-18-2018
// ***********************************************************************
// <copyright file="AddressesTestData.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>AddressesTestData</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using Api.EntityFramework.Entities;
using Callcredit.Domain.Insolvencies.Models;
using Callcredit.Domain.Repositories;

namespace Api.UnitTests.TestData
{
    /// <summary>
    /// Class AddressesTestData.
    /// </summary>
    public static class AddressesTestData
    {
        /// <summary>
        /// Creates the addresses actuals.
        /// </summary>
        /// <returns>IEnumerable&lt;InsolvencyOrderAddressEntity&gt;.</returns>
        public static IEnumerable<InsolvencyOrderAddressEntity> CreateAddressesActuals() => new List<InsolvencyOrderAddressEntity>
                {
            new InsolvencyOrderAddressEntity
            {
                InsolvencyOrderAddressId = 1,
                InsolvencyOrderId = 1,
                LastKnownPostCode = "1",
                LastKnownAddress = "21"
            },
            new InsolvencyOrderAddressEntity
            {
                InsolvencyOrderAddressId = 2,
                InsolvencyOrderId = 1,
                LastKnownPostCode = "2",
                LastKnownAddress = "22"
            },
            new InsolvencyOrderAddressEntity
            {
                InsolvencyOrderAddressId = 3,
                InsolvencyOrderId = 1,
                LastKnownPostCode = "3",
                LastKnownAddress = "23"
            },
            new InsolvencyOrderAddressEntity
            {
                InsolvencyOrderAddressId = 4,
                InsolvencyOrderId = 3,
                LastKnownPostCode = "4",
                LastKnownAddress = "24"
            },
            new InsolvencyOrderAddressEntity
            {
                InsolvencyOrderAddressId = 5,
                InsolvencyOrderId = 5,
                LastKnownPostCode = "5",
                LastKnownAddress = "25"
            },
            new InsolvencyOrderAddressEntity
            {
                InsolvencyOrderAddressId = 6,
                InsolvencyOrderId = 5,
                LastKnownPostCode = "6",
                LastKnownAddress = "26"
            },
            new InsolvencyOrderAddressEntity
            {
                InsolvencyOrderAddressId = 7,
                InsolvencyOrderId = 5,
                LastKnownPostCode = "7",
                LastKnownAddress = "27"
            },
            new InsolvencyOrderAddressEntity
            {
                InsolvencyOrderAddressId = 8,
                InsolvencyOrderId = 8,
                LastKnownPostCode = "8",
                LastKnownAddress = "28"
            },
            new InsolvencyOrderAddressEntity
            {
                InsolvencyOrderAddressId = 9,
                InsolvencyOrderId = 8,
                LastKnownPostCode = "9",
                LastKnownAddress = "29"
            },
            new InsolvencyOrderAddressEntity
            {
                InsolvencyOrderAddressId = 10,
                InsolvencyOrderId = 10,
                LastKnownPostCode = "10",
                LastKnownAddress = "30"
            },
            new InsolvencyOrderAddressEntity
            {
                InsolvencyOrderAddressId = 11,
                InsolvencyOrderId = 11,
                LastKnownPostCode = "11",
                LastKnownAddress = "31"
            }
        };

        /// <summary>
        /// Creates the addresses expecteds.
        /// </summary>
        /// <returns>IEnumerable&lt;InsolvencyOrderAddressModel&gt;.</returns>
        public static IEnumerable<InsolvencyOrderAddressModel> CreateAddressesExpecteds() => new InsolvencyOrderAddressModel[]
        {
            new InsolvencyOrderAddressModel
            {
                InsolvencyOrderAddressId = 1,
                InsolvencyOrderId = 1,
                PostCode = "1",
                Address = "21"
            },
            new InsolvencyOrderAddressModel
            {
                InsolvencyOrderAddressId = 2,
                InsolvencyOrderId = 1,
                PostCode = "2",
                Address = "22"
            },
            new InsolvencyOrderAddressModel
            {
                InsolvencyOrderAddressId = 3,
                InsolvencyOrderId = 1,
                PostCode = "3",
                Address = "23"
            },
            new InsolvencyOrderAddressModel
            {
                InsolvencyOrderAddressId = 4,
                InsolvencyOrderId = 3,
                PostCode = "4",
                Address = "24"
            },
            new InsolvencyOrderAddressModel
            {
                InsolvencyOrderAddressId = 5,
                InsolvencyOrderId = 5,
                PostCode = "5",
                Address = "25"
            },
            new InsolvencyOrderAddressModel
            {
                InsolvencyOrderAddressId = 6,
                InsolvencyOrderId = 5,
                PostCode = "6",
                Address = "26"
            },
            new InsolvencyOrderAddressModel
            {
                InsolvencyOrderAddressId = 7,
                InsolvencyOrderId = 5,
                PostCode = "7",
                Address = "27"
            },
            new InsolvencyOrderAddressModel
            {
                InsolvencyOrderAddressId = 8,
                InsolvencyOrderId = 8,
                PostCode = "8",
                Address = "28"
            },
            new InsolvencyOrderAddressModel
            {
                InsolvencyOrderAddressId = 9,
                InsolvencyOrderId = 8,
                PostCode = "9",
                Address = "29"
            },
            new InsolvencyOrderAddressModel
            {
                InsolvencyOrderAddressId = 10,
                InsolvencyOrderId = 10,
                PostCode = "10",
                Address = "30"
            },
            new InsolvencyOrderAddressModel
            {
                InsolvencyOrderAddressId = 11,
                InsolvencyOrderId = 11,
                PostCode = "11",
                Address = "31"
            }
        };

        /// <summary>
        /// Gets the expecteds by insolvency order identifier.
        /// </summary>
        /// <param name="insolvencyOrderid">The insolvency orderid.</param>
        /// <returns>IEnumerable&lt;InsolvencyOrderAddressModel&gt;.</returns>
        public static IEnumerable<InsolvencyOrderAddressModel> GetExpectedsByInsolvencyOrderId(int insolvencyOrderid) =>
            CreateAddressesExpecteds()
            .Where(record => int.Equals(record.InsolvencyOrderId, insolvencyOrderid));

        /// <summary>
        /// Gets the expecteds by insolvency order identifier.
        /// </summary>
        /// <param name="pageInformation">The page information.</param>
        /// <param name="insolvencyOrderId">The insolvency order identifier.</param>
        /// <returns>IEnumerable&lt;InsolvencyOrderAddressModel&gt;.</returns>
        public static IEnumerable<InsolvencyOrderAddressModel> GetExpectedsByInsolvencyOrderId(PageInformation pageInformation, int insolvencyOrderId) =>
            CreateAddressesExpecteds()
            .Where(record => int.Equals(record.InsolvencyOrderId, insolvencyOrderId))
            .Page(pageInformation);

        /// <summary>
        /// Creates the addresses expecteds.
        /// </summary>
        /// <param name="pageInformation">The page information.</param>
        /// <returns>IEnumerable&lt;InsolvencyOrderAddressModel&gt;.</returns>
        public static IEnumerable<InsolvencyOrderAddressModel> CreateAddressesExpecteds(PageInformation pageInformation) =>
            CreateAddressesExpecteds()
            .Skip((pageInformation.Page - 1) * pageInformation.PageSize)
            .Take(pageInformation.PageSize);

        /// <summary>
        /// Gets the addresses by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>InsolvencyOrderAddressModel.</returns>
        public static InsolvencyOrderAddressModel GetAddressesById(int id) =>
            CreateAddressesExpecteds()
            .FirstOrDefault(record => record.InsolvencyOrderAddressId == id);
    }
}
