// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 03-21-2018
//
// Last Modified By : MartinG
// Last Modified On : 05-21-2018
// ***********************************************************************
// <copyright file="AddressControllerTests.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>AddressControllerTests class</summary>
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
    /// Class AddressControllerTests.
    /// </summary>
    [TestClass]
    public class AddressControllerTests
    {
        /// <summary>
        /// The mock data access cradle
        /// </summary>
        private Mock<IDataAccessCradle<InsolvencyOrderAddressModel>> mockDataAccessCradle;

        /// <summary>
        /// The mock addresses repository
        /// </summary>
        private Mock<IInsolvencyOrderAddressesRepository<InsolvencyOrderAddressModel, InsolvencyOrderAddressEntity>> mockAddressesRepository;

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
        private Mock<IHalFormatter<InsolvencyOrderAddressModel>> mockHalFormatter;

        /// <summary>
        /// The mock hal collection formatter
        /// </summary>
        private Mock<IHalCollectionFormatter<InsolvencyOrderAddressModel>> mockHalCollectionFormatter;

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
            mockDataAccessCradle = new Mock<IDataAccessCradle<InsolvencyOrderAddressModel>>();
            mockAddressesRepository = new Mock<IInsolvencyOrderAddressesRepository<InsolvencyOrderAddressModel, InsolvencyOrderAddressEntity>>();
            mockIncludeReader = new Mock<IIncludeReader>();
            mockPageInformationProvider = new Mock<IPageInformationProvider>();
            mockHalFormatter = new Mock<IHalFormatter<InsolvencyOrderAddressModel>>();
            mockHalCollectionFormatter = new Mock<IHalCollectionFormatter<InsolvencyOrderAddressModel>>();
            mockTelemetryClient = new Mock<ITelemetryClient>();
        }

        /// <summary>
        /// Constructings the addresses controller with null cradle throws argument null exception.
        /// </summary>
        [TestMethod]
        [TestCategory("Null cradle - throws")]
        public void ConstructingAddressesController_WithNullCradle_ThrowsArgumentNullException()
        {
            // Arrange
            // Act
            void Constructing() => new InsolvencyOrderAddressesController(
                null, // IDataAccessCradle<InsolvencyOrderAddressModel>
                mockAddressesRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the addresses controller with null cradle has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        [TestCategory("Category [UnitTest - Null cradle - correct parameter name in exception]")]
        public void ConstructingAddressesController_WithNullCradle_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "cradle";

            // Act
            void Constructing() => new InsolvencyOrderAddressesController(
                null, // IDataAccessCradle<InsolvencyOrderAddressModel>
                mockAddressesRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the addresses controller with nulladdresses repository throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingAddressesController_WithNulladdressesRepository_ThrowsArgumentNullException()
        {
            // Arrange
            IInsolvencyOrderAddressesRepository<InsolvencyOrderAddressModel, InsolvencyOrderAddressEntity> addressesRepository = null;

            // Act
            void Constructing() => new InsolvencyOrderAddressesController(
                mockDataAccessCradle.Object,
                addressesRepository,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the addresses controller with nulladdresses repository has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingAddressesController_WithNulladdressesRepository_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "addressesRepository";

            IInsolvencyOrderAddressesRepository<InsolvencyOrderAddressModel, InsolvencyOrderAddressEntity> addressesRepository = null;

            // Act
            void Constructing() => new InsolvencyOrderAddressesController(
                mockDataAccessCradle.Object,
                addressesRepository,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the addresses controller with null page information provider throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingAddressesController_WithNullPageInformationProvider_ThrowsArgumentNullException()
        {
            // Arrange
            IPageInformationProvider pageInformationProvider = null;

            // Act
            void Constructing() => new InsolvencyOrderAddressesController(
                mockDataAccessCradle.Object,
                mockAddressesRepository.Object,
                mockIncludeReader.Object,
                pageInformationProvider,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the addresses controller with null page information provider has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingAddressesController_WithNullPageInformationProvider_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "pageInformationProvider";

            IPageInformationProvider pageInformationProvider = null;

            // Act
            void Constructing() => new InsolvencyOrderAddressesController(
                mockDataAccessCradle.Object,
                mockAddressesRepository.Object,
                mockIncludeReader.Object,
                pageInformationProvider,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the addresses controller with null insolvencies formatter throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingAddressesController_WithNullInsolvenciesFormatter_ThrowsArgumentNullException()
        {
            // Arrange
            IHalFormatter<InsolvencyOrderAddressModel> halFormatter = null;

            // Act
            void Constructing() => new InsolvencyOrderAddressesController(
                mockDataAccessCradle.Object,
                mockAddressesRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                halFormatter,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the addresses controller with null insolvencies formatter has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingAddressesController_WithNullInsolvenciesFormatter_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "addressFormatter";

            IHalFormatter<InsolvencyOrderAddressModel> halFormatter = null;

            // Act
            void Constructing() => new InsolvencyOrderAddressesController(
                mockDataAccessCradle.Object,
                mockAddressesRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                halFormatter,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the addresses controller with null insolvencies collection formatter throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingAddressesController_WithNullInsolvenciesCollectionFormatter_ThrowsArgumentNullException()
        {
            // Arrange
            IHalCollectionFormatter<InsolvencyOrderAddressModel> halCollectionFormatter = null;

            // Act
            void Constructing() => new InsolvencyOrderAddressesController(
                mockDataAccessCradle.Object,
                mockAddressesRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                halCollectionFormatter,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the addresses controller with null insolvencies collection formatter has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingAddressesController_WithNullInsolvenciesCollectionFormatter_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "addressCollectionFormatter";

            IHalCollectionFormatter<InsolvencyOrderAddressModel> halCollectionFormatter = null;

            // Act
            void Constructing() => new InsolvencyOrderAddressesController(
                mockDataAccessCradle.Object,
                mockAddressesRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                halCollectionFormatter,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the addresses controller with null includeReader throws argument null exception.
        /// </summary>
        [TestMethod]
        [TestCategory("Null Include Reader - throws")]
        public void ConstructingAddressesController_WithNullIncludeReader_ThrowsArgumentNullException()
        {
            // Arrange
            // Act
            void Constructing() => new InsolvencyOrderAddressesController(
                                                                          mockDataAccessCradle.Object,
                                                                          mockAddressesRepository.Object,
                                                                          null,
                                                                          mockPageInformationProvider.Object,
                                                                          mockHalFormatter.Object,
                                                                          mockHalCollectionFormatter.Object,
                                                                          mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the addresses controller with null includeReader has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingAddressesController_WithNullIncludeReader_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "includeReader";

            // Act
            void Constructing() => new InsolvencyOrderAddressesController(
                                                                          mockDataAccessCradle.Object,
                                                                          mockAddressesRepository.Object,
                                                                          null,
                                                                          mockPageInformationProvider.Object,
                                                                          mockHalFormatter.Object,
                                                                          mockHalCollectionFormatter.Object,
                                                                          mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the addresses controller with null Telemetry Client throws argument null exception.
        /// </summary>
        [TestMethod]
        [TestCategory("Null Telemetry Client - throws")]
        public void ConstructingAddressesController_WithNullTelemetryClient_ThrowsArgumentNullException()
        {
            // Arrange
            // Act
            void Constructing() => new InsolvencyOrderAddressesController(
                                                                          mockDataAccessCradle.Object,
                                                                          mockAddressesRepository.Object,
                                                                          mockIncludeReader.Object,
                                                                          mockPageInformationProvider.Object,
                                                                          mockHalFormatter.Object,
                                                                          mockHalCollectionFormatter.Object,
                                                                          null);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the addresses controller with null Telemetry Client throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingAddressesController_WithNullTelemetryClient_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "telemetryClient";

            // Act
            void Constructing() => new InsolvencyOrderAddressesController(
                                                                          mockDataAccessCradle.Object,
                                                                          mockAddressesRepository.Object,
                                                                          mockIncludeReader.Object,
                                                                          mockPageInformationProvider.Object,
                                                                          mockHalFormatter.Object,
                                                                          mockHalCollectionFormatter.Object,
                                                                          null);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Gettings the addresses details by insolvency order identifier when there are some returns ok status code200.
        /// </summary>
        /// <param name="insolvencyOrderId">The insolvency order identifier.</param>
        [TestMethod]
        [DataRow(123456, DisplayName = "With positive value InsolvencyOrderID.")]
        [DataRow(-321654, DisplayName = "With negative value InsolvencyOrderID.")]
        public void GettingAddressesDetailsByInsolvencyOrderId_WhenThereAreSome_ReturnsOkStatusCode200(int insolvencyOrderId)
        {
            // Arrange
            const int expectedStatusCode = 200;

            var collectionResourceInfo = new CollectionResourceInfo(0, 10, 1);
            var collectionResource =
                new CollectionResource<InsolvencyOrderAddressModel>(
                    collectionResourceInfo,
                    TestFixture.CreateMany<InsolvencyOrderAddressModel>(10).ToList());

            mockDataAccessCradle
                .Setup(cradle =>
                    cradle.GetPagedDataSetAsync(
                        It.IsAny<Func<Task<IEnumerable<InsolvencyOrderAddressModel>>>>(),
                        It.IsAny<Func<Task<int>>>()))
                .Returns(Task.FromResult(collectionResource));

            var controller = new InsolvencyOrderAddressesController(
                mockDataAccessCradle.Object,
                mockAddressesRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Act
            var result = controller.GetAddressesByInsolvencyIdAsync(insolvencyOrderId).GetAwaiter().GetResult();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(expectedStatusCode, ((OkObjectResult)result).StatusCode);
        }

        /// <summary>
        /// Gettings the addresses details by insolvency order identifier when there are none to return returns ok status code200.
        /// </summary>
        [TestMethod]
        public void GettingAddressesDetailsByInsolvencyOrderId_WhenThereAreNoneToReturn_ReturnsOkStatusCode200()
        {
            // Arrange
            const int insolvencyOrderId = 7654321;
            const int expectedStatusCode = 200;

            var collectionResourceInfo = new CollectionResourceInfo(0, 10, 1);
            var collectionResource =
                new CollectionResource<InsolvencyOrderAddressModel>(collectionResourceInfo, new List<InsolvencyOrderAddressModel>());

            mockDataAccessCradle
                .Setup(cradle =>
                    cradle.GetPagedDataSetAsync(
                        It.IsAny<Func<Task<IEnumerable<InsolvencyOrderAddressModel>>>>(),
                        It.IsAny<Func<Task<int>>>()))
                .Returns(Task.FromResult(collectionResource));

            var controller =
                new InsolvencyOrderAddressesController(
                    mockDataAccessCradle.Object,
                    mockAddressesRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationProvider.Object,
                    mockHalFormatter.Object,
                    mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Act
            var result = controller.GetAddressesByInsolvencyIdAsync(insolvencyOrderId).GetAwaiter().GetResult();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(expectedStatusCode, ((OkObjectResult)result).StatusCode);
        }

        /// <summary>
        /// Gettings the addresses details by insolvency order identifier when the cradle throws an exception bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GettingAddressesDetailsByInsolvencyOrderId_WhenTheCradleThrowsAnException_BubblesUpTheException()
        {
            // Arrange
            const int insolvencyOrderId = 1237654;

            mockDataAccessCradle
                .Setup(cradle =>
                    cradle.GetPagedDataSetAsync(
                        It.IsAny<Func<Task<IEnumerable<InsolvencyOrderAddressModel>>>>(),
                        It.IsAny<Func<Task<int>>>()))
                .Throws<Exception>();

            var controller = new InsolvencyOrderAddressesController(
                mockDataAccessCradle.Object,
                mockAddressesRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Act
            void Result() => controller.GetAddressesByInsolvencyIdAsync(insolvencyOrderId).GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Gettings the addresses details by insolvency order identifier when there is a problem formatting the response as hal bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GettingAddressesDetailsByInsolvencyOrderId_WhenThereIsAProblemFormattingTheResponseAsHal_BubblesUpTheException()
        {
            // Arrange
            const int insolvencyOrderId = 9876543;

            mockHalCollectionFormatter
                .Setup(insolvencyCollectionFormatter =>
                    insolvencyCollectionFormatter.FormatForHal(It.IsAny<CollectionResource<InsolvencyOrderAddressModel>>()))
                .Throws<Exception>();

            var controller =
                new InsolvencyOrderAddressesController(
                    mockDataAccessCradle.Object,
                    mockAddressesRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationProvider.Object,
                    mockHalFormatter.Object,
                    mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Act
            void Result() => controller.GetAddressesByInsolvencyIdAsync(insolvencyOrderId).GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Gets the addresses by address identifier when there is a record returns status code200.
        /// </summary>
        [TestMethod]
        public void GetAddressesByAddressId_WhenThereIsARecord_ReturnsStatusCode200()
        {
            // Arrange
            const int expectedStatusCode = 200;
            const int recordId = 1;

            mockDataAccessCradle
                .Setup(insolvencyOrdersCradle =>
                    insolvencyOrdersCradle.GetItemAsync(It.IsAny<Func<Task<InsolvencyOrderAddressModel>>>()))
                .Returns(Task.FromResult(new InsolvencyOrderAddressModel()));

            // Act
            var insolvencyOrdersController = new InsolvencyOrderAddressesController(
                mockDataAccessCradle.Object,
                mockAddressesRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Act
            var result = insolvencyOrdersController.GetAddressesByAddressId(recordId).GetAwaiter().GetResult();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(expectedStatusCode, ((OkObjectResult)result).StatusCode);
        }

        /// <summary>
        /// Gets the addresses by address identifier when the cradle throws an exception bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GetAddressesByAddressId_WhenTheCradleThrowsAnException_BubblesUpTheException()
        {
            // Arrange
            const int recordId = 1;

            mockDataAccessCradle
                .Setup(insolvencyOrdersCradle =>
                    insolvencyOrdersCradle.GetItemAsync(It.IsAny<Func<Task<InsolvencyOrderAddressModel>>>()))
                .Throws<Exception>();

            // Act
            var insolvencyOrdersController = new InsolvencyOrderAddressesController(
                mockDataAccessCradle.Object,
                mockAddressesRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Act
            void Result() => insolvencyOrdersController.GetAddressesByAddressId(recordId).GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Gets the addresses by address identifier when there is a problem formatting the response as hal bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GetAddressesByAddressId_WhenThereIsAProblemFormattingTheResponseAsHal_BubblesUpTheException()
        {
            // Arrange
            const int recordId = 1;

            mockHalFormatter
                .Setup(insolvencyFormatter => insolvencyFormatter.FormatForHal(It.IsAny<InsolvencyOrderAddressModel>()))
                .Throws<Exception>();

            // Act
            var insolvencyOrdersController = new InsolvencyOrderAddressesController(
                mockDataAccessCradle.Object,
                mockAddressesRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Act
            void Result() => insolvencyOrdersController.GetAddressesByAddressId(recordId).GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Checks the addresses controller attribute has authorization attribute and query scope.
        /// </summary>
        [TestMethod]
        [TestCategory("Category [UnitTest - Authorization")]
        public void CheckAddressesControllerAttribute_HasAuthorizationAttributeAndQueryScope()
        {
            // Arrange

            // Act
            var attributes = Attribute.GetCustomAttribute(typeof(InsolvencyOrderAddressesController), typeof(AuthorizeAttribute));
            var authorizeAttribute = attributes as AuthorizeAttribute;

            // Assert
            Assert.IsNotNull(attributes);
            Assert.IsNotNull(authorizeAttribute);
            Assert.AreEqual("Query", authorizeAttribute.Policy);
        }
    }
}
