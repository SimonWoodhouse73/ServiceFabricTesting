// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 03-02-2018
//
// Last Modified By : MartinG
// Last Modified On : 05-18-2018
// ***********************************************************************
// <copyright file="InsolvenciesWithDisputesTestData.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>InsolvenciesWithDisputesTestData</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using Api.EntityFramework.Entities;
using Callcredit.Domain.Insolvencies.Models;

namespace Api.UnitTests.TestData
{
    /// <summary>
    /// Class InsolvenciesWithDisputesTestData.
    /// </summary>
    internal static class InsolvenciesWithDisputesTestData
    {
        /// <summary>
        /// Creates the insolvencies actuals.
        /// </summary>
        /// <returns>IEnumerable&lt;InsolvencyOrderEntity&gt;.</returns>
        public static IEnumerable<InsolvencyOrderEntity> CreateInsolvenciesActuals() => new List<InsolvencyOrderEntity>
         {
            new InsolvencyOrderEntity
            {
                InsolvencyOrderId = 1,
                ResidenceId = 11,
                OrderDate = new DateTime(2018, 03, 06),
                ValueOfDebt = 5000,
                InsolvencyOrderDisputes = new List<InsolvencyOrderDisputeEntity>()
                {
                    new InsolvencyOrderDisputeEntity()
                    {
                        InsolvencyOrderId = 1,
                        DisputeId = 1,
                        Dispute =
                            new DisputeEntity
                            {
                                DisputeId = 1,
                                RefNum = "1",
                                DateRaised = new DateTime(2017, 12, 06),
                                Displayed = true
                            }
                    },
                    new InsolvencyOrderDisputeEntity()
                    {
                        InsolvencyOrderId = 1,
                        DisputeId = 2,
                        Dispute =
                            new DisputeEntity
                            {
                                DisputeId = 2,
                                RefNum = "2",
                                DateRaised = new DateTime(2017, 11, 06),
                                Displayed = false
                            }
                    },
                    new InsolvencyOrderDisputeEntity()
                    {
                        InsolvencyOrderId = 1,
                        DisputeId = 3,
                        Dispute =
                            new DisputeEntity
                            {
                                DisputeId = 3,
                                RefNum = "3",
                                DateRaised = new DateTime(2017, 10, 06),
                                Displayed = true
                            }
                    }
                }
            },
            new InsolvencyOrderEntity
            {
                InsolvencyOrderId = 2,
                ResidenceId = 12,
                OrderDate = new DateTime(2017, 03, 06),
                ValueOfDebt = null
            },
            new InsolvencyOrderEntity
            {
                InsolvencyOrderId = 3,
                ResidenceId = 13,
                OrderDate = new DateTime(2016, 03, 06),
                ValueOfDebt = 7000,
                InsolvencyOrderDisputes = new List<InsolvencyOrderDisputeEntity>()
                {
                    new InsolvencyOrderDisputeEntity()
                    {
                        InsolvencyOrderId = 3,
                        DisputeId = 4,
                        Dispute =
                            new DisputeEntity
                            {
                                DisputeId = 4,
                                RefNum = "4",
                                DateRaised = new DateTime(2011, 09, 06),
                                Displayed = false
                            }
                    }
                }
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
                OrderDate = new DateTime(2014, 03, 06),
                ValueOfDebt = 11000,
                InsolvencyOrderDisputes = new List<InsolvencyOrderDisputeEntity>()
                {
                    new InsolvencyOrderDisputeEntity()
                    {
                        InsolvencyOrderId = 5,
                        DisputeId = 5,
                        Dispute =
                            new DisputeEntity
                            {
                                DisputeId = 5,
                                RefNum = "5",
                                DateRaised = new DateTime(2009, 08, 06),
                                Displayed = true
                            }
                    },
                    new InsolvencyOrderDisputeEntity()
                    {
                        InsolvencyOrderId = 5,
                        DisputeId = 6,
                        Dispute =
                            new DisputeEntity
                            {
                                DisputeId = 6,
                                RefNum = "6",
                                DateRaised = new DateTime(2013, 07, 06),
                                Displayed = true
                            }
                    },
                    new InsolvencyOrderDisputeEntity()
                    {
                        InsolvencyOrderId = 5,
                        DisputeId = 7,
                        Dispute =
                            new DisputeEntity
                            {
                                DisputeId = 7,
                                RefNum = "7",
                                DateRaised = new DateTime(2007, 06, 06),
                                Displayed = false
                            }
                    }
                }
            },
            new InsolvencyOrderEntity
            {
                InsolvencyOrderId = 6,
                ResidenceId = 16,
                OrderDate = new DateTime(2014, 03, 06),
                ValueOfDebt = 13000
            },
            new InsolvencyOrderEntity
            {
                InsolvencyOrderId = 7,
                ResidenceId = 17,
                OrderDate = new DateTime(2018, 03, 06),
                ValueOfDebt = 15000
            },
            new InsolvencyOrderEntity
            {
                InsolvencyOrderId = 8,
                ResidenceId = 18,
                OrderDate = new DateTime(2017, 03, 06),
                ValueOfDebt = null,
                InsolvencyOrderDisputes = new List<InsolvencyOrderDisputeEntity>
                {
                    new InsolvencyOrderDisputeEntity()
                    {
                        InsolvencyOrderId = 8,
                        DisputeId = 8,
                        Dispute =
                            new DisputeEntity
                            {
                                DisputeId = 8,
                                RefNum = "8",
                                DateRaised = new DateTime(2014, 05, 06),
                                Displayed = false
                            },
                    },
                    new InsolvencyOrderDisputeEntity()
                    {
                        InsolvencyOrderId = 8,
                        DisputeId = 9,
                        Dispute =
                            new DisputeEntity
                            {
                                DisputeId = 9,
                                RefNum = "9",
                                DateRaised = new DateTime(2002, 04, 06),
                                Displayed = false
                            }
                    }
                }
            },
            new InsolvencyOrderEntity
            {
                InsolvencyOrderId = 9,
                ResidenceId = 19,
                OrderDate = new DateTime(2016, 03, 06),
                ValueOfDebt = 17000
            },
            new InsolvencyOrderEntity
            {
                InsolvencyOrderId = 10,
                ResidenceId = 20,
                OrderDate = new DateTime(2018, 03, 06),
                ValueOfDebt = 19000,
                InsolvencyOrderDisputes = new List<InsolvencyOrderDisputeEntity>
                {
                    new InsolvencyOrderDisputeEntity()
                    {
                        InsolvencyOrderId = 10,
                        DisputeId = 10,
                        Dispute =
                        new DisputeEntity
                        {
                            DisputeId = 10,
                            RefNum = "10",
                            DateRaised = new DateTime(2001, 03, 06),
                            Displayed = true
                        }
                    }
                }
            },
            new InsolvencyOrderEntity
            {
                InsolvencyOrderId = 11,
                    ResidenceId = 21,
                    OrderDate = new DateTime(2017, 03, 06),
                ValueOfDebt = 20000,
                InsolvencyOrderDisputes = new List<InsolvencyOrderDisputeEntity>
                {
                    new InsolvencyOrderDisputeEntity()
                    {
                        InsolvencyOrderId = 11,
                        DisputeId = 11,
                        Dispute =
                        new DisputeEntity
                        {
                            DisputeId = 11,
                            RefNum = "11",
                            DateRaised = new DateTime(2017, 12, 06),
                            Displayed = true
                        }
                    }
                }
            },
            new InsolvencyOrderEntity
            {
                InsolvencyOrderId = 12,
                ResidenceId = 22,
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
         };

        /// <summary>
        /// Creates the insolvencies expecteds.
        /// </summary>
        /// <returns>IEnumerable&lt;InsolvencyOrderModel&gt;.</returns>
        public static IEnumerable<InsolvencyOrderModel> CreateInsolvenciesExpecteds() => new InsolvencyOrderModel[]
        {
            new InsolvencyOrderModel
            {
                InsolvencyOrderId = 1,
                ResidenceId = 11,
                OrderDate = new DateTime(2018, 03, 06),
                Disputes = new List<DisputeModel>
                {
                    new DisputeModel
                    {
                        DisputeId = 1,
                        InsolvencyOrderId = 1,
                        ReferenceNumber = "1",
                        DateRaised = new DateTime(2017, 12, 06),
                        Displayed = true
                    },
                    new DisputeModel
                    {
                        DisputeId = 3,
                        InsolvencyOrderId = 1,
                        ReferenceNumber = "3",
                        DateRaised = new DateTime(2017, 10, 06),
                        Displayed = true
                    }
                }
            },
            new InsolvencyOrderModel
            {
                InsolvencyOrderId = 2,
                ResidenceId = 12,
                OrderDate = new DateTime(2017, 03, 06),
                Disputes = new List<DisputeModel> { }
            },
            new InsolvencyOrderModel
            {
                InsolvencyOrderId = 3,
                ResidenceId = 13,
                OrderDate = new DateTime(2016, 03, 06),
                Disputes = new List<DisputeModel> { }
            },
            new InsolvencyOrderModel
            {
                InsolvencyOrderId = 4,
                ResidenceId = 14,
                OrderDate = new DateTime(2015, 03, 06),
                Disputes = new List<DisputeModel> { }
            },
            new InsolvencyOrderModel
            {
                InsolvencyOrderId = 5,
                ResidenceId = 15,
                OrderDate = new DateTime(2014, 03, 06),
                Disputes = new List<DisputeModel>
                {
                    new DisputeModel
                    {
                        DisputeId = 6,
                        InsolvencyOrderId = 5,
                        ReferenceNumber = "6",
                        DateRaised = new DateTime(2013, 07, 06),
                        Displayed = true
                    }
                }
            },
            new InsolvencyOrderModel
            {
                InsolvencyOrderId = 6,
                ResidenceId = 17,
                OrderDate = new DateTime(2018, 03, 06),
                Disputes = new List<DisputeModel> { }
            },
            new InsolvencyOrderModel
            {
                InsolvencyOrderId = 7,
                ResidenceId = 18,
                OrderDate = new DateTime(2017, 03, 06),
                Disputes = new List<DisputeModel> { }
            },
            new InsolvencyOrderModel
            {
                InsolvencyOrderId = 8,
                ResidenceId = 19,
                OrderDate = new DateTime(2016, 03, 06),
                Disputes = new List<DisputeModel> { }
            },
            new InsolvencyOrderModel
            {
                InsolvencyOrderId = 9,
                ResidenceId = 20,
                OrderDate = new DateTime(2018, 03, 06),
                Disputes = new List<DisputeModel>
                {
                }
            },
            new InsolvencyOrderModel
            {
                InsolvencyOrderId = 10,
                ResidenceId = 21,
                OrderDate = new DateTime(2017, 03, 06),
                Disputes = new List<DisputeModel>
                {
                    new DisputeModel
                    {
                        DisputeId = 11,
                        InsolvencyOrderId = 11,
                        ReferenceNumber = "11",
                        DateRaised = new DateTime(2017, 12, 06),
                        Displayed = true
                    }
                }
            },
            new InsolvencyOrderModel
            {
                InsolvencyOrderId = 11,
                ResidenceId = 22,
                OrderDate = new DateTime(2016, 03, 06),
                Disputes = new List<DisputeModel> { }
            },
            new InsolvencyOrderModel
            {
                InsolvencyOrderId = 12,
                ResidenceId = 22,
                OrderDate = new DateTime(2016, 03, 06),
                Disputes = new List<DisputeModel> { }
            }
        };

        /// <summary>
        /// Gets the insolvency by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>InsolvencyOrderModel.</returns>
        public static InsolvencyOrderModel GetInsolvencyById(int id) =>
            CreateInsolvenciesExpecteds().FirstOrDefault(record => record.InsolvencyOrderId == id);

        /// <summary>
        /// Creates the insolvencies actuals.
        /// </summary>
        /// <returns>IEnumerable&lt;InsolvencyOrderFlattenedEntity&gt;.</returns>
        public static IEnumerable<InsolvencyOrderFlattenedEntity> CreateFlattenedInsolvenciesActuals() => new List<InsolvencyOrderFlattenedEntity>
         {
            new InsolvencyOrderFlattenedEntity
            {
                InsolvencyOrderId = 1,
                ResidenceId = 11,
                OrderDate = new DateTime(2018, 03, 06),
                ValueOfDebt = 5000,
                Dispute_DisputeId = 1,
                Dispute_RefNum = "1",
                DateRaised = new DateTime(2017, 12, 06),
                Displayed = true
            },
            new InsolvencyOrderFlattenedEntity
            {
                InsolvencyOrderId = 2,
                ResidenceId = 12,
                OrderDate = new DateTime(2017, 03, 06),
                ValueOfDebt = null
            },
            new InsolvencyOrderFlattenedEntity
            {
                InsolvencyOrderId = 3,
                ResidenceId = 13,
                OrderDate = new DateTime(2016, 03, 06),
                ValueOfDebt = 7000,
                Dispute_DisputeId = 4,
                Dispute_RefNum = "4",
                DateRaised = new DateTime(2011, 09, 06),
                Displayed = false
            },
            new InsolvencyOrderFlattenedEntity
            {
                InsolvencyOrderId = 4,
                ResidenceId = 14,
                OrderDate = new DateTime(2015, 03, 06),
                ValueOfDebt = 9000
            },
            new InsolvencyOrderFlattenedEntity
            {
                InsolvencyOrderId = 5,
                ResidenceId = 15,
                OrderDate = new DateTime(2014, 03, 06),
                ValueOfDebt = 11000,
                Dispute_DisputeId = 5,
                Dispute_RefNum = "5",
                DateRaised = new DateTime(2009, 08, 06),
                Displayed = true
             },
             new InsolvencyOrderFlattenedEntity
             {
                 InsolvencyOrderId = 6,
                 ResidenceId = 15,
                 OrderDate = new DateTime(2014, 03, 06),
                 ValueOfDebt = 11000,
                 Dispute_DisputeId = 6,
                 Dispute_RefNum = "6",
                 DateRaised = new DateTime(2013, 07, 06),
                 Displayed = true
             },
             new InsolvencyOrderFlattenedEntity
             {
                 InsolvencyOrderId = 7,
                 ResidenceId = 15,
                 OrderDate = new DateTime(2014, 03, 06),
                 ValueOfDebt = 11000,
                 Dispute_DisputeId = 7,
                 Dispute_RefNum = "7",
                 DateRaised = new DateTime(2007, 06, 06),
                 Displayed = false
             },
             new InsolvencyOrderFlattenedEntity
            {
                InsolvencyOrderId = 8,
                ResidenceId = 16,
                OrderDate = new DateTime(2016, 03, 06),
                ValueOfDebt = 13000
            },
            new InsolvencyOrderFlattenedEntity
            {
                InsolvencyOrderId = 9,
                ResidenceId = 17,
                OrderDate = new DateTime(2018, 03, 06),
                ValueOfDebt = 15000
            },
            new InsolvencyOrderFlattenedEntity
            {
                InsolvencyOrderId = 10,
                ResidenceId = 18,
                OrderDate = new DateTime(2017, 03, 06),
                ValueOfDebt = null,
                Dispute_DisputeId = 8,
                Dispute_RefNum = "8",
                DateRaised = new DateTime(2014, 05, 06),
                Displayed = false
            },
            new InsolvencyOrderFlattenedEntity
            {
                 InsolvencyOrderId = 11,
                 ResidenceId = 18,
                 OrderDate = new DateTime(2017, 03, 06),
                 ValueOfDebt = null,
                 Dispute_DisputeId = 9,
                 Dispute_RefNum = "9",
                 DateRaised = new DateTime(2014, 04, 06),
                 Displayed = false
            },
            new InsolvencyOrderFlattenedEntity
            {
                InsolvencyOrderId = 12,
                ResidenceId = 19,
                OrderDate = new DateTime(2016, 03, 06),
                ValueOfDebt = 17000
            },
            new InsolvencyOrderFlattenedEntity
            {
                InsolvencyOrderId = 13,
                ResidenceId = 20,
                OrderDate = new DateTime(2018, 03, 06),
                ValueOfDebt = 19000,
                Dispute_DisputeId = 10,
                Dispute_RefNum = "10",
                DateRaised = new DateTime(2001, 03, 06),
                Displayed = true
            },
            new InsolvencyOrderFlattenedEntity
            {
                InsolvencyOrderId = 14,
                ResidenceId = 21,
                OrderDate = new DateTime(2017, 03, 06),
                ValueOfDebt = 20000,
                Dispute_DisputeId = 11,
                Dispute_RefNum = "11",
                DateRaised = new DateTime(2017, 12, 06),
                Displayed = true
            }
         };

        /// <summary>
        /// Gets the insolvency by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>InsolvencyOrderModel.</returns>
        public static InsolvencyOrderModel GetFlattenedInsolvencyById(int id) =>
            CreateFlattenedInsolvenciesExpecteds().FirstOrDefault(record => record.InsolvencyOrderId == id);

        /// <summary>
        /// Creates the insolvencies expecteds.
        /// </summary>
        /// <returns>IEnumerable&lt;InsolvencyOrderModel&gt;.</returns>
        public static IEnumerable<InsolvencyOrderModel> CreateFlattenedInsolvenciesExpecteds() => new InsolvencyOrderModel[]
        {
            new InsolvencyOrderModel
            {
                InsolvencyOrderId = 1,
                ResidenceId = 11,
                OrderDate = new DateTime(2018, 03, 06),
                Disputes = new List<DisputeModel>
                {
                    new DisputeModel
                    {
                        DisputeId = 1,
                        InsolvencyOrderId = 1,
                        ReferenceNumber = "1",
                        DateRaised = new DateTime(2017, 12, 06),
                        Displayed = true
                    },
                    new DisputeModel
                    {
                        DisputeId = 3,
                        InsolvencyOrderId = 1,
                        ReferenceNumber = "3",
                        DateRaised = new DateTime(2017, 10, 06),
                        Displayed = true
                    }
                }
            },
            new InsolvencyOrderModel
            {
                InsolvencyOrderId = 2,
                ResidenceId = 12,
                OrderDate = new DateTime(2017, 03, 06),
                Disputes = new List<DisputeModel> { }
            },
            new InsolvencyOrderModel
            {
                InsolvencyOrderId = 3,
                ResidenceId = 13,
                OrderDate = new DateTime(2016, 03, 06),
                Disputes = new List<DisputeModel> { }
            },
            new InsolvencyOrderModel
            {
                InsolvencyOrderId = 4,
                ResidenceId = 14,
                OrderDate = new DateTime(2015, 03, 06),
                Disputes = new List<DisputeModel> { }
            },
            new InsolvencyOrderModel
            {
                InsolvencyOrderId = 5,
                ResidenceId = 15,
                OrderDate = new DateTime(2014, 03, 06),
                Disputes = new List<DisputeModel>
                {
                    new DisputeModel
                    {
                        DisputeId = 6,
                        InsolvencyOrderId = 5,
                        ReferenceNumber = "6",
                        DateRaised = new DateTime(2013, 07, 06),
                        Displayed = true
                    }
                }
            },
            new InsolvencyOrderModel
            {
                InsolvencyOrderId = 6,
                ResidenceId = 17,
                OrderDate = new DateTime(2018, 03, 06),
                Disputes = new List<DisputeModel> { }
            },
            new InsolvencyOrderModel
            {
                InsolvencyOrderId = 7,
                ResidenceId = 18,
                OrderDate = new DateTime(2017, 03, 06),
                Disputes = new List<DisputeModel> { }
            },
            new InsolvencyOrderModel
            {
                InsolvencyOrderId = 8,
                ResidenceId = 19,
                OrderDate = new DateTime(2016, 03, 06),
                Disputes = new List<DisputeModel> { }
            },
            new InsolvencyOrderModel
            {
                InsolvencyOrderId = 9,
                ResidenceId = 20,
                OrderDate = new DateTime(2018, 03, 06),
                Disputes = new List<DisputeModel>
                {
                }
            },
            new InsolvencyOrderModel
            {
                InsolvencyOrderId = 10,
                ResidenceId = 21,
                OrderDate = new DateTime(2017, 03, 06),
                Disputes = new List<DisputeModel>
                {
                    new DisputeModel
                    {
                        DisputeId = 11,
                        InsolvencyOrderId = 11,
                        ReferenceNumber = "11",
                        DateRaised = new DateTime(2017, 12, 06),
                        Displayed = true
                    }
                }
            },
            new InsolvencyOrderModel
            {
                InsolvencyOrderId = 11,
                ResidenceId = 22,
                OrderDate = new DateTime(2016, 03, 06),
                Disputes = new List<DisputeModel> { }
            },
            new InsolvencyOrderModel
            {
                InsolvencyOrderId = 12,
                ResidenceId = 212,
                OrderDate = new DateTime(2016, 03, 06),
                Disputes = new List<DisputeModel> { }
            },
            new InsolvencyOrderModel
            {
                InsolvencyOrderId = 13,
                ResidenceId = 122,
                OrderDate = new DateTime(2015, 03, 06),
                Disputes = new List<DisputeModel> { }
            },
            new InsolvencyOrderModel
            {
                InsolvencyOrderId = 14,
                ResidenceId = 32,
                OrderDate = new DateTime(2017, 03, 06),
                Disputes = new List<DisputeModel> { }
            }
        };
    }
}
