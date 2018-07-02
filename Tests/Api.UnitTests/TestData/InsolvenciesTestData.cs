// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 03-02-2018
//
// Last Modified By : MartinG
// Last Modified On : 05-18-2018
// ***********************************************************************
// <copyright file="InsolvenciesTestData.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>InsolvenciesTestData</summary>
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
    /// Class InsolvenciesTestData.
    /// </summary>
    public static class InsolvenciesTestData
    {
        /// <summary>
        /// Creates the insolvencies actuals.
        /// </summary>
        /// <returns>IEnumerable&lt;InsolvencyOrderEntity&gt;.</returns>
        public static IEnumerable<InsolvencyOrderEntity> CreateInsolvenciesActuals()
            =>
            new List<InsolvencyOrderEntity>
            {
                new InsolvencyOrderEntity
                {
                    InsolvencyOrderId = 1,
                    ResidenceId = 11,
                    OrderDate = new DateTime(2018, 03, 06),
                    ValueOfDebt = 5000
                },
                new InsolvencyOrderEntity
                {
                    InsolvencyOrderId = 2,
                    ResidenceId = 11,
                    OrderDate = new DateTime(2017, 03, 06),
                    ValueOfDebt = null
                },
                new InsolvencyOrderEntity
                {
                    InsolvencyOrderId = 3,
                    ResidenceId = 11,
                    OrderDate = new DateTime(2016, 03, 06),
                    ValueOfDebt = 7000
                },
                new InsolvencyOrderEntity
                {
                    InsolvencyOrderId = 4,
                    ResidenceId = 14,
                    OrderDate = new DateTime(2015, 03, 06),
                    ValueOfDebt = 9000
                },
                new InsolvencyOrderEntity
                {
                    InsolvencyOrderId = 5,
                    ResidenceId = 15,
                    OrderDate = new DateTime(2012, 03, 06),
                    ValueOfDebt = 11000
                },
                new InsolvencyOrderEntity
                {
                    InsolvencyOrderId = 6,
                    ResidenceId = 16,
                    OrderDate = new DateTime(2011, 03, 06),
                    ValueOfDebt = 13000
                },
                new InsolvencyOrderEntity
                {
                    InsolvencyOrderId = 7,
                    ResidenceId = 17,
                    OrderDate = new DateTime(2008, 03, 06),
                    ValueOfDebt = 15000
                },
                new InsolvencyOrderEntity
                {
                    InsolvencyOrderId = 8,
                    ResidenceId = 18,
                    OrderDate = new DateTime(2007, 03, 06),
                    ValueOfDebt = null
                },
                new InsolvencyOrderEntity
                {
                    InsolvencyOrderId = 9,
                    ResidenceId = 19,
                    OrderDate = new DateTime(2006, 03, 06),
                    ValueOfDebt = 17000
                },
                new InsolvencyOrderEntity
                {
                    InsolvencyOrderId = 10,
                    ResidenceId = 20,
                    OrderDate = new DateTime(2018, 03, 06),
                    ValueOfDebt = 19000
                },
                new InsolvencyOrderEntity
                {
                    InsolvencyOrderId = 11,
                    ResidenceId = 21,
                    OrderDate = new DateTime(2017, 03, 06),
                    ValueOfDebt = 20000
                },
                new InsolvencyOrderEntity
                {
                    InsolvencyOrderId = 12,
                    ResidenceId = 21,
                    OrderDate = new DateTime(2016, 03, 06),
                    ValueOfDebt = 22000
                },
                new InsolvencyOrderEntity
                {
                    InsolvencyOrderId = 13,
                    ResidenceId = 23,
                    OrderDate = new DateTime(2011, 03, 06),
                    ValueOfDebt = 24000
                }
            }.AsQueryable();

        /// <summary>
        /// Creates the insolvencies expecteds.
        /// </summary>
        /// <returns>IEnumerable&lt;InsolvencyOrderModel&gt;.</returns>
        public static IEnumerable<InsolvencyOrderModel> CreateInsolvenciesExpecteds()
            =>
            new InsolvencyOrderModel[]
            {
                new InsolvencyOrderModel
                {
                    InsolvencyOrderId = 1,
                    ResidenceId = 11,
                    OrderDate = new DateTime(2018, 03, 06),
                    ValueOfDebt = 5000,
                    Disputes = new List<DisputeModel>()
                },
                new InsolvencyOrderModel
                {
                    InsolvencyOrderId = 2,
                    ResidenceId = 11,
                    OrderDate = new DateTime(2017, 03, 06),
                    ValueOfDebt = null,
                    Disputes = new List<DisputeModel>()
                },
                new InsolvencyOrderModel
                {
                    InsolvencyOrderId = 3,
                    ResidenceId = 11,
                    OrderDate = new DateTime(2016, 03, 06),
                    ValueOfDebt = 7000,
                    Disputes = new List<DisputeModel>()
                },
                new InsolvencyOrderModel
                {
                    InsolvencyOrderId = 4,
                    ResidenceId = 14,
                    OrderDate = new DateTime(2015, 03, 06),
                    ValueOfDebt = 9000,
                    Disputes = new List<DisputeModel>()
                },
                ////new InsolvencyModel
                ////{
                ////    InsolvencyOrderId = 5,
                ////    ResidenceId = 15,
                ////    OrderDate = new DateTime(2012, 03, 06),
                ////    ValueOfDebt = 11000
                ////},
                ////new InsolvencyModel
                ////{
                ////    InsolvencyOrderId = 6,
                ////    ResidenceId = 16,
                ////    OrderDate = new DateTime(2011, 03, 06),
                ////    ValueOfDebt = 13000
                ////},
                ////new InsolvencyModel
                ////{
                ////    InsolvencyOrderId = 7,
                ////    ResidenceId = 17,
                ////    OrderDate = new DateTime(2008, 03, 06),
                ////    ValueOfDebt = 15000
                ////},
                ////new InsolvencyModel
                ////{
                ////    InsolvencyOrderId = 8,
                ////    ResidenceId = 18,
                ////    OrderDate = new DateTime(2007, 03, 06),
                ////    ValueOfDebt = null
                ////},
                ////new InsolvencyModel
                ////{
                ////    InsolvencyOrderId = 9,
                ////    ResidenceId = 19,
                ////    OrderDate = new DateTime(2006, 03, 06),
                ////    ValueOfDebt = 17000
                ////},
                new InsolvencyOrderModel
                {
                    InsolvencyOrderId = 10,
                    ResidenceId = 20,
                    OrderDate = new DateTime(2018, 03, 06),
                    ValueOfDebt = 19000,
                    Disputes = new List<DisputeModel>()
                },
                new InsolvencyOrderModel
                {
                    InsolvencyOrderId = 11,
                    ResidenceId = 21,
                    OrderDate = new DateTime(2017, 03, 06),
                    ValueOfDebt = 20000,
                    Disputes = new List<DisputeModel>()
                },
                new InsolvencyOrderModel
                {
                    InsolvencyOrderId = 12,
                    ResidenceId = 21,
                    OrderDate = new DateTime(2016, 03, 06),
                    ValueOfDebt = 22000,
                    Disputes = new List<DisputeModel>()
                }

                ////,
                ////new InsolvencyModel
                ////{
                ////    InsolvencyOrderId = 13,
                ////    ResidenceId = 23,
                ////    OrderDate = new DateTime(2011, 03, 06),
                ////    ValueOfDebt = 24000
                ////}
            }.AsQueryable();

        /// <summary>
        /// Creates the flattened insolvencies actuals.
        /// </summary>
        /// <returns>IEnumerable&lt;InsolvencyOrderFlattenedEntity&gt;.</returns>
        public static IEnumerable<InsolvencyOrderFlattenedEntity> CreateFlattenedInsolvenciesActuals()
            =>
                new List<InsolvencyOrderFlattenedEntity>
                {
                    new InsolvencyOrderFlattenedEntity
                    {
                        InsolvencyOrderId = 1,
                        ResidenceId = 11,
                        OrderDate = new DateTime(2018, 03, 06),
                        ValueOfDebt = 5000
                    },
                    new InsolvencyOrderFlattenedEntity
                    {
                        InsolvencyOrderId = 2,
                        ResidenceId = 11,
                        OrderDate = new DateTime(2018, 03, 06),
                        ValueOfDebt = 7000
                    },
                    new InsolvencyOrderFlattenedEntity
                    {
                        InsolvencyOrderId = 3,
                        ResidenceId = 11,
                        OrderDate = new DateTime(2018, 03, 06),
                        ValueOfDebt = 15000,
                        InsolvencyOrderPerson_Title = "Mr",
                        InsolvencyOrderPerson_Forename = "Sid",
                        InsolvencyOrderPerson_Surname = "Snake",
                        InsolvencyOrderPerson_DateOfBirth = new DateTime(2018, 01, 01),
                        InsolvencyOrderAddress_InsolvencyOrderAddressId = 1,
                        InsolvencyTradingDetails_InsolvencyTradingId = 1,
                        InsolvencyOrderHistory_CourtId = 1
                    },
                    new InsolvencyOrderFlattenedEntity
                    {
                        InsolvencyOrderId = 4,
                        ResidenceId = 14,
                        OrderDate = new DateTime(2018, 03, 06),
                        ValueOfDebt = 7000,
                        InsolvencyOrderPerson_Title = "Mr",
                        InsolvencyOrderPerson_Forename = "Pete",
                        InsolvencyOrderPerson_Surname = "Python",
                        InsolvencyOrderPerson_DateOfBirth = new DateTime(2018, 02, 01),
                        InsolvencyOrderAddress_InsolvencyOrderAddressId = 1,
                        InsolvencyTradingDetails_InsolvencyTradingId = 1,
                        InsolvencyOrderHistory_CourtId = 1
                    },
                    new InsolvencyOrderFlattenedEntity
                    {
                        InsolvencyOrderId = 10,
                        ResidenceId = 20,
                        OrderDate = new DateTime(2018, 03, 06),
                        ValueOfDebt = 19000,
                        InsolvencyOrderPerson_Title = "Mrs",
                        InsolvencyOrderPerson_Forename = "Paula",
                        InsolvencyOrderPerson_Surname = "Python",
                        InsolvencyOrderPerson_DateOfBirth = new DateTime(2018, 02, 01),
                        InsolvencyOrderAddress_InsolvencyOrderAddressId = 1,
                        InsolvencyTradingDetails_InsolvencyTradingId = 1,
                        InsolvencyOrderHistory_CourtId = 1
                    },
                    new InsolvencyOrderFlattenedEntity
                    {
                        InsolvencyOrderId = 11,
                        ResidenceId = 21,
                        OrderDate = new DateTime(2017, 03, 06),
                        ValueOfDebt = 20000,
                        InsolvencyOrderPerson_Title = "Mrs",
                        InsolvencyOrderPerson_Forename = "Sarah",
                        InsolvencyOrderPerson_Surname = "Snake",
                        InsolvencyOrderPerson_DateOfBirth = new DateTime(2018, 02, 01),
                        InsolvencyOrderAddress_InsolvencyOrderAddressId = 1,
                        InsolvencyTradingDetails_InsolvencyTradingId = 1,
                        InsolvencyOrderHistory_CourtId = 1
                    },
                    new InsolvencyOrderFlattenedEntity
                    {
                        InsolvencyOrderId = 12,
                        ResidenceId = 21,
                        OrderDate = new DateTime(2016, 03, 06),
                        ValueOfDebt = 22000,
                        InsolvencyOrderPerson_Title = "Mr",
                        InsolvencyOrderPerson_Forename = "Len",
                        InsolvencyOrderPerson_Surname = "Lizard",
                        InsolvencyOrderPerson_DateOfBirth = new DateTime(2018, 02, 01),
                        InsolvencyOrderAddress_InsolvencyOrderAddressId = 1,
                        InsolvencyTradingDetails_InsolvencyTradingId = 1,
                        InsolvencyOrderHistory_CourtId = 1
                    }
                }.AsQueryable();

        /// <summary>
        /// Creates the insolvencies expecteds.
        /// </summary>
        /// <param name="pageInformation">The page information.</param>
        /// <returns>IEnumerable&lt;InsolvencyOrderModel&gt;.</returns>
        public static IEnumerable<InsolvencyOrderModel> CreateInsolvenciesExpecteds(PageInformation pageInformation) => CreateInsolvenciesExpecteds()
            .Skip((pageInformation.Page - 1) * pageInformation.PageSize).Take(pageInformation.PageSize);

        /// <summary>
        /// Gets the expecteds by residence identifier.
        /// </summary>
        /// <param name="residenceId">The residence identifier.</param>
        /// <returns>IEnumerable&lt;InsolvencyOrderModel&gt;.</returns>
        public static IEnumerable<InsolvencyOrderModel> GetExpectedsByResidenceId(int residenceId) =>
            CreateInsolvenciesExpecteds()
            .Where(record => int.Equals(record.ResidenceId, residenceId));

        /// <summary>
        /// Gets the expecteds by residence identifier.
        /// </summary>
        /// <param name="pageInformation">The page information.</param>
        /// <param name="residenceId">The residence identifier.</param>
        /// <returns>IEnumerable&lt;InsolvencyOrderModel&gt;.</returns>
        public static IEnumerable<InsolvencyOrderModel>
        GetExpectedsByResidenceId(PageInformation pageInformation, int residenceId) =>
            CreateInsolvenciesExpecteds()
            .Where(record => int.Equals(record.ResidenceId, residenceId))
            .Page(pageInformation);

        /// <summary>
        /// Gets the insolvency by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>InsolvencyOrderModel.</returns>
        public static InsolvencyOrderModel GetInsolvencyById(int id) =>
            CreateInsolvenciesExpecteds().FirstOrDefault(record => record.InsolvencyOrderId == id);
    }
}
