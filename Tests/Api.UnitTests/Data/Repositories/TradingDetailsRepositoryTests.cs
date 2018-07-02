// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 04-20-2018
//
// Last Modified By : MartinG
// Last Modified On : 05-18-2018
// ***********************************************************************
// <copyright file="TradingDetailsRepositoryTests.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>TradingDetailsRepositoryTests</summary>
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
    /// Class TradingDetailsRepositoryTests.
    /// </summary>
    [TestClass]
    public class TradingDetailsRepositoryTests
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
        /// Constructings the trading details repository with null database context throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingTradingDetailsRepository_WithNullDatabaseContext_ThrowsArgumentNullException()
        {
            // Arrange
            const DatabaseContext context = null;

            // Act
            void ConstructTradingDetailsRepository() => new InsolvencyOrderTradingDetailsRepository(context, mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)ConstructTradingDetailsRepository);
        }

        /// <summary>
        /// Constructings the trading details repository with null database context has correct parameter name in exception.
        /// </summary>
        [TestMethod]
        public void ConstructingTradingDetailsRepository_WithNullDatabaseContext_HasCorrectParameterNameInException()
        {
            // Arrange
            const string expectedParameterName = "context";
            const DatabaseContext context = null;

            // Act
            void ConstructTradingDetailsRepository() => new InsolvencyOrderTradingDetailsRepository(context, mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(ConstructTradingDetailsRepository, expectedParameterName);
        }

        /// <summary>
        /// Countings all records with seeded trading details records returns correct count.
        /// </summary>
        /// <param name="records">The records.</param>
        /// <returns>Task.</returns>
        [TestMethod]
        [DataRow(1, DisplayName = "With 1 seeded record.")]
        [DataRow(5, DisplayName = "With 5 seeded records.")]
        [DataRow(10, DisplayName = "With 10 seeded records.")]
        public async Task CountingAllRecords_WithSeededTradingDetailsRecords_ReturnsCorrectCount(int records)
        {
            // Arrange
            var tradingDetailsRecords = TradingDetailsDataCreationFixture.CreateMany(records);
            var context = TestDbContext.CreateContextWithSeededData(tradingDetailsRecords);
            var tradingDetailsRepository = new InsolvencyOrderTradingDetailsRepository(context, mockTelemetryClient.Object);

            // Act
            var actualCount = await tradingDetailsRepository.CountAsync();

            // Assert
            Assert.AreEqual(records, actualCount);
        }

        /// <summary>
        /// Countings the records by identifier with seeded trading details records returns correct count.
        /// </summary>
        /// <returns>Task.</returns>
        [TestMethod]
        public async Task CountingRecordsById_WithSeededTradingDetailsRecords_ReturnsCorrectCount()
        {
            // Arrange
            const int recordId = 1;
            const int expectedCount = 1;

            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var tradingDetailsRecords = TradingDetailsTestData.CreateTradingDetailsActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords).Seed(tradingDetailsRecords);
            var tradingDetailsRepository = new InsolvencyOrderTradingDetailsRepository(context, mockTelemetryClient.Object);

            // Act
            var actualCount = await tradingDetailsRepository.CountByAsync(recordId, record => record.InsolvencyTradingId);

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Gettings the record by identifier with seeded trading details records returns mapped model.
        /// </summary>
        /// <returns>Task.</returns>
        [TestMethod]
        public async Task GettingRecordById_WithSeededTradingDetailsRecords_ReturnsMappedModel()
        {
            // Arrange
            const int recordId = 1;

            var expectedRecord = TradingDetailsTestData.GetTradingDetailsById(recordId);
            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var tradingDetailsRecords = TradingDetailsTestData.CreateTradingDetailsActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords).Seed(tradingDetailsRecords);

            var tradingDetailsRepository = new InsolvencyOrderTradingDetailsRepository(context, mockTelemetryClient.Object);

            // Act
            var actualRecord = await tradingDetailsRepository.GetResultByAsync(recordId, record => record.InsolvencyTradingId);

            // Assert
            Assert.AreEqual(expectedRecord.InsolvencyOrderId, actualRecord.InsolvencyOrderId);
            Assert.AreEqual(expectedRecord.Name, actualRecord.Name);
            Assert.AreEqual(expectedRecord.InsolvencyOrderTradingDetailsId, actualRecord.InsolvencyOrderTradingDetailsId);
            Assert.AreEqual(expectedRecord.Address, actualRecord.Address);
        }

        /// <summary>
        /// Gettings all records with seeded trading details records returns mapped models.
        /// </summary>
        /// <returns>Task.</returns>
        [TestMethod]
        public async Task GettingAllRecords_WithSeededTradingDetailsRecords_ReturnsMappedModels()
        {
            // Arrange
            var pageInformation = new PageInformation(1, 100);
            var expectedRecords = TradingDetailsTestData.CreateTradingDetailsExpecteds();
            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var tradingDetailsRecords = TradingDetailsTestData.CreateTradingDetailsActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords).Seed(tradingDetailsRecords);

            var tradingDetailsRepository = new InsolvencyOrderTradingDetailsRepository(context, mockTelemetryClient.Object);

            // Act
            var actualRecords = await tradingDetailsRepository.GetAllAsync(pageInformation);

            // Assert
            CollectionAssert.AreEqual(
                expectedRecords.OrderBy(x => x.InsolvencyOrderTradingDetailsId).ToList(),
                actualRecords.OrderBy(x => x.InsolvencyOrderTradingDetailsId).ToList(),
                new InsolvencyTradingDetailsModelComparer());
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
            var expectedRecords = TradingDetailsTestData.CreateTradingDetailsExpecteds(pageInformation);
            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var tradingDetailsRecords = TradingDetailsTestData.CreateTradingDetailsActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords).Seed(tradingDetailsRecords);

            var tradingDetailsRepository = new InsolvencyOrderTradingDetailsRepository(context, mockTelemetryClient.Object);

            // Act
            var actualRecords = await tradingDetailsRepository.GetAllAsync(pageInformation);

            // Assert
            CollectionAssert.AreEqual(
                expectedRecords.OrderBy(x => x.InsolvencyOrderTradingDetailsId).ToList(),
                actualRecords.OrderBy(x => x.InsolvencyOrderTradingDetailsId).ToList(),
                new InsolvencyTradingDetailsModelComparer());
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
            var tradingDetailsRecords = TradingDetailsTestData.CreateTradingDetailsActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords).Seed(tradingDetailsRecords);

            var tradingDetailsRepository = new InsolvencyOrderTradingDetailsRepository(context, mockTelemetryClient.Object);

            // Act
            var actualCount = (await tradingDetailsRepository.GetAllAsync(pageInformation)).Count();

            // Assert
            Assert.AreEqual(expected, actualCount);
        }

        /// <summary>
        /// Gettings the records by insolvency order identifier with seeded trading details records returns mapped models.
        /// </summary>
        /// <param name="insolvencyOrderId">The insolvency order identifier.</param>
        /// <returns>Task.</returns>
        [TestMethod]
        [DataRow(5, DisplayName = "With insolvency order id of 5.")]
        [DataRow(11, DisplayName = "With insolvency order id of 11.")]
        public async Task GettingRecordsByInsolvencyOrderId_WithSeededTradingDetailsRecords_ReturnsMappedModels(
            int insolvencyOrderId)
        {
            // Arrange
            var pageInformation = new PageInformation(1, 100);
            var expecteds = TradingDetailsTestData.GetExpectedsByInsolvencyOrderId(insolvencyOrderId).ToList();
            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var tradingDetailsRecords = TradingDetailsTestData.CreateTradingDetailsActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords).Seed(tradingDetailsRecords);

            var tradingDetailsRepository = new InsolvencyOrderTradingDetailsRepository(context, mockTelemetryClient.Object);

            // Act
            var actuals =
                (await tradingDetailsRepository.GetResultsByAsync(
                    insolvencyOrderId,
                    record => record.InsolvencyOrderEntity.InsolvencyOrderId,
                    pageInformation)).ToList();

            // Assert
            CollectionAssert.AreEqual(
                expecteds.OrderBy(x => x.InsolvencyOrderTradingDetailsId).ToList(),
                actuals.OrderBy(x => x.InsolvencyOrderTradingDetailsId).ToList(),
                new InsolvencyTradingDetailsModelComparer());
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
            var expecteds = TradingDetailsTestData.GetExpectedsByInsolvencyOrderId(pageInformation, insolvencyOrderId);
            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var tradingDetailsRecords = TradingDetailsTestData.CreateTradingDetailsActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords).Seed(tradingDetailsRecords);

            var tradingDetailsRepository = new InsolvencyOrderTradingDetailsRepository(context, mockTelemetryClient.Object);

            // Act
            var actuals =
                await tradingDetailsRepository.GetResultsByAsync(
                    insolvencyOrderId,
                    record => record.InsolvencyOrderEntity.InsolvencyOrderId,
                    pageInformation);

            // Assert
            CollectionAssert.AreEqual(
                expecteds.OrderBy(x => x.InsolvencyOrderTradingDetailsId).ToList(),
                actuals.OrderBy(x => x.InsolvencyOrderTradingDetailsId).ToList(),
                new InsolvencyTradingDetailsModelComparer());
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
            var tradingDetailsRecords = TradingDetailsTestData.CreateTradingDetailsActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords).Seed(tradingDetailsRecords);

            var tradingDetailsRepository = new InsolvencyOrderTradingDetailsRepository(context, mockTelemetryClient.Object);

            // Act
            var actualCount =
                (await tradingDetailsRepository.GetResultsByAsync(
                    insolvencyOrderId,
                    record => record.InsolvencyOrderEntity.InsolvencyOrderId,
                    pageInformation))
                .Count();

            // Assert
            Assert.AreEqual(expected, actualCount);
        }
    }
}
