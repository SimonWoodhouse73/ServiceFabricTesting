// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 03-02-2018
//
// Last Modified By : MartinG
// Last Modified On : 05-18-2018
// ***********************************************************************
// <copyright file="InsolvencyOrderControllerTests.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>Defines unit tests for the InsolvencyOrderController URIs.</summary>
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
    /// Class InsolvencyOrderControllerTests.
    /// </summary>
    [TestClass]
    public class InsolvencyOrderControllerTests
    {
        /// <summary>
        /// The mock data access cradle
        /// </summary>
        private Mock<IDataAccessCradle<InsolvencyOrderModel>> mockDataAccessCradle;

        /// <summary>
        /// The mock insolvency orders repository
        /// </summary>
        private Mock<IInsolvencyOrdersRepository<InsolvencyOrderModel, InsolvencyOrderEntity>> mockInsolvencyOrdersRepository;

        /// <summary>
        /// The mock insolvency orders flattened repository
        /// </summary>
        private Mock<IInsolvencyOrdersRepository<InsolvencyOrderModel, InsolvencyOrderFlattenedEntity>> mockInsolvencyOrdersFlattenedRepository;

        /// <summary>
        /// The mock include reader
        /// </summary>
        private Mock<IIncludeReader> mockIncludeReader;

        /// <summary>
        /// The mock page information reader
        /// </summary>
        private Mock<IPageInformationProvider> mockPageInformationReader;

        /// <summary>
        /// The mock insolvencies hal formatter
        /// </summary>
        private Mock<IHalFormatter<InsolvencyOrderModel>> mockInsolvenciesHalFormatter;

        /// <summary>
        /// The mock insolvencies hal collection formatter
        /// </summary>
        private Mock<IHalCollectionFormatter<InsolvencyOrderModel>> mockInsolvenciesHalCollectionFormatter;

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
            this.mockDataAccessCradle = new Mock<IDataAccessCradle<InsolvencyOrderModel>>();
            this.mockInsolvencyOrdersRepository = new Mock<IInsolvencyOrdersRepository<InsolvencyOrderModel, InsolvencyOrderEntity>>();
            this.mockInsolvencyOrdersFlattenedRepository = new Mock<IInsolvencyOrdersRepository<InsolvencyOrderModel, InsolvencyOrderFlattenedEntity>>();
            this.mockIncludeReader = new Mock<IIncludeReader>();
            this.mockPageInformationReader = new Mock<IPageInformationProvider>();
            this.mockInsolvenciesHalFormatter = new Mock<IHalFormatter<InsolvencyOrderModel>>();
            this.mockInsolvenciesHalCollectionFormatter = new Mock<IHalCollectionFormatter<InsolvencyOrderModel>>();
            this.mockTelemetryClient = new Mock<ITelemetryClient>();
        }

        /// <summary>
        /// Gettings the insolvency order entitys by residence identifier when there are some returns ok status code200.
        /// </summary>
        /// <param name="residenceId">The residence identifier.</param>
        [TestMethod]
        [DataRow(1234567, DisplayName = "With positive value residence ID.")]
        [DataRow(-572810, DisplayName = "With negative value residence ID.")]
        public void GettingInsolvencyOrderEntitysByResidenceId_WhenThereAreSome_ReturnsOkStatusCode200(int residenceId)
        {
            // Arrange
            const int expectedStatusCode = 200;

            var collectionResourceInfo = new CollectionResourceInfo(0, 10, 1);
            var collectionResource =
                new CollectionResource<InsolvencyOrderModel>(collectionResourceInfo, TestFixture.CreateMany<InsolvencyOrderModel>(10).ToList());

            mockDataAccessCradle
                .Setup(cradle =>
                    cradle.GetPagedDataSetAsync(
                        It.IsAny<Func<Task<IEnumerable<InsolvencyOrderModel>>>>(),
                        It.IsAny<Func<Task<int>>>()))
                .Returns(Task.FromResult(collectionResource));

            var controller =
                new InsolvencyOrdersController(
                    mockDataAccessCradle.Object,
                    mockInsolvencyOrdersRepository.Object,
                    mockInsolvencyOrdersFlattenedRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationReader.Object,
                    mockInsolvenciesHalFormatter.Object,
                    mockInsolvenciesHalCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Act
            var result = controller.GetInsolvencyOrdersByResidenceId(residenceId).GetAwaiter().GetResult();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(expectedStatusCode, ((OkObjectResult)result).StatusCode);
        }

        /// <summary>
        /// Gettings the insolvency order entitys by residence identifier when there are none to return returns ok status code200.
        /// </summary>
        [TestMethod]
        public void GettingInsolvencyOrderEntitysByResidenceId_WhenThereAreNoneToReturn_ReturnsOkStatusCode200()
        {
            // Arrange
            const int residenceId = 1234567;
            const int expectedStatusCode = 200;

            var collectionResourceInfo = new CollectionResourceInfo(0, 10, 1);
            var collectionResource =
                new CollectionResource<InsolvencyOrderModel>(collectionResourceInfo, new List<InsolvencyOrderModel>());

            mockDataAccessCradle
                .Setup(cradle =>
                    cradle.GetPagedDataSetAsync(
                        It.IsAny<Func<Task<IEnumerable<InsolvencyOrderModel>>>>(),
                        It.IsAny<Func<Task<int>>>()))
                .Returns(Task.FromResult(collectionResource));

            var controller =
                new InsolvencyOrdersController(
                    mockDataAccessCradle.Object,
                    mockInsolvencyOrdersRepository.Object,
                    mockInsolvencyOrdersFlattenedRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationReader.Object,
                    mockInsolvenciesHalFormatter.Object,
                    mockInsolvenciesHalCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Act
            var result = controller.GetInsolvencyOrdersByResidenceId(residenceId).GetAwaiter().GetResult();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(expectedStatusCode, ((OkObjectResult)result).StatusCode);
        }

        /// <summary>
        /// Gettings the insolvency order entitys by residence identifier when the cradle throws an exception bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GettingInsolvencyOrderEntitysByResidenceId_WhenTheCradleThrowsAnException_BubblesUpTheException()
        {
            // Arrange
            const int residenceId = 1234567;

            mockDataAccessCradle
                .Setup(cradle =>
                    cradle.GetPagedDataSetAsync(
                        It.IsAny<Func<Task<IEnumerable<InsolvencyOrderModel>>>>(),
                        It.IsAny<Func<Task<int>>>()))
                .Throws<Exception>();

            var controller =
                new InsolvencyOrdersController(
                    mockDataAccessCradle.Object,
                    mockInsolvencyOrdersRepository.Object,
                    mockInsolvencyOrdersFlattenedRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationReader.Object,
                    mockInsolvenciesHalFormatter.Object,
                    mockInsolvenciesHalCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Act
            void Result() => controller.GetInsolvencyOrdersByResidenceId(residenceId).GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Gettings the insolvency order entitys by residence identifier when there is a problem getting the includes bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GettingInsolvencyOrderEntitysByResidenceId_WhenThereIsAProblemGettingTheIncludes_BubblesUpTheException()
        {
            // Arrange
            const int residenceId = 1234567;

            mockIncludeReader
                .Setup(includeReader => includeReader.GetIncludes())
                .Throws<Exception>();

            var controller =
                new InsolvencyOrdersController(
                    mockDataAccessCradle.Object,
                    mockInsolvencyOrdersRepository.Object,
                    mockInsolvencyOrdersFlattenedRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationReader.Object,
                    mockInsolvenciesHalFormatter.Object,
                    mockInsolvenciesHalCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Act
            void Result() => controller.GetInsolvencyOrdersByResidenceId(residenceId).GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Gettings the insolvency order entitys by residence identifier when there is a problem getting the page information bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GettingInsolvencyOrderEntitysByResidenceId_WhenThereIsAProblemGettingThePageInformation_BubblesUpTheException()
        {
            // Arrange
            const int residenceId = 1234567;

            mockPageInformationReader
                .Setup(pageInformationReader => pageInformationReader.GetPageInformation())
                .Throws<Exception>();

            var controller =
                new InsolvencyOrdersController(
                    mockDataAccessCradle.Object,
                    mockInsolvencyOrdersRepository.Object,
                    mockInsolvencyOrdersFlattenedRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationReader.Object,
                    mockInsolvenciesHalFormatter.Object,
                    mockInsolvenciesHalCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Act
            void Result() => controller.GetInsolvencyOrdersByResidenceId(residenceId).GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Gettings the insolvency order entitys by residence identifier when there is a problem formatting the response as hal bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GettingInsolvencyOrderEntitysByResidenceId_WhenThereIsAProblemFormattingTheResponseAsHal_BubblesUpTheException()
        {
            // Arrange
            const int residenceId = 1234567;

            mockInsolvenciesHalCollectionFormatter
                .Setup(insolvencyCollectionFormatter => insolvencyCollectionFormatter.FormatForHal(It.IsAny<CollectionResource<InsolvencyOrderModel>>()))
                .Throws<Exception>();

            var controller =
                new InsolvencyOrdersController(
                    mockDataAccessCradle.Object,
                    mockInsolvencyOrdersRepository.Object,
                    mockInsolvencyOrdersFlattenedRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationReader.Object,
                    mockInsolvenciesHalFormatter.Object,
                    mockInsolvenciesHalCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Act
            void Result() => controller.GetInsolvencyOrdersByResidenceId(residenceId).GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Constructings the insolvencies controller with null cradle throws argument null exception.
        /// </summary>
        [TestMethod]
        [TestCategory("Null cradle - throws")]
        public void ConstructingInsolvenciesController_WithNullCradle_ThrowsArgumentNullException()
        {
            // Arrange
            IDataAccessCradle<InsolvencyOrderModel> cradle = null;

            // Act
            void Constructing() => new InsolvencyOrdersController(
                cradle,
                mockInsolvencyOrdersRepository.Object,
                mockInsolvencyOrdersFlattenedRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationReader.Object,
                mockInsolvenciesHalFormatter.Object,
                mockInsolvenciesHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the insolvencies controller with null cradle has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        [TestCategory("Category [UnitTest - Null cradle - correct parameter name in exception]")]
        public void ConstructingInsolvenciesController_WithNullCradle_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "cradle";

            IDataAccessCradle<InsolvencyOrderModel> cradle = null;

            // Act
            void Constructing() =>
                new InsolvencyOrdersController(
                    cradle,
                    mockInsolvencyOrdersRepository.Object,
                    mockInsolvencyOrdersFlattenedRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationReader.Object,
                    mockInsolvenciesHalFormatter.Object,
                    mockInsolvenciesHalCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the insolvencies controller with null insolvencies repository throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingInsolvenciesController_WithNullInsolvenciesRepository_ThrowsArgumentNullException()
        {
            // Arrange
            IInsolvencyOrdersRepository<InsolvencyOrderModel, InsolvencyOrderEntity> insolvencyOrdersRepository = null;

            // Act
            void Constructing() =>
                new InsolvencyOrdersController(
                    mockDataAccessCradle.Object,
                    insolvencyOrdersRepository,
                    mockInsolvencyOrdersFlattenedRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationReader.Object,
                    mockInsolvenciesHalFormatter.Object,
                    mockInsolvenciesHalCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the insolvencies controller with null insolvencies repository has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingInsolvenciesController_WithNullInsolvenciesRepository_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "insolvencyOrdersRepository";

            IInsolvencyOrdersRepository<InsolvencyOrderModel, InsolvencyOrderEntity> insolvencyOrdersRepository = null;

            // Act
            void Constructing() =>
                new InsolvencyOrdersController(
                    mockDataAccessCradle.Object,
                    insolvencyOrdersRepository,
                    mockInsolvencyOrdersFlattenedRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationReader.Object,
                    mockInsolvenciesHalFormatter.Object,
                    mockInsolvenciesHalCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the insolvencies controller with null insolvencies flattened repository throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingInsolvenciesController_WithNullInsolvenciesFlattenedRepository_ThrowsArgumentNullException()
        {
            // Arrange
            // Act
            void Constructing() =>
                new InsolvencyOrdersController(
                    mockDataAccessCradle.Object,
                    mockInsolvencyOrdersRepository.Object,
                    null,
                    mockIncludeReader.Object,
                    mockPageInformationReader.Object,
                    mockInsolvenciesHalFormatter.Object,
                    mockInsolvenciesHalCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the insolvencies controller with null insolvencies flattened repository has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingInsolvenciesController_WithNullInsolvenciesFlattenedRepository_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "insolvencyOrdersFlattenedRepository";

            // Act
            void Constructing() =>
                new InsolvencyOrdersController(
                    mockDataAccessCradle.Object,
                    mockInsolvencyOrdersRepository.Object,
                    null,
                    mockIncludeReader.Object,
                    mockPageInformationReader.Object,
                    mockInsolvenciesHalFormatter.Object,
                    mockInsolvenciesHalCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the insolvencies controller with null include reader throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingInsolvenciesController_WithNullIncludeReader_ThrowsArgumentNullException()
        {
            // Arrange
            IIncludeReader includeReader = null;

            // Act
            void Constructing() => new InsolvencyOrdersController(
                mockDataAccessCradle.Object,
                mockInsolvencyOrdersRepository.Object,
                mockInsolvencyOrdersFlattenedRepository.Object,
                includeReader,
                mockPageInformationReader.Object,
                mockInsolvenciesHalFormatter.Object,
                mockInsolvenciesHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the insolvencies controller with null include reader has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingInsolvenciesController_WithNullIncludeReader_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "includeReader";

            IIncludeReader includeReader = null;

            // Act
            void Constructing() => new InsolvencyOrdersController(
                mockDataAccessCradle.Object,
                mockInsolvencyOrdersRepository.Object,
                mockInsolvencyOrdersFlattenedRepository.Object,
                includeReader,
                mockPageInformationReader.Object,
                mockInsolvenciesHalFormatter.Object,
                mockInsolvenciesHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the insolvencies controller with null page information provider throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingInsolvenciesController_WithNullPageInformationProvider_ThrowsArgumentNullException()
        {
            // Arrange
            IPageInformationProvider pageInformationProvider = null;

            // Act
            void Constructing() => new InsolvencyOrdersController(
                mockDataAccessCradle.Object,
                mockInsolvencyOrdersRepository.Object,
                mockInsolvencyOrdersFlattenedRepository.Object,
                mockIncludeReader.Object,
                pageInformationProvider,
                mockInsolvenciesHalFormatter.Object,
                mockInsolvenciesHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the insolvencies controller with null page information provider has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingInsolvenciesController_WithNullPageInformationProvider_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "pageInformationProvider";

            IPageInformationProvider pageInformationProvider = null;

            // Act
            void Constructing() => new InsolvencyOrdersController(
                mockDataAccessCradle.Object,
                mockInsolvencyOrdersRepository.Object,
                mockInsolvencyOrdersFlattenedRepository.Object,
                mockIncludeReader.Object,
                pageInformationProvider,
                mockInsolvenciesHalFormatter.Object,
                mockInsolvenciesHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the insolvencies controller with null insolvencies formatter throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingInsolvenciesController_WithNullInsolvenciesFormatter_ThrowsArgumentNullException()
        {
            // Arrange
            IHalFormatter<InsolvencyOrderModel> halFormatter = null;

            // Act
            void Constructing() => new InsolvencyOrdersController(
                mockDataAccessCradle.Object,
                mockInsolvencyOrdersRepository.Object,
                mockInsolvencyOrdersFlattenedRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationReader.Object,
                halFormatter,
                mockInsolvenciesHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the insolvencies controller with null insolvencies formatter has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingInsolvenciesController_WithNullInsolvenciesFormatter_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "insolvencyOrdersFormatter";

            IHalFormatter<InsolvencyOrderModel> halFormatter = null;

            // Act
            void Constructing() => new InsolvencyOrdersController(
                mockDataAccessCradle.Object,
                mockInsolvencyOrdersRepository.Object,
                mockInsolvencyOrdersFlattenedRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationReader.Object,
                halFormatter,
                mockInsolvenciesHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the insolvencies controller with null insolvencies collection formatter throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingInsolvenciesController_WithNullInsolvenciesCollectionFormatter_ThrowsArgumentNullException()
        {
            // Arrange
            IHalCollectionFormatter<InsolvencyOrderModel> halCollectionFormatter = null;

            // Act
            void Constructing() => new InsolvencyOrdersController(
                mockDataAccessCradle.Object,
                mockInsolvencyOrdersRepository.Object,
                mockInsolvencyOrdersFlattenedRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationReader.Object,
                mockInsolvenciesHalFormatter.Object,
                halCollectionFormatter,
                mockTelemetryClient.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the insolvencies controller with null insolvencies collection formatter has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingInsolvenciesController_WithNullInsolvenciesCollectionFormatter_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "insolvencyOrdersCollectionFormatter";

            IHalCollectionFormatter<InsolvencyOrderModel> halCollectionFormatter = null;

            // Act
            void Constructing() => new InsolvencyOrdersController(
                mockDataAccessCradle.Object,
                mockInsolvencyOrdersRepository.Object,
                mockInsolvencyOrdersFlattenedRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationReader.Object,
                mockInsolvenciesHalFormatter.Object,
                halCollectionFormatter,
                mockTelemetryClient.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Constructings the insolvencies controller with null telemetry client throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingInsolvenciesController_WithNullTelemetryClient_ThrowsArgumentNullException()
        {
            // Arrange
            // Act
            void Constructing() => new InsolvencyOrdersController(
                                                                  mockDataAccessCradle.Object,
                                                                  mockInsolvencyOrdersRepository.Object,
                                                                  mockInsolvencyOrdersFlattenedRepository.Object,
                                                                  mockIncludeReader.Object,
                                                                  mockPageInformationReader.Object,
                                                                  mockInsolvenciesHalFormatter.Object,
                                                                  mockInsolvenciesHalCollectionFormatter.Object,
                                                                  null);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)Constructing);
        }

        /// <summary>
        /// Constructings the insolvencies controller with null telemetry client has correct parameter name for argument exception.
        /// </summary>
        [TestMethod]
        public void ConstructingInsolvenciesController_WithNullTelemetryClient_HasCorrectParameterNameForArgumentException()
        {
            // Arrange
            const string expectedParameterName = "telemetryClient";

            // Act
            void Constructing() => new InsolvencyOrdersController(
                mockDataAccessCradle.Object,
                mockInsolvencyOrdersRepository.Object,
                mockInsolvencyOrdersFlattenedRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationReader.Object,
                mockInsolvenciesHalFormatter.Object,
                mockInsolvenciesHalCollectionFormatter.Object,
                null);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(Constructing, expectedParameterName);
        }

        /// <summary>
        /// Gets the insolvency record by identifier when there is a record returns status code200.
        /// </summary>
        [TestMethod]
        public void GetInsolvencyRecordById_WhenThereIsARecord_ReturnsStatusCode200()
        {
            // Arrange
            const int expectedStatusCode = 200;
            const int recordId = 1;

            mockDataAccessCradle
                .Setup(insolvencyOrdersCradle => insolvencyOrdersCradle.GetItemAsync(It.IsAny<Func<Task<InsolvencyOrderModel>>>()))
                .Returns(Task.FromResult(new InsolvencyOrderModel()));

            // Act
            var insolvencyOrdersController = new InsolvencyOrdersController(
                mockDataAccessCradle.Object,
                mockInsolvencyOrdersRepository.Object,
                mockInsolvencyOrdersFlattenedRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationReader.Object,
                mockInsolvenciesHalFormatter.Object,
                mockInsolvenciesHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Act
            var result = insolvencyOrdersController.GetInsolvencyOrderById(recordId).GetAwaiter().GetResult();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(expectedStatusCode, ((OkObjectResult)result).StatusCode);
        }

        /// <summary>
        /// Gets the insolvency record by identifier when the cradle throws an exception bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GetInsolvencyRecordById_WhenTheCradleThrowsAnException_BubblesUpTheException()
        {
            // Arrange
            const int recordId = 1;

            mockDataAccessCradle
                .Setup(insolvencyOrdersCradle => insolvencyOrdersCradle.GetItemAsync(It.IsAny<Func<Task<InsolvencyOrderModel>>>()))
                .Throws<Exception>();

            // Act
            var insolvencyOrdersController = new InsolvencyOrdersController(
                mockDataAccessCradle.Object,
                mockInsolvencyOrdersRepository.Object,
                mockInsolvencyOrdersFlattenedRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationReader.Object,
                mockInsolvenciesHalFormatter.Object,
                mockInsolvenciesHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Act
            void Result() => insolvencyOrdersController.GetInsolvencyOrderById(recordId).GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Gets the insolvency record by identifier when there is a problem formatting the response as hal bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GetInsolvencyRecordById_WhenThereIsAProblemFormattingTheResponseAsHal_BubblesUpTheException()
        {
            // Arrange
            const int recordId = 1;

            mockInsolvenciesHalFormatter
                .Setup(insolvencyFormatter => insolvencyFormatter.FormatForHal(It.IsAny<InsolvencyOrderModel>()))
                .Throws<Exception>();

            // Act
            var insolvencyOrdersController = new InsolvencyOrdersController(
                mockDataAccessCradle.Object,
                mockInsolvencyOrdersRepository.Object,
                mockInsolvencyOrdersFlattenedRepository.Object,
                mockIncludeReader.Object,
                mockPageInformationReader.Object,
                mockInsolvenciesHalFormatter.Object,
                mockInsolvenciesHalCollectionFormatter.Object,
                mockTelemetryClient.Object);

            // Act
            void Result() => insolvencyOrdersController.GetInsolvencyOrderById(recordId).GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Gets all insolvency records where there are some returns ok status code200.
        /// </summary>
        [TestMethod]
        public void GetAllInsolvencyRecords_WhereThereAreSome_ReturnsOkStatusCode200()
        {
            // Arrange
            const int expectedStatusCode = 200;

            var collectionResourceInfo = new CollectionResourceInfo(0, 10, 1);
            var collectionResource =
                new CollectionResource<InsolvencyOrderModel>(collectionResourceInfo, TestFixture.CreateMany<InsolvencyOrderModel>(10).ToList());

            mockDataAccessCradle
                .Setup(cradle =>
                    cradle.GetPagedDataSetAsync(
                        It.IsAny<Func<Task<IEnumerable<InsolvencyOrderModel>>>>(),
                        It.IsAny<Func<Task<int>>>()))
                .Returns(Task.FromResult(collectionResource));

            var controller =
                new InsolvencyOrdersController(
                    mockDataAccessCradle.Object,
                    mockInsolvencyOrdersRepository.Object,
                    mockInsolvencyOrdersFlattenedRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationReader.Object,
                    mockInsolvenciesHalFormatter.Object,
                    mockInsolvenciesHalCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Act
            var result = controller.GetAllInsolvencyOrders().GetAwaiter().GetResult();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(expectedStatusCode, ((OkObjectResult)result).StatusCode);
        }

        /// <summary>
        /// Gets all insolvency records where there are none to return returns ok status code200.
        /// </summary>
        [TestMethod]
        public void GetAllInsolvencyRecords_WhereThereAreNoneToReturn_ReturnsOkStatusCode200()
        {
            // Arrange
            const int expectedStatusCode = 200;

            var collectionResourceInfo = new CollectionResourceInfo(0, 10, 1);
            var collectionResource =
                new CollectionResource<InsolvencyOrderModel>(collectionResourceInfo, new List<InsolvencyOrderModel>());

            mockDataAccessCradle
                .Setup(cradle =>
                    cradle.GetPagedDataSetAsync(
                        It.IsAny<Func<Task<IEnumerable<InsolvencyOrderModel>>>>(),
                        It.IsAny<Func<Task<int>>>()))
                .Returns(Task.FromResult(collectionResource));

            var controller =
                new InsolvencyOrdersController(
                    mockDataAccessCradle.Object,
                    mockInsolvencyOrdersRepository.Object,
                    mockInsolvencyOrdersFlattenedRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationReader.Object,
                    mockInsolvenciesHalFormatter.Object,
                    mockInsolvenciesHalCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Act
            var result = controller.GetAllInsolvencyOrders().GetAwaiter().GetResult();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(expectedStatusCode, ((OkObjectResult)result).StatusCode);
        }

        /// <summary>
        /// Gets all insolvencies records when the cradle throws an exception bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GetAllInsolvenciesRecords_WhenTheCradleThrowsAnException_BubblesUpTheException()
        {
            mockDataAccessCradle
                .Setup(cradle =>
                    cradle.GetPagedDataSetAsync(
                        It.IsAny<Func<Task<IEnumerable<InsolvencyOrderModel>>>>(),
                        It.IsAny<Func<Task<int>>>()))
                .Throws<Exception>();

            var controller =
                new InsolvencyOrdersController(
                    mockDataAccessCradle.Object,
                    mockInsolvencyOrdersRepository.Object,
                    mockInsolvencyOrdersFlattenedRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationReader.Object,
                    mockInsolvenciesHalFormatter.Object,
                    mockInsolvenciesHalCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Act
            void Result() => controller.GetAllInsolvencyOrders().GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Gets all insolvencies records when there is a problem getting the includes bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GetAllInsolvenciesRecords_WhenThereIsAProblemGettingTheIncludes_BubblesUpTheException()
        {
            mockIncludeReader
                .Setup(includeReader => includeReader.GetIncludes())
                .Throws<Exception>();

            var controller =
                new InsolvencyOrdersController(
                    mockDataAccessCradle.Object,
                    mockInsolvencyOrdersRepository.Object,
                    mockInsolvencyOrdersFlattenedRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationReader.Object,
                    mockInsolvenciesHalFormatter.Object,
                    mockInsolvenciesHalCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Act
            void Result() => controller.GetAllInsolvencyOrders().GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Gets all insolvency records when there is a problem getting the page information bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GetAllInsolvencyRecords_WhenThereIsAProblemGettingThePageInformation_BubblesUpTheException()
        {
            mockPageInformationReader
                .Setup(pageInformationReader => pageInformationReader.GetPageInformation())
                .Throws<Exception>();

            var controller =
                new InsolvencyOrdersController(
                    mockDataAccessCradle.Object,
                    mockInsolvencyOrdersRepository.Object,
                    mockInsolvencyOrdersFlattenedRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationReader.Object,
                    mockInsolvenciesHalFormatter.Object,
                    mockInsolvenciesHalCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Act
            void Result() => controller.GetAllInsolvencyOrders().GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Gets all insolvency records when there is a problem formatting the response as hal bubbles up the exception.
        /// </summary>
        [TestMethod]
        public void GetAllInsolvencyRecords_WhenThereIsAProblemFormattingTheResponseAsHal_BubblesUpTheException()
        {
            mockInsolvenciesHalCollectionFormatter
                .Setup(insolvencyCollectionFormatter =>
                    insolvencyCollectionFormatter.FormatForHal(It.IsAny<CollectionResource<InsolvencyOrderModel>>()))
                .Throws<Exception>();

            var controller =
                new InsolvencyOrdersController(
                    mockDataAccessCradle.Object,
                    mockInsolvencyOrdersRepository.Object,
                    mockInsolvencyOrdersFlattenedRepository.Object,
                    mockIncludeReader.Object,
                    mockPageInformationReader.Object,
                    mockInsolvenciesHalFormatter.Object,
                    mockInsolvenciesHalCollectionFormatter.Object,
                    mockTelemetryClient.Object);

            // Act
            void Result() => controller.GetAllInsolvencyOrders().GetAwaiter().GetResult();

            // Assert
            Assert.ThrowsException<Exception>((Action)Result);
        }

        /// <summary>
        /// Checks the insolvencies controller attribute has authorization attribute and query scope.
        /// </summary>
        [TestMethod]
        [TestCategory("Category [UnitTest - Authorization")]
        public void CheckInsolvenciesControllerAttribute_HasAuthorizationAttributeAndQueryScope()
        {
            // Arrange

            // Act
            var attributes = Attribute.GetCustomAttribute(typeof(InsolvencyOrdersController), typeof(AuthorizeAttribute));
            var authorizeAttribute = attributes as AuthorizeAttribute;

            // Assert
            Assert.IsNotNull(attributes);
            Assert.IsNotNull(authorizeAttribute);
            Assert.AreEqual("Query", authorizeAttribute.Policy);
        }
    }
}
