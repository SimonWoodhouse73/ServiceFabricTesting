// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 03-02-2018
//
// Last Modified By : MartinG
// Last Modified On : 05-18-2018
// ***********************************************************************
// <copyright file="DisputesControllerTests.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>DisputesControllerTests</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.Controllers;
using Api.EntityFramework.Entities;
using Api.Telemetry;
using Callcredit.Domain.Insolvencies.Models;
using Callcredit.Domain.Insolvencies.Repositories;
using Callcredit.Domain.Repositories;
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
    /// Class DisputesControllerTests.
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DisputesControllerTests
    {
        /// <summary>
        /// The mock data access cradle
        /// </summary>
        private Mock<IDataAccessCradle<DisputeModel>> mockDataAccessCradle;

        /// <summary>
        /// The mock dispute repository
        /// </summary>
        private Mock<IDisputesRepository<DisputeModel, DisputeEntity>> mockDisputeRepository;

        /// <summary>
        /// The mock include reader
        /// </summary>
        private Mock<IIncludeReader> mockIncludeReader;

        /// <summary>
        /// The mock page information provider
        /// </summary>
        private Mock<IPageInformationProvider> mockPageInformationProvider;

        /// <summary>
        /// The mock dispute formatter
        /// </summary>
        private Mock<IHalFormatter<DisputeModel>> mockDisputeFormatter;

        /// <summary>
        /// The mock dispute collection formatter
        /// </summary>
        private Mock<IHalCollectionFormatter<DisputeModel>> mockDisputeCollectionFormatter;

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
            this.mockDataAccessCradle = new Mock<IDataAccessCradle<DisputeModel>>();
            this.mockDisputeRepository = new Mock<IDisputesRepository<DisputeModel, DisputeEntity>>();
            this.mockIncludeReader = new Mock<IIncludeReader>();
            this.mockPageInformationProvider = new Mock<IPageInformationProvider>();
            this.mockDisputeFormatter = new Mock<IHalFormatter<DisputeModel>>();
            this.mockDisputeCollectionFormatter = new Mock<IHalCollectionFormatter<DisputeModel>>();
            this.mockTelemetryClient = new Mock<ITelemetryClient>();
        }

        /// <summary>
        /// Constructings the disputes controller with null cradle throws argument null exception.
        /// </summary>
        [TestMethod]
        [TestCategory("Null cradle - throws")]
        public void ConstructingDisputesController_WithNullCradle_ThrowsArgumentNullException()
        {
            // Arrange
            IDataAccessCradle<DisputeModel> cradle = null;

            // Act
            void Constructing() => new DisputesController(
                cradle,
                mockDisputeRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockDisputeFormatter.Object,
                mockDisputeCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the disputes controller with null cradle has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        [TestCategory("Category [UnitTest - Null cradle - correct parameter name in exception]")]
        public void ConstructingDisputesController_WithNullCradle_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "cradle";

            IDataAccessCradle<DisputeModel> cradle = null;

            // Act
            void Constructing() => new DisputesController(
                cradle,
                mockDisputeRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockDisputeFormatter.Object,
                mockDisputeCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the disputes controller with null disputes repository throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingDisputesController_WithNullDisputesRepository_ThrowsArgumentNullException()
        {
            // Arrange
            IDisputesRepository<DisputeModel, DisputeEntity> disputesRepository = null;

            // Act
            void Constructing() => new DisputesController(
                mockDataAccessCradle.Object,
                disputesRepository,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockDisputeFormatter.Object,
                mockDisputeCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the disputes controller with null disputes repository has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingDisputesController_WithNullDisputesRepository_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "disputesRepository";

            IDisputesRepository<DisputeModel, DisputeEntity> disputesRepository = null;

            // Act
            void Constructing() => new DisputesController(
                mockDataAccessCradle.Object,
                disputesRepository,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockDisputeFormatter.Object,
                mockDisputeCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the disputes controller with null include reader throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingDisputesController_WithNullIncludeReader_ThrowsArgumentNullException()
        {
            // Arrange
            IIncludeReader includeReader = null;

            // Act
            void Constructing() => new DisputesController(
                mockDataAccessCradle.Object,
                mockDisputeRepository.Object,
                includeReader,
                mockPageInformationProvider.Object,
                mockDisputeFormatter.Object,
                mockDisputeCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the disputes controller with null include reader has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingDisputesController_WithNullIncludeReader_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "includeReader";

            IIncludeReader includeReader = null;

            // Act
            void Constructing() => new DisputesController(
                mockDataAccessCradle.Object,
                mockDisputeRepository.Object,
                includeReader,
                mockPageInformationProvider.Object,
                mockDisputeFormatter.Object,
                mockDisputeCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the disputes controller with null page information provider throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingDisputesController_WithNullPageInformationProvider_ThrowsArgumentNullException()
        {
            // Arrange
            IPageInformationProvider pageInformationProvider = null;

            // Act
            void Constructing() => new DisputesController(
                mockDataAccessCradle.Object,
                mockDisputeRepository.Object,
                mockIncludeReader.Object,
                pageInformationProvider,
                mockDisputeFormatter.Object,
                mockDisputeCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the disputes controller with null page information provider has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingDisputesController_WithNullPageInformationProvider_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "pageInformationProvider";

            IPageInformationProvider pageInformationProvider = null;

            // Act
            void Constructing() => new DisputesController(
                mockDataAccessCradle.Object,
                mockDisputeRepository.Object,
                mockIncludeReader.Object,
                pageInformationProvider,
                mockDisputeFormatter.Object,
                mockDisputeCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the disputes controller with null dispute formatter throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingDisputesController_WithNullDisputeFormatter_ThrowsArgumentNullException()
        {
            // Arrange
            IHalFormatter<DisputeModel> disputeFormatter = null;

            // Act
            void Constructing() => new DisputesController(
                mockDataAccessCradle.Object,
                mockDisputeRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                disputeFormatter,
                mockDisputeCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the disputes controller with null dispute formatter has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingDisputesController_WithNullDisputeFormatter_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "disputeFormatter";

            IHalFormatter<DisputeModel> disputeFormatter = null;

            // Act
            void Constructing() => new DisputesController(
                mockDataAccessCradle.Object,
                mockDisputeRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                disputeFormatter,
                mockDisputeCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the disputes controller with null dispute collection formatter throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingDisputesController_WithNullDisputeCollectionFormatter_ThrowsArgumentNullException()
        {
            // Arrange
            IHalCollectionFormatter<DisputeModel> disputeCollectionFormatter = null;

            // Act
            void Constructing() => new DisputesController(
                mockDataAccessCradle.Object,
                mockDisputeRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockDisputeFormatter.Object,
                disputeCollectionFormatter,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the disputes controller with null dispute collection formatter has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingDisputesController_WithNullDisputeCollectionFormatter_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "disputeCollectionFormatter";

            IHalCollectionFormatter<DisputeModel> disputeCollectionFormatter = null;

            // Act
            void Constructing() => new DisputesController(
                mockDataAccessCradle.Object,
                mockDisputeRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockDisputeFormatter.Object,
                disputeCollectionFormatter,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the disputes controller with null dispute collection formatter throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingDisputesController_WithNullTelemetryClient_ThrowsArgumentNullException()
        {
            // Arrange
            // Act
            void Constructing() => new DisputesController(
                mockDataAccessCradle.Object,
                mockDisputeRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockDisputeFormatter.Object,
                mockDisputeCollectionFormatter.Object,
                null);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the disputes controller with null dispute collection formatter has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingDisputesController_WithNullTelemetryClient_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "telemetryClient";

            // Act
            void Constructing() => new DisputesController(
                mockDataAccessCradle.Object,
                mockDisputeRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationProvider.Object,
                mockDisputeFormatter.Object,
                mockDisputeCollectionFormatter.Object,
                null);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Gettings the disputes by insolvency identifier when there are some returns ok status code200.
        /// </summary>
        [TestMethod]
        public void GettingDisputesByInsolvencyId_WhenThereAreSome_ReturnsOkStatusCode200()
        {
            // Arrange
            const int insolvencyOrderId = 1234567;
            const int expectedStatusCode = 200;

            var collectionResourceInfo = new CollectionResourceInfo(0, 10, 1);
            var collectionResource =
                new CollectionResource<DisputeModel>(
                    collectionResourceInfo,
                    TestFixture.CreateMany<DisputeModel>(10).ToList());

            mockDataAccessCradle
                .Setup(dataAccessCradle =>
                dataAccessCradle.GetPagedDataSetAsync(
                    It.IsAny<Func<Task<IEnumerable<DisputeModel>>>>(),
                    It.IsAny<Func<Task<int>>>()))
                .Returns(Task.FromResult(collectionResource));

            var controller =
                new DisputesController(
                    mockDataAccessCradle.Object,
                    mockDisputeRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationProvider.Object,
                    mockDisputeFormatter.Object,
                    mockDisputeCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Act
            var result = controller.GetDisputesByInsolvencyIdAsync(insolvencyOrderId).GetAwaiter().GetResult();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(expectedStatusCode, ((OkObjectResult)result).StatusCode);
        }

        /// <summary>
        /// Gettings the disputes by insolvency identifier when there are none to return returns ok status code200.
        /// </summary>
        [TestMethod]
        public void GettingDisputesByInsolvencyId_WhenThereAreNoneToReturn_ReturnsOkStatusCode200()
        {
            // Arrange
            const int insolvencyOrderId = 1234567;
            const int expectedStatusCode = 200;

            var collectionResourceInfo = new CollectionResourceInfo(0, 10, 1);
            var collectionResource =
                new CollectionResource<DisputeModel>(collectionResourceInfo, new List<DisputeModel>());

            mockDataAccessCradle
                .Setup(dataAccessCradle =>
                    dataAccessCradle.GetPagedDataSetAsync(
                        It.IsAny<Func<Task<IEnumerable<DisputeModel>>>>(),
                        It.IsAny<Func<Task<int>>>()))
                .Returns(Task.FromResult(collectionResource));

            var controller =
                new DisputesController(
                    mockDataAccessCradle.Object,
                    mockDisputeRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationProvider.Object,
                    mockDisputeFormatter.Object,
                    mockDisputeCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Act
            var result = controller.GetDisputesByInsolvencyIdAsync(insolvencyOrderId).GetAwaiter().GetResult();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(expectedStatusCode, ((OkObjectResult)result).StatusCode);
        }

        /// <summary>
        /// Gettings the disputes by insolvency identifier when the cradle throws an exception bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GettingDisputesByInsolvencyId_WhenTheCradleThrowsAnException_BubblesUpTheException()
        {
            // Arrange
            const int insolvencyOrderId = 1234567;

            mockDataAccessCradle
                .Setup(
                    dataAccessCradle =>
                    dataAccessCradle.GetPagedDataSetAsync(
                        It.IsAny<Func<Task<IEnumerable<DisputeModel>>>>(),
                        It.IsAny<Func<Task<int>>>()))
                .Throws<Exception>();

            var controller =
                new DisputesController(
                    mockDataAccessCradle.Object,
                    mockDisputeRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationProvider.Object,
                    mockDisputeFormatter.Object,
                    mockDisputeCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Act
            void Result() => controller.GetDisputesByInsolvencyIdAsync(insolvencyOrderId).GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Gettings the disputes by insolvency identifier when there is a problem getting the includes bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GettingDisputesByInsolvencyId_WhenThereIsAProblemGettingTheIncludes_BubblesUpTheException()
        {
            // Arrange
            const int insolvencyOrderId = 1234567;

            mockIncludeReader
                .Setup(includeReader => includeReader.GetIncludes())
                .Throws<Exception>();

            var controller =
                new DisputesController(
                    mockDataAccessCradle.Object,
                    mockDisputeRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationProvider.Object,
                    mockDisputeFormatter.Object,
                    mockDisputeCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Act
            void Result() => controller.GetDisputesByInsolvencyIdAsync(insolvencyOrderId).GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Gettings the disputes by insolvency identifier when there is a problem getting the page information bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GettingDisputesByInsolvencyId_WhenThereIsAProblemGettingThePageInformation_BubblesUpTheException()
        {
            // Arrange
            const int insolvencyOrderId = 1234567;

            mockPageInformationProvider
                .Setup(pageInformationReader => pageInformationReader.GetPageInformation())
                .Throws<Exception>();

            var controller =
                new DisputesController(
                    mockDataAccessCradle.Object,
                    mockDisputeRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationProvider.Object,
                    mockDisputeFormatter.Object,
                    mockDisputeCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Act
            void Result() => controller.GetDisputesByInsolvencyIdAsync(insolvencyOrderId).GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Gettings the disputes by insolvency identifier when there is a problem formatting the response as hal bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GettingDisputesByInsolvencyId_WhenThereIsAProblemFormattingTheResponseAsHal_BubblesUpTheException()
        {
            // Arrange
            const int insolvencyOrderId = 1234567;

            mockDisputeCollectionFormatter
                .Setup(disputeCollectionFormatter =>
                    disputeCollectionFormatter.FormatForHal(It.IsAny<CollectionResource<DisputeModel>>()))
                .Throws<Exception>();

            var controller =
                new DisputesController(
                    mockDataAccessCradle.Object,
                    mockDisputeRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationProvider.Object,
                    mockDisputeFormatter.Object,
                    mockDisputeCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Act
            void Result() => controller.GetDisputesByInsolvencyIdAsync(insolvencyOrderId).GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Gettings a single dispute by its identifier when it exists returns ok status code200.
        /// </summary>
        [TestMethod]
        public void GettingASingleDisputeByItsId_WhenItExists_ReturnsOkStatusCode200()
        {
            // Arrange
            const int disputeId = 1234567;
            const int expectedStatusCode = 200;

            var dispute = TestFixture.Create<DisputeModel>();

            mockDataAccessCradle
                .Setup(dataAccessCradle => dataAccessCradle.GetItemAsync(It.IsAny<Func<Task<DisputeModel>>>()))
                .Returns(Task.FromResult(dispute));

            var controller =
                new DisputesController(
                    mockDataAccessCradle.Object,
                    mockDisputeRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationProvider.Object,
                    mockDisputeFormatter.Object,
                    mockDisputeCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Act
            var result = controller.GetDisputesByDisputeId(disputeId).GetAwaiter().GetResult();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(expectedStatusCode, ((OkObjectResult)result).StatusCode);
        }

        /// <summary>
        /// Gettings a single dispute by its identifier whenit doesnt exist bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GettingASingleDisputeByItsId_WhenitDoesntExist_BubblesUpTheException()
        {
            // Arrange
            const int disputeId = 1234567;
            const int expectedStatusCode = 200;

            mockDisputeRepository
                .Setup(
                    disputeRepository =>
                    disputeRepository.GetResultsByAsync(
                        It.IsAny<int>(),
                        It.IsAny<Expression<Func<DisputeEntity, int>>>(),
                        It.IsAny<PageInformation>()))
                .Throws<Exception>();

            var controller =
                new DisputesController(
                    mockDataAccessCradle.Object,
                    mockDisputeRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationProvider.Object,
                    mockDisputeFormatter.Object,
                    mockDisputeCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Act
            var result = controller.GetDisputesByDisputeId(disputeId).GetAwaiter().GetResult();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(expectedStatusCode, ((OkObjectResult)result).StatusCode);
        }

        /// <summary>
        /// Gettings a single dispute by its identifier when the cradle throws an exception bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GettingASingleDisputeByItsId_WhenTheCradleThrowsAnException_BubblesUpTheException()
        {
            // Arrange
            const int disputeId = 1234567;

            mockDataAccessCradle
                .Setup(dataAccessCradle =>
                    dataAccessCradle.GetItemAsync(It.IsAny<Func<Task<DisputeModel>>>()))
                .Throws<Exception>();

            var controller =
                new DisputesController(
                    mockDataAccessCradle.Object,
                    mockDisputeRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationProvider.Object,
                    mockDisputeFormatter.Object,
                    mockDisputeCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Act
            void Result() => controller.GetDisputesByDisputeId(disputeId).GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Gettings a single dispute by its identifier when there is a problem getting the includes bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GettingASingleDisputeByItsId_WhenThereIsAProblemGettingTheIncludes_BubblesUpTheException()
        {
            // Arrange
            const int disputeId = 1234567;

            mockIncludeReader
                .Setup(includeReader => includeReader.GetIncludes())
                .Throws<Exception>();

            var controller =
                new DisputesController(
                    mockDataAccessCradle.Object,
                    mockDisputeRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationProvider.Object,
                    mockDisputeFormatter.Object,
                    mockDisputeCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Act
            void Result() => controller.GetDisputesByDisputeId(disputeId).GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Gettings a single dispute by its identifier when there is a problem formatting the response as hal bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GettingASingleDisputeByItsId_WhenThereIsAProblemFormattingTheResponseAsHal_BubblesUpTheException()
        {
            // Arrange
            const int disputeId = 1234567;

            mockDisputeFormatter
                .Setup(disputeFormatter => disputeFormatter.FormatForHal(It.IsAny<DisputeModel>()))
                .Throws<Exception>();

            var controller =
                new DisputesController(
                    mockDataAccessCradle.Object,
                    mockDisputeRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationProvider.Object,
                    mockDisputeFormatter.Object,
                    mockDisputeCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Act
            void Result() => controller.GetDisputesByDisputeId(disputeId).GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Checks the dispute controller attribute has authorization attribute and query scope.
        /// </summary>
        [TestMethod]
        [TestCategory("Category [UnitTest - Authorization")]
        public void CheckDisputeControllerAttribute_HasAuthorizationAttributeAndQueryScope()
        {
            // Arrange

            // Act
            var attributes = Attribute.GetCustomAttribute(typeof(DisputesController), typeof(AuthorizeAttribute));
            var authorizeAttribute = attributes as AuthorizeAttribute;

            // Assert
            Assert.IsNotNull(attributes);
            Assert.IsNotNull(authorizeAttribute);
            Assert.AreEqual("Query", authorizeAttribute.Policy);
        }
    }
}
