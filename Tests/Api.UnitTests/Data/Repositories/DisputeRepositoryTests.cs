// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 04-20-2018
//
// Last Modified By : MartinG
// Last Modified On : 05-18-2018
// ***********************************************************************
// <copyright file="DisputeRepositoryTests.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>DisputeRepositoryTests</summary>
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
using Callcredit.Domain.Insolvencies.Filters.Dispute;
using Callcredit.Domain.Repositories;
using Callcredit.Domain.Repositories.GDPR;
using Callcredit.FirstInFirstOutFiltering;
using Callcredit.TestHelpers;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Api.UnitTests.Data.Repositories
{
    /// <summary>
    /// Class DisputeRepositoryTests.
    /// </summary>
    [TestClass]
    public class DisputeRepositoryTests
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
        /// Constructings the dispute repository with null database context throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingDisputeRepository_WithNullDatabaseContext_ThrowsArgumentNullException()
        {
            // Arrange
            const DatabaseContext context = null;
            var disputesFilteredBaseData = new Mock<IFilteredBaseData<IDisputeFilterBase>>();

            // Act
            void ConstructDisputeRepository() =>
                new DisputesRepository(context, disputesFilteredBaseData.Object, mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)ConstructDisputeRepository);
        }

        /// <summary>
        /// Constructings the dispute repository with null database context has correct parameter name in exception.
        /// </summary>
        [TestMethod]
        public void ConstructingDisputeRepository_WithNullDatabaseContext_HasCorrectParameterNameInException()
        {
            // Arrange
            const string expectedParameterName = "context";
            const DatabaseContext context = null;
            var disputesFilteredBaseData = new Mock<IFilteredBaseData<IDisputeFilterBase>>();

            // Act
            void ConstructDisputeRepository() =>
                new DisputesRepository(context, disputesFilteredBaseData.Object, mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(ConstructDisputeRepository, expectedParameterName);
        }

        /// <summary>
        /// Constructings the dispute repository with null filtered base data throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingDisputeRepository_WithNullFilteredBaseData_ThrowsArgumentNullException()
        {
            // Arrange
            const IFilteredBaseData<IDisputeFilterBase> filteredBaseData = null;
            var context = new TestDbContext();

            // Act
            void ConstructDisputeRepository() => new DisputesRepository(context, filteredBaseData, mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)ConstructDisputeRepository);
        }

        /// <summary>
        /// Constructings the dispute repository with null filtered base data has correct parameter name in exception.
        /// </summary>
        [TestMethod]
        public void ConstructingDisputeRepository_WithNullFilteredBaseData_HasCorrectParameterNameInException()
        {
            // Arrange
            const string expectedParameterName = "filteredBaseData";
            const IFilteredBaseData<IDisputeFilterBase> filteredBaseData = null;
            var context = new TestDbContext();

            // Act
            void ConstructDisputeRepository() => new DisputesRepository(context, filteredBaseData, mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(ConstructDisputeRepository, expectedParameterName);
        }

        /// <summary>
        /// Countings all records with seeded dispute records returns correct count.
        /// </summary>
        /// <param name="records">The records.</param>
        /// <returns>Task.</returns>
        [TestMethod]
        [DataRow(1, DisplayName = "With 1 seeded record.")]
        [DataRow(5, DisplayName = "With 5 seeded records.")]
        [DataRow(10, DisplayName = "With 10 seeded records.")]
        public async Task CountingAllRecords_WithSeededDisputeRecords_ReturnsCorrectCount(int records)
        {
            // Arrange
            var disputeRecords = DisputesDataCreationFixture.CreateMany(records);
            var context = TestDbContext.CreateContextWithSeededData(disputeRecords);

            var operationDateProvider = new Mock<IOperationDateProvider>();
            operationDateProvider.Setup(x => x.GetOperationDate()).Returns(DateTime.Now);

            IOptions<RetentionOptions> retentionOptions =
                new ConfigurationOption(
                    new RetentionOptions()
                    {
                        RetentionPeriod = 10,
                        CutOffPeriod = 6
                    });

            var disputesFilteredBaseData = new DisputeFilterContext(retentionOptions, operationDateProvider.Object);
            var disputeRepository = new DisputesRepository(context, disputesFilteredBaseData, mockTelemetryClient.Object);

            // Act
            var actualCount = await disputeRepository.CountAsync();

            // Assert
            Assert.AreEqual(records, actualCount);
        }

        /// <summary>
        /// Countings the records by identifier with seeded dispute records returns correct count.
        /// </summary>
        /// <returns>Task.</returns>
        [TestMethod]
        public async Task CountingRecordsById_WithSeededDisputeRecords_ReturnsCorrectCount()
        {
            // Arrange
            const int recordId = 1;
            const int expectedCount = 1;
            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var disputeRecords = DisputeTestData.CreateDisputeActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords).Seed(disputeRecords);

            var operationDateProvider = new Mock<IOperationDateProvider>();
            operationDateProvider.Setup(x => x.GetOperationDate()).Returns(DateTime.Now);

            IOptions<RetentionOptions> retentionOptions =
                new ConfigurationOption(
                    new RetentionOptions()
                    {
                        RetentionPeriod = 10,
                        CutOffPeriod = 6
                    });

            var disputesFilteredBaseData = new DisputeFilterContext(retentionOptions, operationDateProvider.Object);

            var disputeRepository = new DisputesRepository(context, disputesFilteredBaseData, mockTelemetryClient.Object);

            // Act
            var actualCount = await disputeRepository.CountByAsync(recordId, record => record.DisputeId);

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Gettings the record by identifier with seeded dispute records returns mapped model.
        /// </summary>
        /// <returns>Task.</returns>
        [TestMethod]
        public async Task GettingRecordById_WithSeededDisputeRecords_ReturnsMappedModel()
        {
            // Arrange
            const int recordId = 1;

            var expectedRecord = DisputeTestData.GetDisputeById(recordId);
            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var disputeRecords = DisputeTestData.CreateDisputeActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords).Seed(disputeRecords);

            var operationDateProvider = new Mock<IOperationDateProvider>();
            operationDateProvider.Setup(x => x.GetOperationDate()).Returns(DateTime.Now);

            IOptions<RetentionOptions> retentionOptions =
                new ConfigurationOption(
                    new RetentionOptions()
                    {
                        RetentionPeriod = 10,
                        CutOffPeriod = 6
                    });

            var disputesFilteredBaseData = new DisputeFilterContext(retentionOptions, operationDateProvider.Object);

            var disputeRepository = new DisputesRepository(context, disputesFilteredBaseData, mockTelemetryClient.Object);

            // Act
            var actualRecord = await disputeRepository.GetResultByAsync(recordId, record => record.DisputeId);

            // Assert
            Assert.AreEqual(expectedRecord.InsolvencyOrderId, actualRecord.InsolvencyOrderId);
            Assert.AreEqual(expectedRecord.DateRaised, actualRecord.DateRaised);
            Assert.AreEqual(expectedRecord.Displayed, actualRecord.Displayed);
            Assert.AreEqual(expectedRecord.DisputeId, actualRecord.DisputeId);
            Assert.AreEqual(expectedRecord.ReferenceNumber, actualRecord.ReferenceNumber);
        }

        /// <summary>
        /// Gettings all records with seeded dispute records returns mapped models.
        /// </summary>
        /// <returns>Task.</returns>
        [TestMethod]
        public async Task GettingAllRecords_WithSeededDisputeRecords_ReturnsMappedModels()
        {
            // Arrange
            var pageInformation = new PageInformation(1, 100);
            var expectedRecords = DisputeTestData.CreateDisputeExpecteds();
            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var disputeRecords = DisputeTestData.CreateDisputeActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords).Seed(disputeRecords);

            var operationDateProvider = new Mock<IOperationDateProvider>();
            operationDateProvider.Setup(x => x.GetOperationDate()).Returns(DateTime.Now);

            IOptions<RetentionOptions> retentionOptions =
                new ConfigurationOption(
                    new RetentionOptions()
                    {
                        RetentionPeriod = 10,
                        CutOffPeriod = 6
                    });

            var disputesFilteredBaseData = new DisputeFilterContext(retentionOptions, operationDateProvider.Object);

            var disputeRepository = new DisputesRepository(context, disputesFilteredBaseData, mockTelemetryClient.Object);

            // Act
            var actualRecords = await disputeRepository.GetAllAsync(pageInformation);

            // Assert
            CollectionAssert.AreEqual(
                expectedRecords.OrderBy(x => x.DisputeId).ToList(),
                actualRecords.OrderBy(x => x.DisputeId).ToList(),
                new DisputeModelComparer());
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
        [DataRow(2, 10, DisplayName = "With second page and page size of 10.")]
        [DataRow(2, 100, DisplayName = "With second page and page size of 100.")]
        public async Task GettingAllRecords_WithPageInformation_ReturnsMappedModels(
            int currentPage,
            int pageSize)
        {
            // Arrange
            var pageInformation = new PageInformation(currentPage, pageSize);
            var expectedRecords = DisputeTestData.CreateDisputeExpecteds(pageInformation);
            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var disputeRecords = DisputeTestData.CreateDisputeActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords).Seed(disputeRecords);

            var operationDateProvider = new Mock<IOperationDateProvider>();
            operationDateProvider.Setup(x => x.GetOperationDate()).Returns(DateTime.Now);

            IOptions<RetentionOptions> retentionOptions =
                new ConfigurationOption(
                    new RetentionOptions()
                    {
                        RetentionPeriod = 10,
                        CutOffPeriod = 6
                    });

            var disputesFilteredBaseData = new DisputeFilterContext(retentionOptions, operationDateProvider.Object);

            var disputeRepository = new DisputesRepository(context, disputesFilteredBaseData, mockTelemetryClient.Object);

            // Act
            var actualRecords = await disputeRepository.GetAllAsync(pageInformation);

            // Assert
            CollectionAssert.AreEqual(
                expectedRecords.OrderBy(x => x.DisputeId).ToList(),
                actualRecords.OrderBy(x => x.DisputeId).ToList(),
                new DisputeModelComparer());
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
        [DataRow(1, 10, 5, DisplayName = "With first page and page size of 10.")]
        [DataRow(1, 100, 5, DisplayName = "With first page and page size of 100.")]
        [DataRow(2, 3, 2, DisplayName = "With second page and page size of 10.")]
        [DataRow(2, 100, 0, DisplayName = "With second page and page size of 100.")]
        public async Task GettingAllRecords_WithPageInformation_ReturnsExpectedNumberOfRecords(
            int currentPage,
            int pageSize,
            int expected)
        {
            // Arrange
            var pageInformation = new PageInformation(currentPage, pageSize);
            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var disputeRecords = DisputeTestData.CreateDisputeActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords).Seed(disputeRecords);

            var operationDateProvider = new Mock<IOperationDateProvider>();
            operationDateProvider.Setup(x => x.GetOperationDate()).Returns(DateTime.Now);

            IOptions<RetentionOptions> retentionOptions =
                new ConfigurationOption(
                    new RetentionOptions()
                    {
                        RetentionPeriod = 10,
                        CutOffPeriod = 6
                    });

            var disputesFilteredBaseData = new DisputeFilterContext(retentionOptions, operationDateProvider.Object);

            var disputeRepository = new DisputesRepository(context, disputesFilteredBaseData, mockTelemetryClient.Object);

            // Act
            var actualCount = (await disputeRepository.GetAllAsync(pageInformation)).Count();

            // Assert
            Assert.AreEqual(expected, actualCount);
        }

        /// <summary>
        /// Gettings the records by insolvency order identifier with seeded dispute records returns mapped models.
        /// </summary>
        /// <param name="insolvencyOrderId">The insolvency order identifier.</param>
        /// <returns>Task.</returns>
        [TestMethod]
        [DataRow(5, DisplayName = "With insolvency order id of 5.")]
        [DataRow(11, DisplayName = "With insolvency order id of 11.")]
        public async Task GettingRecordsByInsolvencyOrderId_WithSeededDisputeRecords_ReturnsMappedModels(
            int insolvencyOrderId)
        {
            // Arrange
            var pageInformation = new PageInformation(1, 100);
            var expecteds = DisputeTestData.GetExpectedsByInsolvencyOrderId(insolvencyOrderId).ToList();
            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var disputeRecords = DisputeTestData.CreateDisputeActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords).Seed(disputeRecords);

            var operationDateProvider = new Mock<IOperationDateProvider>();
            operationDateProvider.Setup(x => x.GetOperationDate()).Returns(DateTime.Now);

            IOptions<RetentionOptions> retentionOptions =
                new ConfigurationOption(
                    new RetentionOptions()
                    {
                        RetentionPeriod = 10,
                        CutOffPeriod = 6
                    });

            var disputesFilteredBaseData = new DisputeFilterContext(retentionOptions, operationDateProvider.Object);

            var disputeRepository = new DisputesRepository(context, disputesFilteredBaseData, mockTelemetryClient.Object);

            // Act
            var actuals =
                (await disputeRepository.GetResultsByAsync(
                    insolvencyOrderId,
                    record => record.InsolvencyOrderDisputes.FirstOrDefault().InsolvencyOrderId,
                    pageInformation)).ToList();

            // Assert
            CollectionAssert.AreEqual(
                expecteds.OrderBy(x => x.DisputeId).ToList(),
                actuals.OrderBy(x => x.DisputeId).ToList(),
                new DisputeModelComparer());
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
            var expecteds = DisputeTestData.GetExpectedsByInsolvencyOrderId(pageInformation, insolvencyOrderId);
            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var disputeRecords = DisputeTestData.CreateDisputeActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords).Seed(disputeRecords);

            var operationDateProvider = new Mock<IOperationDateProvider>();
            operationDateProvider.Setup(x => x.GetOperationDate()).Returns(DateTime.Now);

            IOptions<RetentionOptions> retentionOptions =
                new ConfigurationOption(
                    new RetentionOptions()
                    {
                        RetentionPeriod = 10,
                        CutOffPeriod = 6
                    });

            var disputesFilteredBaseData = new DisputeFilterContext(retentionOptions, operationDateProvider.Object);

            var disputeRepository = new DisputesRepository(context, disputesFilteredBaseData, mockTelemetryClient.Object);

            // Act
            var actuals =
                await disputeRepository.GetResultsByAsync(
                    insolvencyOrderId,
                    record => record.InsolvencyOrderDisputes.FirstOrDefault().InsolvencyOrderId,
                    pageInformation);

            // Assert
            CollectionAssert.AreEqual(
                expecteds.OrderBy(x => x.DisputeId).ToList(),
                actuals.OrderBy(x => x.DisputeId).ToList(),
                new DisputeModelComparer());
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
        [DataRow(1, 10, 5, 2, DisplayName = "With insolvency order id of 5, page 1 and pagesize of 10.")]
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
            var disputeRecords = DisputeTestData.CreateDisputeActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords).Seed(disputeRecords);

            var operationDateProvider = new Mock<IOperationDateProvider>();
            operationDateProvider.Setup(x => x.GetOperationDate()).Returns(DateTime.Now);

            IOptions<RetentionOptions> retentionOptions =
                new ConfigurationOption(
                    new RetentionOptions()
                    {
                        RetentionPeriod = 10,
                        CutOffPeriod = 6
                    });

            var disputesFilteredBaseData = new DisputeFilterContext(retentionOptions, operationDateProvider.Object);

            var disputeRepository = new DisputesRepository(context, disputesFilteredBaseData, mockTelemetryClient.Object);

            // Act
            var actualCount =
                (await disputeRepository.GetResultsByAsync(
                    insolvencyOrderId,
                    record => record.InsolvencyOrderDisputes.FirstOrDefault().InsolvencyOrderId,
                    pageInformation))
                .Count();

            // Assert
            Assert.AreEqual(expected, actualCount);
        }

        /// <summary>
        /// Usings the get all asynchronous with displayed filter returns expected records.
        /// </summary>
        /// <returns>Task.</returns>
        [TestMethod]
        public async Task UsingGetAllAsync_WithDisplayedFilter_ReturnsExpectedRecords()
        {
            // Arrange
            var pageInformation = new PageInformation(1, 100);
            var expectedRecords = DisputeTestData.CreateDisputeExpecteds().Where(dispute => dispute.Displayed.Value);
            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var disputeRecords = DisputeTestData.CreateDisputeActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords).Seed(disputeRecords);

            var operationDateProvider = new Mock<IOperationDateProvider>();
            operationDateProvider.Setup(x => x.GetOperationDate()).Returns(DateTime.Now);

            IOptions<RetentionOptions> retentionOptions =
                new ConfigurationOption(
                    new RetentionOptions()
                    {
                        RetentionPeriod = 10,
                        CutOffPeriod = 6
                    });

            var disputesFilteredBaseData = new DisputeFilterContext(retentionOptions, operationDateProvider.Object);

            var disputeRepository = new DisputesRepository(context, disputesFilteredBaseData, mockTelemetryClient.Object);

            // Act
            var actualRecords = await disputeRepository.GetAllAsync(pageInformation);

            // Assert
            CollectionAssert.AreEqual(
                expectedRecords.OrderBy(x => x.DisputeId).ToList(),
                actualRecords.OrderBy(x => x.DisputeId).ToList(),
                new DisputeModelComparer());
        }
    }
}
