// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 04-20-2018
//
// Last Modified By : MartinG
// Last Modified On : 05-18-2018
// ***********************************************************************
// <copyright file="HistoriesRepositoryTests.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>HistoriesRepositoryTests</summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Threading.Tasks;
using Api.EntityFramework;
using Api.EntityFramework.Configuration;
using Api.EntityFramework.Repositories;
using Api.Telemetry;
using Api.UnitTests.Comparers;
using Api.UnitTests.TestData;
using Api.UnitTests.TestFixtures;
using Callcredit.Domain.Repositories;
using Callcredit.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Api.UnitTests.Data.Repositories
{
    /// <summary>
    /// Class HistoriesRepositoryTests.
    /// </summary>
    [TestClass]
    public class HistoriesRepositoryTests
    {
        /// <summary>
        /// The mock telemetry client
        /// </summary>
        private Mock<ITelemetryClient> mockTelemetryClient;

        /// <summary>
        /// Configurations the mapper initializer.
        /// </summary>
        /// <param name="context">The context.</param>
        [ClassInitialize]
        public static void ConfigurationMapperInitializer(TestContext context)
        {
            AutoMapper.Mapper.Reset();
            ModelMappingConfiguration.CreateModelMapping();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            mockTelemetryClient = new Mock<ITelemetryClient>();
        }

        /// <summary>
        /// Constructings the histories repository with null database context throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingHistoriesRepository_WithNullDatabaseContext_ThrowsArgumentNullException()
        {
            // Arrange
            const DatabaseContext context = null;

            // Act
            void ConstructHistoriesRepository() => new InsolvencyOrderHistoriesRepository(context, mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)ConstructHistoriesRepository);
        }

        /// <summary>
        /// Constructings the histories repository with null database context has correct parameter case reference in exception.
        /// </summary>
        [TestMethod]
        public void ConstructingHistoriesRepository_WithNullDatabaseContext_HasCorrectParameterCaseReferenceInException()
        {
            // Arrange
            const string expectedParameterCaseReference = "context";
            const DatabaseContext context = null;

            // Act
            void ConstructHistoriesRepository() => new InsolvencyOrderHistoriesRepository(context, mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(ConstructHistoriesRepository, expectedParameterCaseReference);
        }

        /// <summary>
        /// Countings all records with seeded histories records returns correct count.
        /// </summary>
        /// <param name="records">The records.</param>
        /// <returns>Task.</returns>
        [TestMethod]
        [DataRow(1, DisplayName = "With 1 seeded record.")]
        [DataRow(5, DisplayName = "With 5 seeded records.")]
        [DataRow(10, DisplayName = "With 10 seeded records.")]
        public async Task CountingAllRecords_WithSeededHistoriesRecords_ReturnsCorrectCount(int records)
        {
            // Arrange
            var historyRecords = HistoriesDataCreationFixture.CreateMany(records);
            var context = TestDbContext.CreateContextWithSeededData(historyRecords);
            var historiesRepository = new InsolvencyOrderHistoriesRepository(context, mockTelemetryClient.Object);

            // Act
            var actualCount = await historiesRepository.CountAsync();

            // Assert
            Assert.AreEqual(records, actualCount);
        }

        /// <summary>
        /// Countings the records by identifier with seeded histories records returns correct count.
        /// </summary>
        /// <returns>Task.</returns>
        [TestMethod]
        public async Task CountingRecordsById_WithSeededHistoriesRecords_ReturnsCorrectCount()
        {
            // Arrange
            const int recordId = 1;
            const int expectedCount = 1;

            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var historyRecords = HistoriesTestData.CreateHistoriesActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords).Seed(historyRecords);
            var historiesRepository = new InsolvencyOrderHistoriesRepository(context, mockTelemetryClient.Object);

            // Act
            var actualCount = await historiesRepository.CountByAsync(recordId, record => record.InsolvencyOrderHistoryId);

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Gettings the record by identifier with seeded histories records returns mapped model.
        /// </summary>
        /// <returns>Task.</returns>
        [TestMethod]
        public async Task GettingRecordById_WithSeededHistoriesRecords_ReturnsMappedModel()
        {
            // Arrange
            const int recordId = 1;

            var expectedRecord = HistoriesTestData.GetHistoriesById(recordId);
            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var historyRecords = HistoriesTestData.CreateHistoriesActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords).Seed(historyRecords);

            var historiesRepository = new InsolvencyOrderHistoriesRepository(context, mockTelemetryClient.Object);

            // Act
            var actualRecord = await historiesRepository.GetResultByAsync(recordId, record => record.InsolvencyOrderHistoryId);

            // Assert
            Assert.AreEqual(expectedRecord.InsolvencyOrderId, actualRecord.InsolvencyOrderId);
            Assert.AreEqual(expectedRecord.CaseReference, actualRecord.CaseReference);
            Assert.AreEqual(expectedRecord.InsolvencyOrderHistoryId, actualRecord.InsolvencyOrderHistoryId);
            Assert.AreEqual(expectedRecord.CaseYear, actualRecord.CaseYear);
            Assert.AreEqual(expectedRecord.CourtId, actualRecord.CourtId);
            Assert.AreEqual(expectedRecord.InsolvencyOrderStatusId, actualRecord.InsolvencyOrderStatusId);
        }

        /// <summary>
        /// Gettings all records with seeded histories records returns mapped models.
        /// </summary>
        /// <returns>Task.</returns>
        [TestMethod]
        public async Task GettingAllRecords_WithSeededHistoriesRecords_ReturnsMappedModels()
        {
            // Arrange
            var pageInformation = new PageInformation(1, 100);
            var expectedRecords = HistoriesTestData.CreateHistoriesExpecteds();
            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var historyRecords = HistoriesTestData.CreateHistoriesActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords).Seed(historyRecords);

            var historiesRepository = new InsolvencyOrderHistoriesRepository(context, mockTelemetryClient.Object);

            // Act
            var actualRecords = await historiesRepository.GetAllAsync(pageInformation);

            // Assert
            CollectionAssert.AreEqual(
                expectedRecords.OrderBy(x => x.InsolvencyOrderHistoryId).ToList(),
                actualRecords.OrderBy(x => x.InsolvencyOrderHistoryId).ToList(),
                new InsolvencyHistoryModelComparer());
        }

        /// <summary>
        /// Gettings all records with page information returns mapped models.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Task.</returns>
        [TestMethod]
        [DataRow(1, 2, DisplayName = "With first page and page size of 2.")]
        [DataRow(1, 10, DisplayName = "With first page and page size of 10.")]
        [DataRow(1, 100, DisplayName = "With first page and page size of 100.")]
        [DataRow(2, 3, DisplayName = "With second page and page size of 10.")]
        [DataRow(2, 10, DisplayName = "With second page and page size of 100.")]
        public async Task GettingAllRecords_WithPageInformation_ReturnsMappedModels(
            int currentPage,
            int pageSize)
        {
            // Arrange
            var pageInformation = new PageInformation(currentPage, pageSize);
            var expectedRecords = HistoriesTestData.CreateHistoriesExpecteds(pageInformation);
            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var historyRecords = HistoriesTestData.CreateHistoriesActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords).Seed(historyRecords);

            var historiesRepository = new InsolvencyOrderHistoriesRepository(context, mockTelemetryClient.Object);

            // Act
            var actualRecords = await historiesRepository.GetAllAsync(pageInformation);

            // Assert
            CollectionAssert.AreEqual(
                expectedRecords.OrderBy(x => x.InsolvencyOrderHistoryId).ToList(),
                actualRecords.OrderBy(x => x.InsolvencyOrderHistoryId).ToList(),
                new InsolvencyHistoryModelComparer());
        }

        /// <summary>
        /// Gettings all records with page information returns expected number of records.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="expected">The expected.</param>
        /// <returns>Task.</returns>
        [TestMethod]
        [DataRow(1, 2, 2, DisplayName = "With first page and page size of 2.")]
        [DataRow(1, 10, 10, DisplayName = "With first page and page size of 10.")]
        [DataRow(1, 100, 11, DisplayName = "With first page and page size of 100.")]
        [DataRow(4, 3, 2, DisplayName = "With second page and page size of 10.")]
        [DataRow(2, 100, 0, DisplayName = "With second page and page size of 100.")]
        public async Task GettingAllRecords_WithPageInformation_ReturnsExpectedNumberOfRecords(
            int currentPage,
            int pageSize,
            int expected)
        {
            // Arrange
            var pageInformation = new PageInformation(currentPage, pageSize);
            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var historyRecords = HistoriesTestData.CreateHistoriesActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords).Seed(historyRecords);

            var historiesRepository = new InsolvencyOrderHistoriesRepository(context, mockTelemetryClient.Object);

            // Act
            var actualCount = (await historiesRepository.GetAllAsync(pageInformation)).Count();

            // Assert
            Assert.AreEqual(expected, actualCount);
        }

        /// <summary>
        /// Gettings the records by insolvency order identifier with seeded histories records returns mapped models.
        /// </summary>
        /// <param name="insolvencyOrderId">The insolvency order identifier.</param>
        /// <returns>Task.</returns>
        [TestMethod]
        [DataRow(5, DisplayName = "With insolvency order id of 5.")]
        [DataRow(11, DisplayName = "With insolvency order id of 11.")]
        public async Task GettingRecordsByInsolvencyOrderId_WithSeededHistoriesRecords_ReturnsMappedModels(
            int insolvencyOrderId)
        {
            // Arrange
            var pageInformation = new PageInformation(1, 100);
            var expecteds = HistoriesTestData.GetExpectedsByInsolvencyOrderId(insolvencyOrderId).ToList();
            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var historyRecords = HistoriesTestData.CreateHistoriesActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords).Seed(historyRecords);

            var historiesRepository = new InsolvencyOrderHistoriesRepository(context, mockTelemetryClient.Object);

            // Act
            var actuals =
                (await historiesRepository.GetResultsByAsync(
                    insolvencyOrderId,
                    record => record.InsolvencyOrderId,
                    pageInformation)).ToList();

            // Assert
            CollectionAssert.AreEqual(
                expecteds.OrderBy(x => x.InsolvencyOrderHistoryId).ToList(),
                actuals.OrderBy(x => x.InsolvencyOrderHistoryId).ToList(),
                new InsolvencyHistoryModelComparer());
        }

        /// <summary>
        /// Gettings the records by insolvency order identifier with page information returns mapped models.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="insolvencyOrderId">The insolvency order identifier.</param>
        /// <returns>Task.</returns>
        [TestMethod]
        [DataRow(1, 10, 5, DisplayName = "With insolvency order id of 5.")]
        [DataRow(1, 10, 6, DisplayName = "With insolvency order id of 6.")]
        [DataRow(1, 100, 13, DisplayName = "With insolvency order id of 13.")]
        [DataRow(2, 10, 12, DisplayName = "With insolvency order id of 12.")]
        public async Task GettingRecordsByInsolvencyOrderId_WithPageInformation_ReturnsMappedModels(
            int currentPage,
            int pageSize,
            int insolvencyOrderId)
        {
            // Arrange
            var pageInformation = new PageInformation(currentPage, pageSize);
            var expecteds = HistoriesTestData.GetExpectedsByInsolvencyOrderId(pageInformation, insolvencyOrderId);
            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var historyRecords = HistoriesTestData.CreateHistoriesActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords).Seed(historyRecords);

            var historiesRepository = new InsolvencyOrderHistoriesRepository(context, mockTelemetryClient.Object);

            // Act
            var actuals =
                await historiesRepository.GetResultsByAsync(
                    insolvencyOrderId,
                    record => record.InsolvencyOrderId,
                    pageInformation);

            // Assert
            CollectionAssert.AreEqual(
                expecteds.OrderBy(x => x.InsolvencyOrderHistoryId).ToList(),
                actuals.OrderBy(x => x.InsolvencyOrderHistoryId).ToList(),
                new InsolvencyHistoryModelComparer());
        }

        /// <summary>
        /// Gettings the records by insolvency order identifier with page information returns expected number of records.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="insolvencyOrderId">The insolvency order identifier.</param>
        /// <param name="expected">The expected.</param>
        /// <returns>Task.</returns>
        [TestMethod]
        [DataRow(1, 10, 5, 3, DisplayName = "With insolvency order id of 5, page 1 and pagesize of 10.")]
        [DataRow(1, 5, 11, 1, DisplayName = "With insolvency order id of 6, page 1 and pagesize of 5.")]
        [DataRow(2, 10, 13, 0, DisplayName = "With insolvency order id of 13, page 2 and pagesize of 10.")]
        public async Task GettingRecordsByInsolvencyOrderId_WithPageInformation_ReturnsExpectedNumberOfRecords(
            int currentPage,
            int pageSize,
            int insolvencyOrderId,
            int expected)
        {
            // Arrange
            var pageInformation = new PageInformation(currentPage, pageSize);
            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var historyRecords = HistoriesTestData.CreateHistoriesActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords).Seed(historyRecords);

            var historiesRepository = new InsolvencyOrderHistoriesRepository(context, mockTelemetryClient.Object);

            // Act
            var actualCount =
                (await historiesRepository.GetResultsByAsync(
                    insolvencyOrderId,
                    record => record.InsolvencyOrderId,
                    pageInformation))
                .Count();

            // Assert
            Assert.AreEqual(expected, actualCount);
        }
    }
}
