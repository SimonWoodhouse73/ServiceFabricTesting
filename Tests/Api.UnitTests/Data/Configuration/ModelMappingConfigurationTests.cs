// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 04-20-2018
//
// Last Modified By : MartinG
// Last Modified On : 05-18-2018
// ***********************************************************************
// <copyright file="ModelMappingConfigurationTests_InsolvencyOrderEntity.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>ModelMappingConfigurationTests_InsolvencyOrderEntity</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Api.EntityFramework.Configuration;
using Api.EntityFramework.Entities;
using Api.UnitTests.Comparers;
using Callcredit.Domain.Insolvencies.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Api.UnitTests.Data.Configuration
{
    /// <summary>
    /// Class ModelMappingConfigurationTests_InsolvencyOrderEntity.
    /// </summary>
    [TestClass]
    public class ModelMappingConfigurationTests_InsolvencyOrderEntity
    {
        /// <summary>
        /// Mappings the insolvency order entity to model with configured model mapping returns equal model.
        /// </summary>
        [TestMethod]
        public void MappingInsolvencyOrderEntityToModel_WithConfiguredModelMapping_ReturnsEqualModel()
        {
            // Arrange
            AutoMapper.Mapper.Reset();
            ModelMappingConfiguration.CreateModelMapping();

            var insolvencyOrderEntity =
                new InsolvencyOrderEntity()
                {
                    InsolvencyOrderId = 1,
                    InsolvencyOrderTypeId = 2,
                    InsolvencyOrderType =
                        new InsolvencyOrderTypeEntity()
                        {
                            InsolvencyOrderTypeId = 2,
                            CallReportCode = "code1",
                            Description = "type desc",
                            InsolvencyOrderEntities = null
                        },
                    ResidenceId = 3,
                    OrderDate = DateTime.Parse("2014-02-15"),
                    RestrictionsTypeId = 4,
                    InsolvencyRestrictionsType =
                        new InsolvencyRestrictionsTypeEntity()
                        {
                            RestrictionsTypeId = 4,
                            Code = "A",
                            Description = "restriction description",
                            InsolvencyOrderEntities = null
                        },
                    RestrictionsStartDate = DateTime.Parse("2015-02-15"),
                    RestrictionsEndDate = DateTime.Parse("2015-04-15"),
                    LineOfBusiness = "Entertainer",
                    InsolvencyServiceCaseId = 5,
                    ValueOfDebt = 5000,
                    DischargeDate = DateTime.Parse("2017-02-15"),
                    InsolvencyOrderPersons =
                        new List<InsolvencyOrderPersonEntity>()
                        {
                            new InsolvencyOrderPersonEntity()
                            {
                                InsolvencyOrderPersonId = 8,
                                InsolvencyOrderId = 1,
                                DateOfBirth = DateTime.Parse("1990, 07, 21"),
                                Forename = "John",
                                Surname = "Smith",
                                Title = "Mr"
                            }
                        },
                    InsolvencyOrderAddresses =
                        new List<InsolvencyOrderAddressEntity>()
                        {
                            new InsolvencyOrderAddressEntity()
                            {
                                InsolvencyOrderAddressId = 9,
                                InsolvencyOrderId = 1,
                                LastKnownAddress = "12 Low Street",
                                LastKnownPostCode = "OX4 3PE"
                            }
                        },
                    InsolvencyOrderHistory =
                        new List<InsolvencyOrderHistoryEntity>()
                        {
                            new InsolvencyOrderHistoryEntity()
                            {
                                InsolvencyOrderHistoryId = 10,
                                InsolvencyOrderId = 1,
                                CaseReference = "1234",
                                CaseYear = 2014,
                                CourtId = 11,
                                Court =
                                    new CourtEntity()
                                    {
                                        CourtId = 11,
                                        CourtCode = "AB",
                                        CourtName = "The Old Bailey",
                                        District = "Nine"
                                    }
                            }
                        },
                    InsolvencyTradingDetails =
                        new List<InsolvencyTradingDetailsEntity>()
                        {
                            new InsolvencyTradingDetailsEntity()
                            {
                                InsolvencyTradingId = 12,
                                InsolvencyOrderId = 1,
                                TradingAddress = "20 High Street",
                                TradingName = "Tescos"
                            }
                        },
                    InsolvencyOrderDisputes =
                        new List<InsolvencyOrderDisputeEntity>()
                        {
                            new InsolvencyOrderDisputeEntity()
                            {
                                DisputeId = 13,
                                InsolvencyOrderId = 1,
                                Deleted = false,
                                Dispute =
                                    new DisputeEntity()
                                    {
                                        DisputeId = 13,
                                        DateRaised = DateTime.Parse("2016-08-14"),
                                        Deleted = false,
                                        Displayed = true,
                                        InsolvencyOrderDisputes = null,
                                        RefNum = "7890"
                                    },
                                InsolvencyOrder = null
                            }
                        }
                };

            InsolvencyOrderModel expected =
                new InsolvencyOrderModel()
                {
                    InsolvencyOrderId = 1,
                    InsolvencyOrderTypeId = 2,
                    InsolvencyOrderType =
                        new InsolvencyOrderTypeModel()
                        {
                            InsolvencyOrderTypeId = 2,
                            Code = "code1",
                            Description = "type desc"
                        },
                    ResidenceId = 3,
                    OrderDate = DateTime.Parse("2014-02-15"),
                    RestrictionsTypeId = 4,
                    RestrictionsType =
                        new InsolvencyOrderRestrictionsTypeModel()
                        {
                            RestrictionsTypeId = 4,
                            Code = "A",
                            Description = "restriction description"
                        },
                    RestrictionsStartDate = DateTime.Parse("2015-02-15"),
                    RestrictionsEndDate = DateTime.Parse("2015-04-15"),
                    LineOfBusiness = "Entertainer",
                    InsolvencyServiceCaseId = 5,
                    ValueOfDebt = 5000,
                    DischargeDate = DateTime.Parse("2017-02-15"),
                    InsolvencyOrderPersons =
                        new List<InsolvencyOrderPersonModel>()
                        {
                            new InsolvencyOrderPersonModel()
                            {
                                InsolvencyOrderPersonId = 8,
                                InsolvencyOrderId = 1,
                                DateOfBirth = DateTime.Parse("1990, 07, 21"),
                                Forename = "John",
                                Surname = "Smith",
                                Title = "Mr"
                            }
                        },
                    InsolvencyOrderAddresses =
                        new List<InsolvencyOrderAddressModel>()
                        {
                            new InsolvencyOrderAddressModel()
                            {
                                InsolvencyOrderAddressId = 9,
                                InsolvencyOrderId = 1,
                                Address = "12 Low Street",
                                PostCode = "OX4 3PE"
                            }
                        },
                    InsolvencyOrderHistories =
                        new List<InsolvencyOrderHistoryModel>()
                        {
                            new InsolvencyOrderHistoryModel()
                            {
                                InsolvencyOrderHistoryId = 10,
                                InsolvencyOrderId = 1,
                                CaseReference = "1234",
                                CaseYear = 2014,
                                CourtId = 11,
                                Court =
                                    new CourtModel()
                                    {
                                        CourtId = 11,
                                        Code = "AB",
                                        Name = "The Old Bailey",
                                        District = "Nine"
                                    }
                            }
                        },
                    InsolvencyOrderTradingDetails =
                        new List<InsolvencyOrderTradingDetailsModel>()
                        {
                            new InsolvencyOrderTradingDetailsModel()
                            {
                                InsolvencyOrderTradingDetailsId = 12,
                                InsolvencyOrderId = 1,
                                Address = "20 High Street",
                                Name = "Tescos"
                            }
                        },
                    Disputes =
                        new List<DisputeModel>()
                        {
                            new DisputeModel()
                            {
                                DisputeId = 13,
                                InsolvencyOrderId = 1,
                                DateRaised = DateTime.Parse("2016-08-14"),
                                Displayed = true,
                                ReferenceNumber = "7890"
                            }
                        }
                };

            // Act
            var actual = AutoMapper.Mapper.Map<InsolvencyOrderModel>(insolvencyOrderEntity);

            // Assert
            Assert.IsTrue(new InsolvencyModelEqualityComparer().Equals(expected, actual));
        }
    }
}
