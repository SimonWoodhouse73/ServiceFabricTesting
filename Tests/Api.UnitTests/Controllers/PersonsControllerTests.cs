// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 03-21-2018
//
// Last Modified By : MartinG
// Last Modified On : 05-18-2018
// ***********************************************************************
// <copyright file="PersonsControllerTests.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>PersonsControllerTests</summary>
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
    /// Class PersonsControllerTests.
    /// </summary>
    [TestClass]
    public class PersonsControllerTests
    {
        /// <summary>
        /// The mock data access cradle
        /// </summary>
        private Mock<IDataAccessCradle<InsolvencyOrderPersonModel>> mockDataAccessCradle;

        /// <summary>
        /// The mock persons repository
        /// </summary>
        private Mock<IInsolvencyOrderPersonsRepository<InsolvencyOrderPersonModel, InsolvencyOrderPersonEntity>> mockPersonsRepository;

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
        private Mock<IHalFormatter<InsolvencyOrderPersonModel>> mockHalFormatter;

        /// <summary>
        /// The mock hal collection formatter
        /// </summary>
        private Mock<IHalCollectionFormatter<InsolvencyOrderPersonModel>> mockHalCollectionFormatter;

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
            this.mockDataAccessCradle = new Mock<IDataAccessCradle<InsolvencyOrderPersonModel>>();
            this.mockPersonsRepository = new Mock<IInsolvencyOrderPersonsRepository<InsolvencyOrderPersonModel, InsolvencyOrderPersonEntity>>();
            this.mockIncludeReader = new Mock<IIncludeReader>();
            this.mockPageInformationProvider = new Mock<IPageInformationProvider>();
            this.mockHalFormatter = new Mock<IHalFormatter<InsolvencyOrderPersonModel>>();
            this.mockHalCollectionFormatter = new Mock<IHalCollectionFormatter<InsolvencyOrderPersonModel>>();
            this.mockTelemetryClient = new Mock<ITelemetryClient>();
        }

        /// <summary>
        /// Constructings the persons controller with null cradle throws argument null exception.
        /// </summary>
        [TestMethod]
        [TestCategory("Null cradle - throws")]
        public void ConstructingPersonsController_WithNullCradle_ThrowsArgumentNullException()
        {
            // Arrange
            IDataAccessCradle<InsolvencyOrderPersonModel> cradle = null;

            // Act
            void Constructing() => new InsolvencyOrderPersonsController(
                cradle,
                mockPersonsRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the persons controller with null cradle has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        [TestCategory("Category [UnitTest - Null cradle - correct parameter name in exception]")]
        public void ConstructingPersonsController_WithNullCradle_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "cradle";

            IDataAccessCradle<InsolvencyOrderPersonModel> cradle = null;

            // Act
            void Constructing() => new InsolvencyOrderPersonsController(
                cradle,
                mockPersonsRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the persons controller with nullpersons repository throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingPersonsController_WithNullpersonsRepository_ThrowsArgumentNullException()
        {
            // Arrange
            IInsolvencyOrderPersonsRepository<InsolvencyOrderPersonModel, InsolvencyOrderPersonEntity> personsRepository = null;

            // Act
            void Constructing() => new InsolvencyOrderPersonsController(
                mockDataAccessCradle.Object,
                personsRepository,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the persons controller with nullpersons repository has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingPersonsController_WithNullpersonsRepository_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "personsRepository";

            IInsolvencyOrderPersonsRepository<InsolvencyOrderPersonModel, InsolvencyOrderPersonEntity> personsRepository = null;

            // Act
            void Constructing() => new InsolvencyOrderPersonsController(
                mockDataAccessCradle.Object,
                personsRepository,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the persons controller with null IncludeReader throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingPersonsController_WithNullIncludeReader_ThrowsArgumentNullException()
        {
            // Arrange
            // Act
            void Constructing() => new InsolvencyOrderPersonsController(
                mockDataAccessCradle.Object,
                mockPersonsRepository.Object,
                null,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the persons controller with nullpersons repository has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingPersonsController_WithNullIncludeReader_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "includeReader";

            // Act
            void Constructing() => new InsolvencyOrderPersonsController(
                                                                        mockDataAccessCradle.Object,
                                                                        mockPersonsRepository.Object,
                                                                        null,
                                                                        mockPageInformationProvider.Object,
                                                                        mockHalFormatter.Object,
                                                                        mockHalCollectionFormatter.Object,
                                                                        mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the persons controller with null page information provider throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingPersonsController_WithNullPageInformationProvider_ThrowsArgumentNullException()
        {
            // Arrange
            IPageInformationProvider pageInformationProvider = null;

            // Act
            void Constructing() => new InsolvencyOrderPersonsController(
                mockDataAccessCradle.Object,
                mockPersonsRepository.Object,
                mockIncludeReader.Object,
                pageInformationProvider,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the persons controller with null page information provider has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingPersonsController_WithNullPageInformationProvider_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "pageInformationProvider";

            IPageInformationProvider pageInformationProvider = null;

            // Act
            void Constructing() => new InsolvencyOrderPersonsController(
                mockDataAccessCradle.Object,
                mockPersonsRepository.Object,
                mockIncludeReader.Object,
                pageInformationProvider,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the persons controller with null insolvencies formatter throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingPersonsController_WithNullInsolvenciesFormatter_ThrowsArgumentNullException()
        {
            // Arrange
            IHalFormatter<InsolvencyOrderPersonModel> halFormatter = null;

            // Act
            void Constructing() => new InsolvencyOrderPersonsController(
                mockDataAccessCradle.Object,
                mockPersonsRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                halFormatter,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the persons controller with null insolvencies formatter has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingPersonsController_WithNullInsolvenciesFormatter_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "personFormatter";

            IHalFormatter<InsolvencyOrderPersonModel> halFormatter = null;

            // Act
            void Constructing() => new InsolvencyOrderPersonsController(
                mockDataAccessCradle.Object,
                mockPersonsRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                halFormatter,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the persons controller with null insolvencies collection formatter throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingPersonsController_WithNullInsolvenciesCollectionFormatter_ThrowsArgumentNullException()
        {
            // Arrange
            IHalCollectionFormatter<InsolvencyOrderPersonModel> halCollectionFormatter = null;

            // Act
            void Constructing() => new InsolvencyOrderPersonsController(
                mockDataAccessCradle.Object,
                mockPersonsRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                halCollectionFormatter,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the persons controller with null insolvencies collection formatter has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingPersonsController_WithNullInsolvenciesCollectionFormatter_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "personCollectionFormatter";

            IHalCollectionFormatter<InsolvencyOrderPersonModel> halCollectionFormatter = null;

            // Act
            void Constructing() => new InsolvencyOrderPersonsController(
                mockDataAccessCradle.Object,
                mockPersonsRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                halCollectionFormatter,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the persons controller with null telemetry client throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingPersonsController_WithNullTelemetryClient_ThrowsArgumentNullException()
        {
            // Arrange
            // Act
            void Constructing() => new InsolvencyOrderPersonsController(
                mockDataAccessCradle.Object,
                mockPersonsRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                null);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the persons controller with null telemetry client has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingPersonsController_WithNullTelemetryClient_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "telemetryClient";

            // Act
            void Constructing() => new InsolvencyOrderPersonsController(
                mockDataAccessCradle.Object,
                mockPersonsRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                null);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Gettings the persons by insolvency order identifier when there are some returns ok status code200.
        /// </summary>
        /// <param name="insolvencyOrderId">The insolvency order identifier.</param>
        [TestMethod]
        [DataRow(123456, DisplayName = "With positive value InsolvencyOrderID.")]
        [DataRow(-321654, DisplayName = "With negative value InsolvencyOrderID.")]
        public void GettingPersonsByInsolvencyOrderId_WhenThereAreSome_ReturnsOkStatusCode200(int insolvencyOrderId)
        {
            // Arrange
            const int expectedStatusCode = 200;

            var collectionResourceInfo = new CollectionResourceInfo(0, 10, 1);
            var collectionResource =
                new CollectionResource<InsolvencyOrderPersonModel>(
                    collectionResourceInfo,
                    TestFixture.CreateMany<InsolvencyOrderPersonModel>(10).ToList());

            mockDataAccessCradle
                .Setup(cradle =>
                    cradle.GetPagedDataSetAsync(
                        It.IsAny<Func<Task<IEnumerable<InsolvencyOrderPersonModel>>>>(),
                        It.IsAny<Func<Task<int>>>()))
                .Returns(Task.FromResult(collectionResource));

            var controller = new InsolvencyOrderPersonsController(
                mockDataAccessCradle.Object,
                mockPersonsRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Act
            var result = controller.GetPersonsByInsolvencyIdAsync(insolvencyOrderId).GetAwaiter().GetResult();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(expectedStatusCode, ((OkObjectResult)result).StatusCode);
        }

        /// <summary>
        /// Gettings the persons by insolvency order identifier when there are none to return returns ok status code200.
        /// </summary>
        [TestMethod]
        public void GettingPersonsByInsolvencyOrderId_WhenThereAreNoneToReturn_ReturnsOkStatusCode200()
        {
            // Arrange
            const int insolvencyOrderId = 7654321;
            const int expectedStatusCode = 200;

            var collectionResourceInfo = new CollectionResourceInfo(0, 10, 1);
            var collectionResource =
                new CollectionResource<InsolvencyOrderPersonModel>(collectionResourceInfo, new List<InsolvencyOrderPersonModel>());

            mockDataAccessCradle
                .Setup(cradle =>
                    cradle.GetPagedDataSetAsync(
                        It.IsAny<Func<Task<IEnumerable<InsolvencyOrderPersonModel>>>>(),
                        It.IsAny<Func<Task<int>>>()))
                .Returns(Task.FromResult(collectionResource));

            var controller =
                new InsolvencyOrderPersonsController(
                    mockDataAccessCradle.Object,
                    mockPersonsRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationProvider.Object,
                    mockHalFormatter.Object,
                    mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Act
            var result = controller.GetPersonsByInsolvencyIdAsync(insolvencyOrderId).GetAwaiter().GetResult();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(expectedStatusCode, ((OkObjectResult)result).StatusCode);
        }

        /// <summary>
        /// Gettings the persons by insolvency order identifier when the cradle throws an exception bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GettingPersonsByInsolvencyOrderId_WhenTheCradleThrowsAnException_BubblesUpTheException()
        {
            // Arrange
            const int insolvencyOrderId = 1237654;

            mockDataAccessCradle
                .Setup(cradle =>
                    cradle.GetPagedDataSetAsync(
                        It.IsAny<Func<Task<IEnumerable<InsolvencyOrderPersonModel>>>>(),
                        It.IsAny<Func<Task<int>>>()))
                .Throws<Exception>();

            var controller = new InsolvencyOrderPersonsController(
                mockDataAccessCradle.Object,
                mockPersonsRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Act
            void Result() => controller.GetPersonsByInsolvencyIdAsync(insolvencyOrderId).GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Gettings the persons by insolvency order identifier when there is a problem formatting the response as hal bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GettingPersonsByInsolvencyOrderId_WhenThereIsAProblemFormattingTheResponseAsHal_BubblesUpTheException()
        {
            // Arrange
            const int insolvencyOrderId = 9876543;

            mockHalCollectionFormatter
                .Setup(insolvencyCollectionFormatter =>
                    insolvencyCollectionFormatter.FormatForHal(It.IsAny<CollectionResource<InsolvencyOrderPersonModel>>()))
                .Throws<Exception>();

            var controller =
                new InsolvencyOrderPersonsController(
                    mockDataAccessCradle.Object,
                    mockPersonsRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationProvider.Object,
                    mockHalFormatter.Object,
                    mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Act
            void Result() => controller.GetPersonsByInsolvencyIdAsync(insolvencyOrderId).GetAwaiter().GetResult();

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
                    insolvencyOrdersCradle.GetItemAsync(
                        It.IsAny<Func<Task<InsolvencyOrderPersonModel>>>()))
                .Returns(Task.FromResult(new InsolvencyOrderPersonModel()));

            // Act
            var insolvencyOrdersController = new InsolvencyOrderPersonsController(
                mockDataAccessCradle.Object,
                mockPersonsRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Act
            var result = insolvencyOrdersController.GetPersonsByPersonId(recordId).GetAwaiter().GetResult();

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
                    insolvencyOrdersCradle.GetItemAsync(It.IsAny<Func<Task<InsolvencyOrderPersonModel>>>()))
                .Throws<Exception>();

            // Act
            var insolvencyOrdersController = new InsolvencyOrderPersonsController(
                mockDataAccessCradle.Object,
                mockPersonsRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Act
            void Result() => insolvencyOrdersController.GetPersonsByPersonId(recordId).GetAwaiter().GetResult();

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
                .Setup(insolvencyFormatter =>
                    insolvencyFormatter.FormatForHal(It.IsAny<InsolvencyOrderPersonModel>()))
                .Throws<Exception>();

            // Act
            var insolvencyOrdersController = new InsolvencyOrderPersonsController(
                mockDataAccessCradle.Object,
                mockPersonsRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockHalFormatter.Object,
                mockHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Act
            void Result() => insolvencyOrdersController.GetPersonsByPersonId(recordId).GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Checks the persons controller attribute has authorization attribute and query scope.
        /// </summary>
        [TestMethod]
        [TestCategory("Category [UnitTest - Authorization")]
        public void CheckPersonsControllerAttribute_HasAuthorizationAttributeAndQueryScope()
        {
            // Arrange

            // Act
            var attributes = Attribute.GetCustomAttribute(typeof(InsolvencyOrderPersonsController), typeof(AuthorizeAttribute));
            var authorizeAttribute = attributes as AuthorizeAttribute;

            // Assert
            Assert.IsNotNull(attributes);
            Assert.IsNotNull(authorizeAttribute);
            Assert.AreEqual("Query", authorizeAttribute.Policy);
        }
    }
}
