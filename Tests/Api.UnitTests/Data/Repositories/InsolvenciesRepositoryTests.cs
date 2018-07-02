// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 04-20-2018
//
// Last Modified By : MartinG
// Last Modified On : 05-18-2018
// ***********************************************************************
// <copyright file="InsolvenciesRepositoryTests.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>InsolvenciesRepositoryTests</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
using Callcredit.Domain.Insolvencies.Filters.InsolvencyOrder;
using Callcredit.Domain.Insolvencies.Models;
using Callcredit.Domain.Insolvencies.Resources;
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
    /// Class InsolvenciesRepositoryTests.
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class InsolvenciesRepositoryTests
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
        /// Constructings the insolvencies repository with null database context throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingInsolvenciesRepository_WithNullDatabaseContext_ThrowsArgumentNullException()
        {
            // Arrange
            const DatabaseContext context = null;
            var insolvencyOrdersFilteredBaseData = new Mock<IFilteredBaseData<IInsolvencyOrderFilterBase>>();
            var disputesFilteredBaseData = new Mock<IFilteredBaseData<IDisputeFilterBase>>();

            // Act
            void ConstructInsolvenciesRepository() =>
                new InsolvencyOrdersRepository(
                    context,
                    insolvencyOrdersFilteredBaseData.Object,
                    disputesFilteredBaseData.Object,
                    mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)ConstructInsolvenciesRepository);
        }

        /// <summary>
        /// Constructings the insolvencies repository with null database context has correct parameter name in exception.
        /// </summary>
        [TestMethod]
        public void ConstructingInsolvenciesRepository_WithNullDatabaseContext_HasCorrectParameterNameInException()
        {
            // Arrange
            const string expectedParameterName = "context";
            const DatabaseContext context = null;
            var insolvencyOrdersFilteredBaseData = new Mock<IFilteredBaseData<IInsolvencyOrderFilterBase>>();
            var disputesFilteredBaseData = new Mock<IFilteredBaseData<IDisputeFilterBase>>();

            // Act
            void ConstructInsolvenciesRepository() =>
                    new InsolvencyOrdersRepository(
                        context,
                        insolvencyOrdersFilteredBaseData.Object,
                        disputesFilteredBaseData.Object,
                        mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(ConstructInsolvenciesRepository, expectedParameterName);
        }

        /// <summary>
        /// Countings all records with seeded insolvencies records returns correct count.
        /// </summary>
        /// <param name="records">The records.</param>
        /// <returns>Task.</returns>
        [TestMethod]
        [DataRow(1, DisplayName = "With 1 seeded record.")]
        [DataRow(5, DisplayName = "With 5 seeded records.")]
        [DataRow(10, DisplayName = "With 10 seeded records.")]
        public async Task CountingAllRecords_WithSeededInsolvenciesRecords_ReturnsCorrectCount(int records)
        {
            // Arrange
            var insolvencyRecords = InsolvenciesDataCreationFixture.CreateMany(records);
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords);

            var operationDateProvider = new Mock<IOperationDateProvider>();
            operationDateProvider.Setup(x => x.GetOperationDate()).Returns(DateTime.Now);

            IOptions<RetentionOptions> retentionOptions =
                new ConfigurationOption(
                    new RetentionOptions()
                    {
                        RetentionPeriod = 10,
                        CutOffPeriod = 6
                    });

            var insolvencyOrdersFilteredBaseData = new InsolvencyOrderFilterContext(retentionOptions, operationDateProvider.Object);
            var disputesFilteredBaseData = new DisputeFilterContext(retentionOptions, operationDateProvider.Object);

            var insolvencyOrdersRepository =
                new InsolvencyOrdersRepository(
                    context,
                    insolvencyOrdersFilteredBaseData,
                    disputesFilteredBaseData,
                    mockTelemetryClient.Object);

            // Act
            var actualCount = await insolvencyOrdersRepository.CountAsync();

            // Assert
            Assert.AreEqual(records, actualCount);
        }

        /// <summary>
        /// Countings the records by identifier with seeded insolvencies records returns correct count.
        /// </summary>
        /// <returns>Task.</returns>
        [TestMethod]
        public async Task CountingRecordsById_WithSeededInsolvenciesRecords_ReturnsCorrectCount()
        {
            // Arrange
            const int recordId = 1;
            const int expectedCount = 1;
            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords);
            var operationDateProvider = new Mock<IOperationDateProvider>();
            operationDateProvider.Setup(x => x.GetOperationDate()).Returns(DateTime.Now);

            IOptions<RetentionOptions> retentionOptions =
                new ConfigurationOption(
                    new RetentionOptions()
                    {
                        RetentionPeriod = 10,
                        CutOffPeriod = 6
                    });

            var insolvencyOrdersFilteredBaseData = new InsolvencyOrderFilterContext(retentionOptions, operationDateProvider.Object);
            var disputesFilteredBaseData = new DisputeFilterContext(retentionOptions, operationDateProvider.Object);

            var insolvencyOrdersRepository =
                new InsolvencyOrdersRepository(
                    context,
                    insolvencyOrdersFilteredBaseData,
                    disputesFilteredBaseData,
                        mockTelemetryClient.Object);

            // Act
            var actualCount = await insolvencyOrdersRepository.CountByAsync(recordId, record => record.InsolvencyOrderId);

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Gettings the record by identifier with seeded insolvencies records returns mapped model.
        /// </summary>
        /// <returns>Task.</returns>
        [TestMethod]
        public async Task GettingRecordById_WithSeededInsolvenciesRecords_ReturnsMappedModel()
        {
            // Arrange
            const int insolvencyOrderId = 1;
            var expectedRecord = InsolvenciesTestData.GetInsolvencyById(insolvencyOrderId);
            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords);
            var operationDateProvider = new Mock<IOperationDateProvider>();
            operationDateProvider.Setup(x => x.GetOperationDate()).Returns(DateTime.Now);

            IOptions<RetentionOptions> retentionOptions =
                new ConfigurationOption(
                    new RetentionOptions()
                    {
                        RetentionPeriod = 10,
                        CutOffPeriod = 6
                    });

            var insolvencyOrdersFilteredBaseData = new InsolvencyOrderFilterContext(retentionOptions, operationDateProvider.Object);
            var disputesFilteredBaseData = new DisputeFilterContext(retentionOptions, operationDateProvider.Object);

            var insolvencyOrdersRepository =
                new InsolvencyOrdersRepository(
                    context,
                    insolvencyOrdersFilteredBaseData,
                    disputesFilteredBaseData,
                        mockTelemetryClient.Object);

            // Act
            var actualRecord = await insolvencyOrdersRepository.GetResultByAsync(insolvencyOrderId, record => record.InsolvencyOrderId);

            // Assert
            Assert.AreEqual(expectedRecord.InsolvencyOrderId, actualRecord.InsolvencyOrderId);
            Assert.AreEqual(expectedRecord.DischargeDate, actualRecord.DischargeDate);
            Assert.AreEqual(expectedRecord.LineOfBusiness, actualRecord.LineOfBusiness);
            Assert.AreEqual(expectedRecord.OrderDate, actualRecord.OrderDate);
            Assert.AreEqual(expectedRecord.RestrictionsEndDate, actualRecord.RestrictionsEndDate);
            Assert.AreEqual(expectedRecord.RestrictionsStartDate, actualRecord.RestrictionsStartDate);
            CollectionAssert.AreEqual(expectedRecord.Disputes, actualRecord.Disputes);
        }

        /// <summary>
        /// Gettings all records with seeded insolvencies records returns mapped models.
        /// </summary>
        /// <returns>Task.</returns>
        [TestMethod]
        public async Task GettingAllRecords_WithSeededInsolvenciesRecords_ReturnsMappedModels()
        {
            // Arrange
            var pageInformation = new PageInformation(1, 100);
            var expectedRecords = InsolvenciesTestData.CreateInsolvenciesExpecteds().ToList();
            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords);
            var operationDateProvider = new Mock<IOperationDateProvider>();
            operationDateProvider.Setup(x => x.GetOperationDate()).Returns(DateTime.Now);

            IOptions<RetentionOptions> retentionOptions =
                new ConfigurationOption(
                    new RetentionOptions()
                    {
                        RetentionPeriod = 10,
                        CutOffPeriod = 6
                    });

            var insolvencyOrdersFilteredBaseData = new InsolvencyOrderFilterContext(retentionOptions, operationDateProvider.Object);
            var disputesFilteredBaseData = new DisputeFilterContext(retentionOptions, operationDateProvider.Object);

            var insolvencyOrdersRepository =
                new InsolvencyOrdersRepository(
                    context,
                    insolvencyOrdersFilteredBaseData,
                    disputesFilteredBaseData,
                        mockTelemetryClient.Object);

            // Act
            var actualRecords = await insolvencyOrdersRepository.GetAllAsync(pageInformation);

            // Assert
            CollectionAssert.AreEqual(expectedRecords, actualRecords.ToList(), new InsolvencyModelComparer());
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
        public async Task GettingAllRecords_WithPageInformation_ReturnsMappedModels(int currentPage, int pageSize)
        {
            // Arrange
            var pageInformation = new PageInformation(currentPage, pageSize);
            var expectedRecords = InsolvenciesTestData.CreateInsolvenciesExpecteds(pageInformation).ToList();
            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords);
            var operationDateProvider = new Mock<IOperationDateProvider>();
            operationDateProvider.Setup(x => x.GetOperationDate()).Returns(DateTime.Now);

            IOptions<RetentionOptions> retentionOptions =
                new ConfigurationOption(
                    new RetentionOptions()
                    {
                        RetentionPeriod = 10,
                        CutOffPeriod = 6
                    });

            var insolvencyOrdersFilteredBaseData = new InsolvencyOrderFilterContext(retentionOptions, operationDateProvider.Object);
            var disputesFilteredBaseData = new DisputeFilterContext(retentionOptions, operationDateProvider.Object);

            var insolvencyOrdersRepository =
                new InsolvencyOrdersRepository(
                    context,
                    insolvencyOrdersFilteredBaseData,
                    disputesFilteredBaseData,
                    mockTelemetryClient.Object);

            // Act
            var actualRecords = (await insolvencyOrdersRepository.GetAllAsync(pageInformation)).ToList();

            // Assert
            CollectionAssert.AreEqual(expectedRecords, actualRecords, new InsolvencyModelComparer());
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
        [DataRow(1, 10, 7, DisplayName = "With first page and page size of 10.")]
        [DataRow(1, 100, 7, DisplayName = "With first page and page size of 100.")]
        [DataRow(2, 5, 2, DisplayName = "With second page and page size of 5.")]
        [DataRow(2, 100, 0, DisplayName = "With second page and page size of 100.")]
        public async Task GettingAllRecords_WithPageInformation_ReturnsExpectedNumberOfRecords(
            int currentPage,
            int pageSize,
            int expected)
        {
            // Arrange
            var pageInformation = new PageInformation(currentPage, pageSize);
            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords);
            var operationDateProvider = new Mock<IOperationDateProvider>();
            operationDateProvider.Setup(x => x.GetOperationDate()).Returns(DateTime.Now);

            IOptions<RetentionOptions> retentionOptions =
                new ConfigurationOption(
                    new RetentionOptions()
                    {
                        RetentionPeriod = 10,
                        CutOffPeriod = 6
                    });

            var insolvencyOrdersFilteredBaseData = new InsolvencyOrderFilterContext(retentionOptions, operationDateProvider.Object);
            var disputesFilteredBaseData = new DisputeFilterContext(retentionOptions, operationDateProvider.Object);

            var insolvencyOrdersRepository =
                new InsolvencyOrdersRepository(
                    context,
                    insolvencyOrdersFilteredBaseData,
                    disputesFilteredBaseData,
                    mockTelemetryClient.Object);

            // Act
            var actualCount = (await insolvencyOrdersRepository.GetAllAsync(pageInformation)).Count();

            // Assert
            Assert.AreEqual(expected, actualCount);
        }

        /// <summary>
        /// Gettings the records by residence identifier with seeded insolvencies records returns mapped models.
        /// </summary>
        /// <param name="residenceId">The residence identifier.</param>
        /// <returns>Task.</returns>
        [TestMethod]
        [DataRow(11, DisplayName = "With residence Id of 11.")]
        [DataRow(14, DisplayName = "With residence Id of 14.")]
        public async Task GettingRecordsByResidenceId_WithSeededInsolvenciesRecords_ReturnsMappedModels(int residenceId)
        {
            // Arrange
            var pageInformation = new PageInformation(1, 100);
            var expecteds = InsolvenciesTestData.GetExpectedsByResidenceId(residenceId);
            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords);
            var operationDateProvider = new Mock<IOperationDateProvider>();
            operationDateProvider.Setup(x => x.GetOperationDate()).Returns(DateTime.Now);

            IOptions<RetentionOptions> retentionOptions =
                new ConfigurationOption(
                    new RetentionOptions()
                    {
                        RetentionPeriod = 10,
                        CutOffPeriod = 6
                    });

            var insolvencyOrdersFilteredBaseData = new InsolvencyOrderFilterContext(retentionOptions, operationDateProvider.Object);
            var disputesFilteredBaseData = new DisputeFilterContext(retentionOptions, operationDateProvider.Object);

            var insolvencyOrdersRepository =
                new InsolvencyOrdersRepository(
                    context,
                    insolvencyOrdersFilteredBaseData,
                    disputesFilteredBaseData,
                    mockTelemetryClient.Object);

            // Act
            var actuals =
                await insolvencyOrdersRepository.GetResultsByAsync(
                    residenceId,
                    record => record.ResidenceId,
                    pageInformation);

            // Assert
            CollectionAssert.AreEqual(
                expecteds.OrderBy(x => x.InsolvencyOrderId).ToList(),
                actuals.OrderBy(x => x.InsolvencyOrderId).ToList(),
                new InsolvencyModelComparer());
        }

        /// <summary>
        /// Gettings the records by residence identifier with page information returns mapped models.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="residenceId">The residence identifier.</param>
        /// <returns>Task.</returns>
        [TestMethod]
        [DataRow(1, 10, 12, DisplayName = "With residence Id of 12.")]
        [DataRow(1, 10, 13, DisplayName = "With residence Id of 13.")]
        [DataRow(1, 100, 15, DisplayName = "With residence Id of 15.")]
        [DataRow(2, 10, 21, DisplayName = "With residence Id of 21.")]
        public async Task GettingRecordsByResidenceId_WithPageInformation_ReturnsMappedModels(
            int currentPage,
            int pageSize,
            int residenceId)
        {
            // Arrange
            var pageInformation = new PageInformation(currentPage, pageSize);
            var expecteds = InsolvenciesTestData.GetExpectedsByResidenceId(pageInformation, residenceId);
            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords);
            var operationDateProvider = new Mock<IOperationDateProvider>();
            operationDateProvider.Setup(x => x.GetOperationDate()).Returns(DateTime.Now);

            IOptions<RetentionOptions> retentionOptions =
                new ConfigurationOption(
                    new RetentionOptions()
                    {
                        RetentionPeriod = 10,
                        CutOffPeriod = 6
                    });

            var insolvencyOrdersFilteredBaseData = new InsolvencyOrderFilterContext(retentionOptions, operationDateProvider.Object);
            var disputesFilteredBaseData = new DisputeFilterContext(retentionOptions, operationDateProvider.Object);

            var insolvencyOrdersRepository =
                new InsolvencyOrdersRepository(
                    context,
                    insolvencyOrdersFilteredBaseData,
                    disputesFilteredBaseData,
                    mockTelemetryClient.Object);

            // Act
            var actuals =
                await insolvencyOrdersRepository.GetResultsByAsync(
                    residenceId,
                    record => record.ResidenceId,
                    pageInformation);

            // Assert
            CollectionAssert.AreEqual(
                expecteds.OrderBy(x => x.InsolvencyOrderId).ToList(),
                actuals.OrderBy(x => x.InsolvencyOrderId).ToList(),
                new InsolvencyModelComparer());
        }

        /// <summary>
        /// getting records by residence identifier with page information returns expected number of records as an asynchronous operation.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="residenceId">The residence identifier.</param>
        /// <param name="expected">The expected.</param>
        /// <returns>Task.</returns>
        [TestMethod]
        [DataRow(1, 10, 11, 3, DisplayName = "With residence Id of 11, page 1 and pagesize of 10.")]
        [DataRow(1, 13, 21, 2, DisplayName = "With residence Id of 21, page 1 and pagesize of 13.")]
        [DataRow(2, 10, 16, 0, DisplayName = "With residence Id of 16, page 2 and pagesize of 10.")]
        public async Task GettingRecordsByResidenceId_WithPageInformation_ReturnsExpectedNumberOfRecordsAsync(
            int currentPage,
            int pageSize,
            int residenceId,
            int expected)
        {
            // Arrange
            var pageInformation = new PageInformation(currentPage, pageSize);
            var insolvencyRecords = InsolvenciesTestData.CreateInsolvenciesActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords);

            var operationDateProvider = new Mock<IOperationDateProvider>();
            operationDateProvider.Setup(x => x.GetOperationDate()).Returns(DateTime.Now);

            IOptions<RetentionOptions> retentionOptions =
                new ConfigurationOption(
                    new RetentionOptions()
                    {
                        RetentionPeriod = 10,
                        CutOffPeriod = 6
                    });

            var insolvencyOrdersFilteredBaseData = new InsolvencyOrderFilterContext(retentionOptions, operationDateProvider.Object);
            var disputesFilteredBaseData = new DisputeFilterContext(retentionOptions, operationDateProvider.Object);

            var insolvencyOrdersRepository =
                new InsolvencyOrdersRepository(
                    context,
                    insolvencyOrdersFilteredBaseData,
                    disputesFilteredBaseData,
                    mockTelemetryClient.Object);

            // Act
            var actualCount = (await insolvencyOrdersRepository.GetResultsByAsync(
                    residenceId,
                    record => record.ResidenceId,
                    pageInformation)).Count();

            // Assert
            Assert.AreEqual(expected, actualCount);
        }

        /// <summary>
        /// Gettings all records with disputes returns insolvencies records with filtered disputes.
        /// </summary>
        /// <returns>Task.</returns>
        [TestMethod]
        public async Task GettingAllRecords_WithDisputes_ReturnsInsolvenciesRecordsWithFilteredDisputes()
        {
            // Arrange
            const string includes = DomainResources.Disputes;
            var pageInformation = new PageInformation(1, 100);
            var expectedInsolvenciesRecords = InsolvenciesWithDisputesTestData.CreateInsolvenciesExpecteds();
            var expectedDisputeRecords = expectedInsolvenciesRecords.SelectMany(insolvencyModel => insolvencyModel.Disputes);
            var insolvencyRecords = InsolvenciesWithDisputesTestData.CreateInsolvenciesActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords);

            var operationDateProvider = new Mock<IOperationDateProvider>();
            operationDateProvider.Setup(x => x.GetOperationDate()).Returns(DateTime.Now);

            IOptions<RetentionOptions> retentionOptions =
                new ConfigurationOption(
                    new RetentionOptions()
                    {
                        RetentionPeriod = 10,
                        CutOffPeriod = 6
                    });

            var insolvencyOrdersFilteredBaseData = new InsolvencyOrderFilterContext(retentionOptions, operationDateProvider.Object);
            var disputesFilteredBaseData = new DisputeFilterContext(retentionOptions, operationDateProvider.Object);

            var insolvencyOrdersRepository =
                new InsolvencyOrdersRepository(
                    context,
                    insolvencyOrdersFilteredBaseData,
                    disputesFilteredBaseData,
                    mockTelemetryClient.Object);

            // Act
            var actualInsolvenciesRecords = await insolvencyOrdersRepository.GetAllAsync(pageInformation, includes);
            var actualDisputeRecords = actualInsolvenciesRecords.SelectMany(insolvencyModel => insolvencyModel.Disputes);

            // Assert
            CollectionAssert.AreEqual(expectedInsolvenciesRecords.ToList(), actualInsolvenciesRecords.ToList(), new InsolvencyModelComparer());
            CollectionAssert.AreEqual(expectedDisputeRecords.ToList(), actualDisputeRecords.ToList(), new DisputeModelComparer());
        }

        /// <summary>
        /// Gettings the record by identifier with disputes returns insolvency record with filtered disputes.
        /// </summary>
        /// <returns>Task.</returns>
        [TestMethod]
        public async Task GettingRecordById_WithDisputes_ReturnsInsolvencyRecordWithFilteredDisputes()
        {
            // Arrange
            const int recordId = 1;
            const string includes = DomainResources.Disputes;
            var expectedRecord = InsolvenciesWithDisputesTestData.GetInsolvencyById(recordId);
            var insolvencyRecords = InsolvenciesWithDisputesTestData.CreateInsolvenciesActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords);

            var operationDateProvider = new Mock<IOperationDateProvider>();
            operationDateProvider.Setup(x => x.GetOperationDate()).Returns(DateTime.Now);

            IOptions<RetentionOptions> retentionOptions =
                new ConfigurationOption(
                    new RetentionOptions()
                    {
                        RetentionPeriod = 10,
                        CutOffPeriod = 6
                    });

            var insolvencyOrdersFilteredBaseData = new InsolvencyOrderFilterContext(retentionOptions, operationDateProvider.Object);
            var disputesFilteredBaseData = new DisputeFilterContext(retentionOptions, operationDateProvider.Object);

            var insolvencyOrdersRepository =
                new InsolvencyOrdersRepository(
                    context,
                    insolvencyOrdersFilteredBaseData,
                    disputesFilteredBaseData,
                    mockTelemetryClient.Object);

            // Act
            var actualRecord = await insolvencyOrdersRepository.GetResultByAsync(recordId, record => record.InsolvencyOrderId, includes);

            // Assert
            Assert.AreEqual(expectedRecord.InsolvencyOrderId, actualRecord.InsolvencyOrderId);
            Assert.AreEqual(expectedRecord.DischargeDate, actualRecord.DischargeDate);
            Assert.AreEqual(expectedRecord.LineOfBusiness, actualRecord.LineOfBusiness);
            Assert.AreEqual(expectedRecord.OrderDate, actualRecord.OrderDate);
            Assert.AreEqual(expectedRecord.RestrictionsEndDate, actualRecord.RestrictionsEndDate);
            Assert.AreEqual(expectedRecord.RestrictionsStartDate, actualRecord.RestrictionsStartDate);
            CollectionAssert.AreEqual(expectedRecord.Disputes, actualRecord.Disputes, new DisputeModelComparer());
        }

        /// <summary>
        /// Gettings the record by identifier with disputes and page information returns insolvencies records with filtered disputes.
        /// </summary>
        /// <returns>Task.</returns>
        [TestMethod]
        public async Task GettingRecordById_WithDisputesAndPageInformation_ReturnsInsolvenciesRecordsWithFilteredDisputes()
        {
            // Arrange
            const int recordId = 1;
            const string includes = DomainResources.Disputes;
            var pageInformation = new PageInformation(1, 100);
            var expectedInsolvencyRecord =
                new List<InsolvencyOrderModel> { InsolvenciesWithDisputesTestData.GetInsolvencyById(recordId) };
            var expectedDisputeRecords = expectedInsolvencyRecord.SelectMany(insolvencyModel => insolvencyModel.Disputes);
            var insolvencyRecords = InsolvenciesWithDisputesTestData.CreateInsolvenciesActuals();
            var context = TestDbContext.CreateContextWithSeededData(insolvencyRecords);

            var operationDateProvider = new Mock<IOperationDateProvider>();
            operationDateProvider.Setup(x => x.GetOperationDate()).Returns(DateTime.Now);

            IOptions<RetentionOptions> retentionOptions =
                new ConfigurationOption(
                    new RetentionOptions()
                    {
                        RetentionPeriod = 10,
                        CutOffPeriod = 6
                    });

            var insolvencyOrdersFilteredBaseData = new InsolvencyOrderFilterContext(retentionOptions, operationDateProvider.Object);
            var disputesFilteredBaseData = new DisputeFilterContext(retentionOptions, operationDateProvider.Object);

            var insolvencyOrdersRepository =
                new InsolvencyOrdersRepository(
                    context,
                    insolvencyOrdersFilteredBaseData,
                    disputesFilteredBaseData,
                    mockTelemetryClient.Object);

            // Act
            var actualInsolvencyRecord =
                await insolvencyOrdersRepository.GetResultsByAsync(
                    recordId,
                    record => record.InsolvencyOrderId,
                    pageInformation,
                    includes);
            var actualDisputeRecord = actualInsolvencyRecord.SelectMany(insolvencyModel => insolvencyModel.Disputes);

            // Assert
            CollectionAssert.AreEqual(
                expectedInsolvencyRecord.ToList(),
                actualInsolvencyRecord.ToList(),
                new InsolvencyModelComparer());
            CollectionAssert.AreEqual(
                expectedDisputeRecords.ToList(),
                actualDisputeRecord.ToList(),
                new DisputeModelComparer());
        }
    }
}
