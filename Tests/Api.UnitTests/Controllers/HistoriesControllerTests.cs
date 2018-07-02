// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 03-21-2018
//
// Last Modified By : MartinG
// Last Modified On : 05-18-2018
// ***********************************************************************
// <copyright file="HistoriesControllerTests.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>HistoriesControllerTests</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Controllers;
using Api.EntityFramework.Entities;
using Api.Telemetry;
using Callcredit.Domain.Insolvencies.Models;
using Callcredit.Domain.Insolvencies.Repositories;
using Callcredit.RESTful.DataAssets;
using Callcredit.RESTful.Services.Hal;
using Callcredit.RESTful.Services.Includes;
using Callcredit.TestHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Api.UnitTests.Controllers
{
    /// <summary>
    /// Class HistoriesControllerTests.
    /// </summary>
    [TestClass]
    public class HistoriesControllerTests
    {
        /// <summary>
        /// The mock data access cradle
        /// </summary>
        private Mock<IDataAccessCradle<InsolvencyOrderHistoryModel>> mockDataAccessCradle;

        /// <summary>
        /// The mock histories repository
        /// </summary>
        private Mock<IInsolvencyOrderHistoriesRepository<InsolvencyOrderHistoryModel, InsolvencyOrderHistoryEntity>> mockHistoriesRepository;

        /// <summary>
        /// The mock include reader
        /// </summary>
        private Mock<IIncludeReader> mockIncludeReader;

        /// <summary>
        /// The mock page information provider
        /// </summary>
        private Mock<IPageInformationProvider> mockPageInformationProvider;

        /// <summary>
        /// The mock hal formatter
        /// </summary>
        private Mock<IHalFormatter<InsolvencyOrderHistoryModel>> mockHalFormatter;

        /// <summary>
        /// The mock hal collection formatter
        /// </summary>
        private Mock<IHalCollectionFormatter<InsolvencyOrderHistoryModel>> mockHalCollectionFormatter;

        /// <summary>
        /// The mock telemetry client
        /// </summary>
        private Mock<ITelemetryClient> mockTelemetryClient;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.mockDataAccessCradle = new Mock<IDataAccessCradle<InsolvencyOrderHistoryModel>>();
            this.mockHistoriesRepository = new Mock<IInsolvencyOrderHistoriesRepository<InsolvencyOrderHistoryModel, InsolvencyOrderHistoryEntity>>();
            this.mockIncludeReader = new Mock<IIncludeReader>();
            this.mockPageInformationProvider = new Mock<IPageInformationProvider>();
            this.mockHalFormatter = new Mock<IHalFormatter<InsolvencyOrderHistoryModel>>();
            this.mockHalCollectionFormatter = new Mock<IHalCollectionFormatter<InsolvencyOrderHistoryModel>>();
            this.mockTelemetryClient = new Mock<ITelemetryClient>();
        }

        /// <summary>
        /// Constructings the histories controller with null cradle throws argument null exception.
        /// </summary>
        [TestMethod]
        [TestCategory("Null cradle - throws")]
        public void ConstructingHistoriesController_WithNullCradle_ThrowsArgumentNullException()
        {
            // Arrange
            IDataAccessCradle<InsolvencyOrderHistoryModel> cradle = null;

            // Act
            void Constructing() => new InsolvencyOrderHistoriesController(
                cradle,
                mockHistoriesRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the histories controller with null cradle has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        [TestCategory("Category [UnitTest - Null cradle - correct parameter name in exception]")]
        public void ConstructingHistoriesController_WithNullCradle_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "cradle";

            IDataAccessCradle<InsolvencyOrderHistoryModel> cradle = null;

            // Act
            void Constructing() => new InsolvencyOrderHistoriesController(
                cradle,
                mockHistoriesRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the histories controller with nullhistories repository throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingHistoriesController_WithNullhistoriesRepository_ThrowsArgumentNullException()
        {
            // Arrange
            IInsolvencyOrderHistoriesRepository<InsolvencyOrderHistoryModel, InsolvencyOrderHistoryEntity> historiesRepository = null;

            // Act
            void Constructing() => new InsolvencyOrderHistoriesController(
                mockDataAccessCradle.Object,
                historiesRepository,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the histories controller with nullhistories repository has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingHistoriesController_WithNullhistoriesRepository_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "historiesRepository";

            IInsolvencyOrderHistoriesRepository<InsolvencyOrderHistoryModel, InsolvencyOrderHistoryEntity> historiesRepository = null;

            // Act
            void Constructing() => new InsolvencyOrderHistoriesController(
                mockDataAccessCradle.Object,
                historiesRepository,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the histories controller with null IncludeReader throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingHistoriesController_WithNullIncludeReader_ThrowsArgumentNullException()
        {
            // Arrange
            // Act
            void Constructing() => new InsolvencyOrderHistoriesController(
                mockDataAccessCradle.Object,
                mockHistoriesRepository.Object,
                null,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the histories controller with null IncludeReader has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingHistoriesController_WithNullIncludeReader_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "includeReader";

            // Act
            void Constructing() => new InsolvencyOrderHistoriesController(
                mockDataAccessCradle.Object,
                mockHistoriesRepository.Object,
                null,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the histories controller with null page information provider throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingHistoriesController_WithNullPageInformationProvider_ThrowsArgumentNullException()
        {
            // Arrange
            IPageInformationProvider pageInformationProvider = null;

            // Act
            void Constructing() => new InsolvencyOrderHistoriesController(
                mockDataAccessCradle.Object,
                mockHistoriesRepository.Object,
                mockIncludeReader.Object,
                pageInformationProvider,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the histories controller with null page information provider has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingHistoriesController_WithNullPageInformationProvider_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "pageInformationProvider";

            IPageInformationProvider pageInformationProvider = null;

            // Act
            void Constructing() => new InsolvencyOrderHistoriesController(
                mockDataAccessCradle.Object,
                mockHistoriesRepository.Object,
                mockIncludeReader.Object,
                pageInformationProvider,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the histories controller with null insolvencies formatter throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingHistoriesController_WithNullInsolvenciesFormatter_ThrowsArgumentNullException()
        {
            // Arrange
            IHalFormatter<InsolvencyOrderHistoryModel> halFormatter = null;

            // Act
            void Constructing() => new InsolvencyOrderHistoriesController(
                mockDataAccessCradle.Object,
                mockHistoriesRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                halFormatter,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the histories controller with null insolvencies formatter has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingHistoriesController_WithNullInsolvenciesFormatter_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "historyFormatter";

            IHalFormatter<InsolvencyOrderHistoryModel> halFormatter = null;

            // Act
            void Constructing() => new InsolvencyOrderHistoriesController(
                mockDataAccessCradle.Object,
                mockHistoriesRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                halFormatter,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the histories controller with null insolvencies collection formatter throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingHistoriesController_WithNullInsolvenciesCollectionFormatter_ThrowsArgumentNullException()
        {
            // Arrange
            IHalCollectionFormatter<InsolvencyOrderHistoryModel> halCollectionFormatter = null;

            // Act
            void Constructing() => new InsolvencyOrderHistoriesController(
                mockDataAccessCradle.Object,
                mockHistoriesRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                halCollectionFormatter,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the histories controller with null insolvencies collection formatter has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingHistoriesController_WithNullInsolvenciesCollectionFormatter_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "historyCollectionFormatter";

            IHalCollectionFormatter<InsolvencyOrderHistoryModel> halCollectionFormatter = null;

            // Act
            void Constructing() => new InsolvencyOrderHistoriesController(
                mockDataAccessCradle.Object,
                mockHistoriesRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                halCollectionFormatter,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the histories controller with null telemetry client throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingHistoriesController_WithNullTelemetryClient_ThrowsArgumentNullException()
        {
            // Arrange
            // Act
            void Constructing() => new InsolvencyOrderHistoriesController(
                                                                          mockDataAccessCradle.Object,
                                                                          mockHistoriesRepository.Object,
                                                                          mockIncludeReader.Object,
                                                                          mockPageInformationProvider.Object,
                                                                          mockHalFormatter.Object,
                                                                          mockHalCollectionFormatter.Object,
                                                                          null);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the histories controller with null telemetry client has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingHistoriesController_WithNullTelemetryClient_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "telemetryClient";

            // Act
            void Constructing() => new InsolvencyOrderHistoriesController(
                                                                          mockDataAccessCradle.Object,
                                                                          mockHistoriesRepository.Object,
                                                                          mockIncludeReader.Object,
                                                                          mockPageInformationProvider.Object,
                                                                          mockHalFormatter.Object,
                                                                          mockHalCollectionFormatter.Object,
                                                                          null);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Gettings the trading details by insolvency order identifier when there are some returns ok status code200.
        /// </summary>
        /// <param name="insolvencyOrderId">The insolvency order identifier.</param>
        [TestMethod]
        [DataRow(123456, DisplayName = "With positive value InsolvencyOrderID.")]
        [DataRow(-321654, DisplayName = "With negative value InsolvencyOrderID.")]
        public void GettingTradingDetailsByInsolvencyOrderId_WhenThereAreSome_ReturnsOkStatusCode200(int insolvencyOrderId)
        {
            // Arrange
            const int expectedStatusCode = 200;

            var collectionResourceInfo = new CollectionResourceInfo(0, 10, 1);
            var collectionResource =
                new CollectionResource<InsolvencyOrderHistoryModel>(collectionResourceInfo, TestFixture.CreateMany<InsolvencyOrderHistoryModel>(10).ToList());

            mockDataAccessCradle
                .Setup(cradle =>
                    cradle.GetPagedDataSetAsync(
                        It.IsAny<Func<Task<IEnumerable<InsolvencyOrderHistoryModel>>>>(),
                        It.IsAny<Func<Task<int>>>()))
                .Returns(Task.FromResult(collectionResource));

            var controller = new InsolvencyOrderHistoriesController(
                mockDataAccessCradle.Object,
                mockHistoriesRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Act
            var result = controller.GetHistoriesByInsolvencyIdAsync(insolvencyOrderId).GetAwaiter().GetResult();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(expectedStatusCode, ((OkObjectResult)result).StatusCode);
        }

        /// <summary>
        /// Gettings the trading details by insolvency order identifier when there are none to return returns ok status code200.
        /// </summary>
        [TestMethod]
        public void GettingTradingDetailsByInsolvencyOrderId_WhenThereAreNoneToReturn_ReturnsOkStatusCode200()
        {
            // Arrange
            const int insolvencyOrderId = 7654321;
            const int expectedStatusCode = 200;

            var collectionResourceInfo = new CollectionResourceInfo(0, 10, 1);
            var collectionResource =
                new CollectionResource<InsolvencyOrderHistoryModel>(collectionResourceInfo, new List<InsolvencyOrderHistoryModel>());

            mockDataAccessCradle
                .Setup(cradle =>
                    cradle.GetPagedDataSetAsync(
                        It.IsAny<Func<Task<IEnumerable<InsolvencyOrderHistoryModel>>>>(),
                        It.IsAny<Func<Task<int>>>()))
                .Returns(Task.FromResult(collectionResource));

            var controller =
                new InsolvencyOrderHistoriesController(
                    mockDataAccessCradle.Object,
                    mockHistoriesRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationProvider.Object,
                    mockHalFormatter.Object,
                    mockHalCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Act
            var result = controller.GetHistoriesByInsolvencyIdAsync(insolvencyOrderId).GetAwaiter().GetResult();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(expectedStatusCode, ((OkObjectResult)result).StatusCode);
        }

        /// <summary>
        /// Gettings the trading details by insolvency order identifier when the cradle throws an exception bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GettingTradingDetailsByInsolvencyOrderId_WhenTheCradleThrowsAnException_BubblesUpTheException()
        {
            // Arrange
            const int insolvencyOrderId = 1237654;

            mockDataAccessCradle
                .Setup(cradle =>
                    cradle.GetPagedDataSetAsync(
                        It.IsAny<Func<Task<IEnumerable<InsolvencyOrderHistoryModel>>>>(),
                        It.IsAny<Func<Task<int>>>()))
                .Throws<Exception>();

            var controller = new InsolvencyOrderHistoriesController(
                mockDataAccessCradle.Object,
                mockHistoriesRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Act
            void Result() => controller.GetHistoriesByInsolvencyIdAsync(insolvencyOrderId).GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Gettings the trading details by insolvency order identifier when there is a problem formatting the response as hal bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GettingTradingDetailsByInsolvencyOrderId_WhenThereIsAProblemFormattingTheResponseAsHal_BubblesUpTheException()
        {
            // Arrange
            const int insolvencyOrderId = 9876543;

            mockHalCollectionFormatter
                .Setup(insolvencyCollectionFormatter =>
                    insolvencyCollectionFormatter.FormatForHal(It.IsAny<CollectionResource<InsolvencyOrderHistoryModel>>()))
                .Throws<Exception>();

            var controller =
                new InsolvencyOrderHistoriesController(
                    mockDataAccessCradle.Object,
                    mockHistoriesRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationProvider.Object,
                    mockHalFormatter.Object,
                    mockHalCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Act
            void Result() => controller.GetHistoriesByInsolvencyIdAsync(insolvencyOrderId).GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Gets the persons by person identifier when there is a record returns status code200.
        /// </summary>
        [TestMethod]
        public void GetPersonsByPersonId_WhenThereIsARecord_ReturnsStatusCode200()
        {
            // Arrange
            const int expectedStatusCode = 200;
            const int recordId = 1;

            mockDataAccessCradle
                .Setup(insolvencyOrdersCradle =>
                    insolvencyOrdersCradle.GetItemAsync(It.IsAny<Func<Task<InsolvencyOrderHistoryModel>>>()))
                .Returns(Task.FromResult(new InsolvencyOrderHistoryModel()));

            // Act
            var insolvencyOrdersController = new InsolvencyOrderHistoriesController(
                mockDataAccessCradle.Object,
                mockHistoriesRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Act
            var result = insolvencyOrdersController.GetHistoriesByHistoryId(recordId).GetAwaiter().GetResult();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(expectedStatusCode, ((OkObjectResult)result).StatusCode);
        }

        /// <summary>
        /// Gets the persons by person identifier when the cradle throws an exception bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GetPersonsByPersonId_WhenTheCradleThrowsAnException_BubblesUpTheException()
        {
            // Arrange
            const int recordId = 1;

            mockDataAccessCradle
                .Setup(insolvencyOrdersCradle =>
                    insolvencyOrdersCradle.GetItemAsync(It.IsAny<Func<Task<InsolvencyOrderHistoryModel>>>()))
                .Throws<Exception>();

            // Act
            var insolvencyOrdersController = new InsolvencyOrderHistoriesController(
                mockDataAccessCradle.Object,
                mockHistoriesRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Act
            void Result() => insolvencyOrdersController.GetHistoriesByHistoryId(recordId).GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Gets the persons by person identifier when there is a problem formatting the response as hal bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GetPersonsByPersonId_WhenThereIsAProblemFormattingTheResponseAsHal_BubblesUpTheException()
        {
            // Arrange
            const int recordId = 1;

            mockHalFormatter
                .Setup(insolvencyFormatter => insolvencyFormatter.FormatForHal(It.IsAny<InsolvencyOrderHistoryModel>()))
                .Throws<Exception>();

            // Act
            var insolvencyOrdersController = new InsolvencyOrderHistoriesController(
                mockDataAccessCradle.Object,
                mockHistoriesRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Act
            void Result() => insolvencyOrdersController.GetHistoriesByHistoryId(recordId).GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Checks the histories controller attribute has authorization attribute and query scope.
        /// </summary>
        [TestMethod]
        [TestCategory("Category [UnitTest - Authorization")]
        public void CheckHistoriesControllerAttribute_HasAuthorizationAttributeAndQueryScope()
        {
            // Arrange

            // Act
            var attributes = Attribute.GetCustomAttribute(typeof(InsolvencyOrderHistoriesController), typeof(AuthorizeAttribute));
            var authorizeAttribute = attributes as AuthorizeAttribute;

            // Assert
            Assert.IsNotNull(attributes);
            Assert.IsNotNull(authorizeAttribute);
            Assert.AreEqual("Query", authorizeAttribute.Policy);
        }
    }
}
