// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 03-21-2018
//
// Last Modified By : MartinG
// Last Modified On : 05-18-2018
// ***********************************************************************
// <copyright file="TradingDetailsControllerTests.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>Defines unit tests for the TradingDetailsController URIs</summary>
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
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Api.UnitTests.Controllers
{
    /// <summary>
    /// Class TradingDetailsControllerTests.
    /// </summary>
    [TestClass]
    public class TradingDetailsControllerTests
    {
        /// <summary>
        /// The mock data access cradle
        /// </summary>
        private Mock<IDataAccessCradle<InsolvencyOrderTradingDetailsModel>> mockDataAccessCradle;

        /// <summary>
        /// The mock trading details repository
        /// </summary>
        private Mock<IInsolvencyOrderTradingDetailsRepository<InsolvencyOrderTradingDetailsModel, InsolvencyTradingDetailsEntity>> mockTradingDetailsRepository;

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
        private Mock<IHalFormatter<InsolvencyOrderTradingDetailsModel>> mockHalFormatter;

        /// <summary>
        /// The mock hal collection formatter
        /// </summary>
        private Mock<IHalCollectionFormatter<InsolvencyOrderTradingDetailsModel>> mockHalCollectionFormatter;

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
            this.mockDataAccessCradle = new Mock<IDataAccessCradle<InsolvencyOrderTradingDetailsModel>>();
            this.mockTradingDetailsRepository = new Mock<IInsolvencyOrderTradingDetailsRepository<InsolvencyOrderTradingDetailsModel, InsolvencyTradingDetailsEntity>>();
            this.mockIncludeReader = new Mock<IIncludeReader>();
            this.mockPageInformationProvider = new Mock<IPageInformationProvider>();
            this.mockHalFormatter = new Mock<IHalFormatter<InsolvencyOrderTradingDetailsModel>>();
            this.mockHalCollectionFormatter = new Mock<IHalCollectionFormatter<InsolvencyOrderTradingDetailsModel>>();
            this.mockTelemetryClient = new Mock<ITelemetryClient>();
        }

        /// <summary>
        /// Constructings the trading details controller with null cradle throws argument null exception.
        /// </summary>
        [TestMethod]
        [TestCategory("Null cradle - throws")]
        public void ConstructingTradingDetailsController_WithNullCradle_ThrowsArgumentNullException()
        {
            // Arrange
            IDataAccessCradle<InsolvencyOrderTradingDetailsModel> cradle = null;

            // Act
            void Constructing() => new InsolvencyOrderTradingDetailsController(
                cradle,
                mockTradingDetailsRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the trading details controller with null cradle has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        [TestCategory("Category [UnitTest - Null cradle - correct parameter name in exception]")]
        public void ConstructingTradingDetailsController_WithNullCradle_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "cradle";

            IDataAccessCradle<InsolvencyOrderTradingDetailsModel> cradle = null;

            // Act
            void Constructing() => new InsolvencyOrderTradingDetailsController(
                cradle,
                mockTradingDetailsRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the trading details controller with null trading details repository throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingTradingDetailsController_WithNullTradingDetailsRepository_ThrowsArgumentNullException()
        {
            // Arrange
            IInsolvencyOrderTradingDetailsRepository<InsolvencyOrderTradingDetailsModel, InsolvencyTradingDetailsEntity> tradingDetailsRepository = null;

            // Act
            void Constructing() => new InsolvencyOrderTradingDetailsController(
                mockDataAccessCradle.Object,
                tradingDetailsRepository,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the trading details controller with null trading details repository has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingTradingDetailsController_WithNullTradingDetailsRepository_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "tradingDetailsRepository";

            IInsolvencyOrderTradingDetailsRepository<InsolvencyOrderTradingDetailsModel, InsolvencyTradingDetailsEntity> tradingDetailsRepository = null;

            // Act
            void Constructing() => new InsolvencyOrderTradingDetailsController(
                mockDataAccessCradle.Object,
                tradingDetailsRepository,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the trading details controller with null include reader throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingTradingDetailsController_WithNullIncludeReader_ThrowsArgumentNullException()
        {
            // Arrange
            IIncludeReader includeReader = null;

            // Act
            void Constructing() => new InsolvencyOrderTradingDetailsController(
                mockDataAccessCradle.Object,
                mockTradingDetailsRepository.Object,
                includeReader,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the trading details controller with null include reader has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingTradingDetailsController_WithNullIncludeReader_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "includeReader";

            IIncludeReader includeReader = null;

            // Act
            void Constructing() => new InsolvencyOrderTradingDetailsController(
                mockDataAccessCradle.Object,
                mockTradingDetailsRepository.Object,
                includeReader,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the trading details controller with null page information provider throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingTradingDetailsController_WithNullPageInformationProvider_ThrowsArgumentNullException()
        {
            // Arrange
            IPageInformationProvider pageInformationProvider = null;

            // Act
            void Constructing() => new InsolvencyOrderTradingDetailsController(
                mockDataAccessCradle.Object,
                mockTradingDetailsRepository.Object,
                mockIncludeReader.Object,
                pageInformationProvider,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the trading details controller with null page information provider has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingTradingDetailsController_WithNullPageInformationProvider_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "pageInformationProvider";

            IPageInformationProvider pageInformationProvider = null;

            // Act
            void Constructing() => new InsolvencyOrderTradingDetailsController(
                mockDataAccessCradle.Object,
                mockTradingDetailsRepository.Object,
                mockIncludeReader.Object,
                pageInformationProvider,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the trading details controller with null insolvencies formatter throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingTradingDetailsController_WithNullInsolvenciesFormatter_ThrowsArgumentNullException()
        {
            // Arrange
            IHalFormatter<InsolvencyOrderTradingDetailsModel> halFormatter = null;

            // Act
            void Constructing() => new InsolvencyOrderTradingDetailsController(
                mockDataAccessCradle.Object,
                mockTradingDetailsRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                halFormatter,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the trading details controller with null insolvencies formatter has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingTradingDetailsController_WithNullInsolvenciesFormatter_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "tradingDetailsFormatter";

            IHalFormatter<InsolvencyOrderTradingDetailsModel> halFormatter = null;

            // Act
            void Constructing() => new InsolvencyOrderTradingDetailsController(
                mockDataAccessCradle.Object,
                mockTradingDetailsRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                halFormatter,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the trading details controller with null insolvencies collection formatter throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingTradingDetailsController_WithNullInsolvenciesCollectionFormatter_ThrowsArgumentNullException()
        {
            // Arrange
            IHalCollectionFormatter<InsolvencyOrderTradingDetailsModel> halCollectionFormatter = null;

            // Act
            void Constructing() => new InsolvencyOrderTradingDetailsController(
                mockDataAccessCradle.Object,
                mockTradingDetailsRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                halCollectionFormatter,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the trading details controller with null insolvencies collection formatter has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingTradingDetailsController_WithNullInsolvenciesCollectionFormatter_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "tradingDetailsCollectionFormatter";

            IHalCollectionFormatter<InsolvencyOrderTradingDetailsModel> halCollectionFormatter = null;

            // Act
            void Constructing() => new InsolvencyOrderTradingDetailsController(
                mockDataAccessCradle.Object,
                mockTradingDetailsRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                halCollectionFormatter,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the trading details controller with null TelemetryClient throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingTradingDetailsController_WithNullTelemetryClient_ThrowsArgumentNullException()
        {
            // Arrange
            ITelemetryClient telemetryClient = null;

            // Act
            void Constructing() => new InsolvencyOrderTradingDetailsController(
                                                                               mockDataAccessCradle.Object,
                                                                               mockTradingDetailsRepository.Object,
                                                                               mockIncludeReader.Object,
                                                                               mockPageInformationProvider.Object,
                                                                               mockHalFormatter.Object,
                                                                               mockHalCollectionFormatter.Object,
                                                                               telemetryClient);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the trading details controller with null TelemetryClient has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingTradingDetailsController_WithNullTelemetryClient_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "telemetryClient";

            ITelemetryClient telemetryClient = null;

            // Act
            void Constructing() => new InsolvencyOrderTradingDetailsController(
                                                                               mockDataAccessCradle.Object,
                                                                               mockTradingDetailsRepository.Object,
                                                                               mockIncludeReader.Object,
                                                                               mockPageInformationProvider.Object,
                                                                               mockHalFormatter.Object,
                                                                               mockHalCollectionFormatter.Object,
                                                                               telemetryClient);

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
                new CollectionResource<InsolvencyOrderTradingDetailsModel>(collectionResourceInfo, TestFixture.CreateMany<InsolvencyOrderTradingDetailsModel>(10).ToList());

            mockDataAccessCradle
                .Setup(cradle =>
                    cradle.GetPagedDataSetAsync(
                        It.IsAny<Func<Task<IEnumerable<InsolvencyOrderTradingDetailsModel>>>>(),
                        It.IsAny<Func<Task<int>>>()))
                .Returns(Task.FromResult(collectionResource));

            var controller = new InsolvencyOrderTradingDetailsController(
                mockDataAccessCradle.Object,
                mockTradingDetailsRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Act
            var result = controller.GetTradingDetailsByInsolvencyIdAsync(insolvencyOrderId).GetAwaiter().GetResult();

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
                new CollectionResource<InsolvencyOrderTradingDetailsModel>(collectionResourceInfo, new List<InsolvencyOrderTradingDetailsModel>());

            mockDataAccessCradle
                .Setup(cradle =>
                    cradle.GetPagedDataSetAsync(
                        It.IsAny<Func<Task<IEnumerable<InsolvencyOrderTradingDetailsModel>>>>(),
                        It.IsAny<Func<Task<int>>>()))
                .Returns(Task.FromResult(collectionResource));

            var controller =
                new InsolvencyOrderTradingDetailsController(
                    mockDataAccessCradle.Object,
                    mockTradingDetailsRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationProvider.Object,
                    mockHalFormatter.Object,
                    mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Act
            var result = controller.GetTradingDetailsByInsolvencyIdAsync(insolvencyOrderId).GetAwaiter().GetResult();

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
                        It.IsAny<Func<Task<IEnumerable<InsolvencyOrderTradingDetailsModel>>>>(),
                        It.IsAny<Func<Task<int>>>()))
                .Throws<Exception>();

            var controller = new InsolvencyOrderTradingDetailsController(
                mockDataAccessCradle.Object,
                mockTradingDetailsRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Act
            void Result() => controller.GetTradingDetailsByInsolvencyIdAsync(insolvencyOrderId).GetAwaiter().GetResult();

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
                    insolvencyCollectionFormatter.FormatForHal(It.IsAny<CollectionResource<InsolvencyOrderTradingDetailsModel>>()))
                .Throws<Exception>();

            var controller =
                new InsolvencyOrderTradingDetailsController(
                    mockDataAccessCradle.Object,
                    mockTradingDetailsRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationProvider.Object,
                    mockHalFormatter.Object,
                    mockHalCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Act
            void Result() => controller.GetTradingDetailsByInsolvencyIdAsync(insolvencyOrderId).GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Gets the trading details by trading details identifier asynchronous when there is a record returns status code200.
        /// </summary>
        [TestMethod]
        public void GetTradingDetailsByTradingDetailsIdAsync_WhenThereIsARecord_ReturnsStatusCode200()
        {
            // Arrange
            const int expectedStatusCode = 200;
            const int recordId = 1;

            mockDataAccessCradle
                .Setup(insolvencyOrdersCradle =>
                    insolvencyOrdersCradle.GetItemAsync(It.IsAny<Func<Task<InsolvencyOrderTradingDetailsModel>>>()))
                .Returns(Task.FromResult(new InsolvencyOrderTradingDetailsModel()));

            // Act
            var insolvencyOrdersController = new InsolvencyOrderTradingDetailsController(
                mockDataAccessCradle.Object,
                mockTradingDetailsRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Act
            var result = insolvencyOrdersController.GetTradingDetailsByTradingDetailsIdAsync(recordId).GetAwaiter().GetResult();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(expectedStatusCode, ((OkObjectResult)result).StatusCode);
        }

        /// <summary>
        /// Gets the trading details by trading details identifier asynchronous when the cradle throws an exception bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GetTradingDetailsByTradingDetailsIdAsync_WhenTheCradleThrowsAnException_BubblesUpTheException()
        {
            // Arrange
            const int recordId = 1;

            mockDataAccessCradle
                .Setup(insolvencyOrdersCradle =>
                    insolvencyOrdersCradle.GetItemAsync(It.IsAny<Func<Task<InsolvencyOrderTradingDetailsModel>>>()))
                .Throws<Exception>();

            // Act
            var insolvencyOrdersController = new InsolvencyOrderTradingDetailsController(
                mockDataAccessCradle.Object,
                mockTradingDetailsRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Act
            void Result() => insolvencyOrdersController.GetTradingDetailsByTradingDetailsIdAsync(recordId).GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Gets the trading details by trading details identifier asynchronous when there is a problem formatting the response as hal bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GetTradingDetailsByTradingDetailsIdAsync_WhenThereIsAProblemFormattingTheResponseAsHal_BubblesUpTheException()
        {
            // Arrange
            const int recordId = 1;

            mockHalFormatter
                .Setup(insolvencyFormatter => insolvencyFormatter.FormatForHal(It.IsAny<InsolvencyOrderTradingDetailsModel>()))
                .Throws<Exception>();

            // Act
            var insolvencyOrdersController = new InsolvencyOrderTradingDetailsController(
                mockDataAccessCradle.Object,
                mockTradingDetailsRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Act
            void Result() => insolvencyOrdersController.GetTradingDetailsByTradingDetailsIdAsync(recordId).GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Checks the trading details controller attribute has authorization attribute and query scope.
        /// </summary>
        [TestMethod]
        [TestCategory("Category [UnitTest - Authorization")]
        public void CheckTradingDetailsControllerAttribute_HasAuthorizationAttributeAndQueryScope()
        {
            // Arrange

            // Act
            var attributes = Attribute.GetCustomAttribute(typeof(InsolvencyOrderTradingDetailsController), typeof(AuthorizeAttribute));
            var authorizeAttribute = attributes as AuthorizeAttribute;

            // Assert
            Assert.IsNotNull(attributes);
            Assert.IsNotNull(authorizeAttribute);
            Assert.AreEqual("Query", authorizeAttribute.Policy);
        }
    }
}
