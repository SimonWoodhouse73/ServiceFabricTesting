// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 03-27-2018
//
// Last Modified By : MartinG
// Last Modified On : 04-23-2018
// ***********************************************************************
// <copyright file="InsolvencyResponseTests.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>InsolvencyResponseTests</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Api.Hateoas;
using Callcredit.Domain.Insolvencies.Models;
using Callcredit.Domain.Insolvencies.Resources;
using Callcredit.RESTful.Services;
using Callcredit.RESTful.Services.Hal;
using Callcredit.RESTful.Services.Includes;
using Callcredit.TestHelpers;
using Callcredit.TestHelpers.Halcyon;
using Halcyon.HAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Api.UnitTests.Hateoas
{
    /// <summary>
    /// Class InsolvencyResponseTests.
    /// </summary>
    [TestClass]
    public class InsolvencyResponseTests
    {
        /// <summary>
        /// Constructings a insolvency response with null address resolver throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingAInsolvencyResponse_WithNullAddressResolver_ThrowsArgumentNullException()
        {
            // Arrange
            IAddressResolver addressResolver = null;
            var includeReader = new Mock<IIncludeReader>();

            // Act
            void ConstructResponseObject() => new InsolvencyOrderResponse(addressResolver, includeReader.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)ConstructResponseObject);
        }

        /// <summary>
        /// Constructings a insolvency response with null address resolver has correct parameter name in exception.
        /// </summary>
        [TestMethod]
        public void ConstructingAInsolvencyResponse_WithNullAddressResolver_HasCorrectParameterNameInException()
        {
            // Arrange
            IAddressResolver addressResolver = null;
            var includeReader = new Mock<IIncludeReader>();

            const string expectedParameterName = "addressResolver";

            // Act
            void ConstructResponseObject() => new InsolvencyOrderResponse(addressResolver, includeReader.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(
                ConstructResponseObject,
                expectedParameterName);
        }

        /// <summary>
        /// Constructings a insolvency response with null disputes list does not throw an exception.
        /// </summary>
        [TestMethod]
        public void ConstructingAInsolvencyResponse_WithNullDisputesList_DoesNotThrowAnException()
        {
            // Arrange
            var addressResolverMock = new Mock<IAddressResolver>();
            var includeReader = new Mock<IIncludeReader>();

            // Act
            void ConstructResponseObject() => new InsolvencyOrderResponse(addressResolverMock.Object, includeReader.Object);

            // Assert
            NoExceptionAssert.DoesNotThrowException<ArgumentNullException>(ConstructResponseObject);
        }

        /// <summary>
        /// Constructings a insolvency response with address resolver provided constructs insolvency response.
        /// </summary>
        [TestMethod]
        public void ConstructingAInsolvencyResponse_WithAddressResolverProvided_ConstructsInsolvencyResponse()
        {
            // Arrange
            var addressResolverMock = new Mock<IAddressResolver>();
            var includeReader = new Mock<IIncludeReader>();

            // Act
            var responseObject = new InsolvencyOrderResponse(addressResolverMock.Object, includeReader.Object);

            // Assert
            Assert.IsNotNull(responseObject);
        }

        /// <summary>
        /// Configurings an insolvency as hal with null data provided throws argument null exception.
        /// </summary>
        [TestMethod]
        [TestCategory("Null data - throws")]
        public void ConfiguringAnInsolvencyAsHal_WithNullDataProvided_ThrowsArgumentNullException()
        {
            // Arrange
            InsolvencyOrderModel insolvency = null;
            var addressResolverMock = new Mock<IAddressResolver>();
            var includeReader = new Mock<IIncludeReader>();

            var responseObject = new InsolvencyOrderResponse(addressResolverMock.Object, includeReader.Object);

            // Act
            void CreateHalResponse() => responseObject.FormatForHal(insolvency);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)CreateHalResponse);
        }

        /// <summary>
        /// Configurings an insolvency as hal with null data provided has correct parameter name in exception.
        /// </summary>
        [TestMethod]
        public void ConfiguringAnInsolvencyAsHal_WithNullDataProvided_HasCorrectParameterNameInException()
        {
            // Arrange
            const string expectedParameterName = "item";

            InsolvencyOrderModel insolvency = null;
            var addressResolverMock = new Mock<IAddressResolver>();
            var includeReader = new Mock<IIncludeReader>();

            var responseObject = new InsolvencyOrderResponse(addressResolverMock.Object, includeReader.Object);

            // Act
            void CreateHalResponse() => responseObject.FormatForHal(insolvency);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(
                CreateHalResponse,
                expectedParameterName);
        }

        /// <summary>
        /// Configurings an insolvency as hal with data provided creates hal configuration for the data.
        /// </summary>
        [TestMethod]
        public void ConfiguringAnInsolvencyAsHal_WithDataProvided_CreatesHalConfigurationForTheData()
        {
            // Arrange
            var addressResolverMock = new Mock<IAddressResolver>();
            var includeReader = new Mock<IIncludeReader>();

            var responseObject = new InsolvencyOrderResponse(addressResolverMock.Object, includeReader.Object);

            var insolvency = new InsolvencyOrderModel();

            // Act
            var halResponse = responseObject.FormatForHal(insolvency);

            // Assert
            Assert.IsNotNull(halResponse);
        }

        /// <summary>
        /// Configurings an insolvency as hal setting hal force true configures force hal true.
        /// </summary>
        [TestMethod]
        [TestCategory("HAL - force HAL")]
        public void ConfiguringAnInsolvencyAsHal_SettingHalForceTrue_ConfiguresForceHalTrue()
        {
            // Arrange
            var addressResolverMock = new Mock<IAddressResolver>();
            var includeReader = new Mock<IIncludeReader>();

            var responseObject = new InsolvencyOrderResponse(addressResolverMock.Object, includeReader.Object);
            var insolvency = new InsolvencyOrderModel();

            // Act
            var halResponse = responseObject.FormatForHal(insolvency);

            // Assert
            Assert.IsTrue(halResponse.Config.ForceHAL);
        }

        /// <summary>
        /// Configurings an insolvency as hal with data provided creates hal configuration model equal to the provided data.
        /// </summary>
        [TestMethod]
        public void ConfiguringAnInsolvencyAsHal_WithDataProvided_CreatesHalConfigurationModelEqualToTheProvidedData()
        {
            // Arrange
            var addressResolverMock = new Mock<IAddressResolver>();
            var includeReader = new Mock<IIncludeReader>();

            var responseObject = new InsolvencyOrderResponse(addressResolverMock.Object, includeReader.Object);

            var expectedInsolvency = new InsolvencyOrderModel();

            // Act
            var halResponse = responseObject.FormatForHal(expectedInsolvency);

            // Assert
            halResponse.ShouldHaveModel(expectedInsolvency);
        }

        /// <summary>
        /// Configurings an insolvency as hal with a call to get the self link in the configuration calls get link for two links.
        /// </summary>
        [TestMethod]
        public void ConfiguringAnInsolvencyAsHal_WithACallToGetTheSelfLinkInTheConfiguration_CallsGetLinkForTwoLinks()
        {
            // Arrange
            const int NumberOfGetLinkCalls = 7;

            var addressResolverMock = new Mock<IAddressResolver>();
            var includeReader = new Mock<IIncludeReader>();

            var responseObject = new InsolvencyOrderResponse(addressResolverMock.Object, includeReader.Object);
            var expectedInsolvency = new InsolvencyOrderModel();

            var numberOfTimesGetLinkWasCalled = Times.Exactly(NumberOfGetLinkCalls);

            // Act
            responseObject.FormatForHal(expectedInsolvency);

            // Assert
            addressResolverMock.Verify(mock => mock.GetLink(It.IsAny<string>(), It.IsAny<string>()), numberOfTimesGetLinkWasCalled);
        }

        /// <summary>
        /// Configurings an insolvency as hal with a call to get a link to the insolvency collection in the configuration calls get link.
        /// </summary>
        [TestMethod]
        public void ConfiguringAnInsolvencyAsHal_WithACallToGetALinkToTheInsolvencyCollectionInTheConfiguration_CallsGetLink()
        {
            // Arrange
            const string routeName = DomainResources.InsolvencyOrders;
            const string linkName = CommonLinks.Parent;

            var addressResolverMock = new Mock<IAddressResolver>();
            var includeReader = new Mock<IIncludeReader>();

            var responseObject = new InsolvencyOrderResponse(addressResolverMock.Object, includeReader.Object);
            var expectedInsolvency = new InsolvencyOrderModel();

            // Act
            responseObject.FormatForHal(expectedInsolvency);

            // Assert
            addressResolverMock.Verify(mock => mock.GetLink(routeName, linkName), Times.Once);
        }

        /// <summary>
        /// Configurings an insolvency as hal with all link calls in the configuration doesnt ask for more links than it needs.
        /// </summary>
        [TestMethod]
        public void ConfiguringAnInsolvencyAsHal_WithAllLinkCallsInTheConfiguration_DoesntAskForMoreLinksThanItNeeds()
        {
            // Arrange
            const int insolvencyOrderId = 12345;
            const int NumberOfGetLinkCalls = 7;

            var addressResolverMock = new Mock<IAddressResolver>();
            var includeReader = new Mock<IIncludeReader>();

            var responseObject = new InsolvencyOrderResponse(addressResolverMock.Object, includeReader.Object);
            var expectedInsolvency = new InsolvencyOrderModel { InsolvencyOrderId = insolvencyOrderId };

            var callsToGetLink = Times.Exactly(NumberOfGetLinkCalls);

            // Act
            responseObject.FormatForHal(expectedInsolvency);

            // Assert
            addressResolverMock.Verify(mock => mock.GetLink(It.IsAny<string>(), It.IsAny<string>()), callsToGetLink);
        }

        /// <summary>
        /// Configurings an insolvency as hal with a valid insolvency should set response properties.
        /// </summary>
        [TestMethod]
        public void ConfiguringAnInsolvencyAsHal_WithAValidInsolvency_ShouldSetResponseProperties()
        {
            // Arrange
            var addressResolverMock = new Mock<IAddressResolver>();
            var includeReader = new Mock<IIncludeReader>();

            var expectedInsolvency = Helpers.TestHelpers.CreateInsolvency();
            var responseObject = new InsolvencyOrderResponse(addressResolverMock.Object, includeReader.Object);

            // Act
            var halRootObject = responseObject.FormatForHal(expectedInsolvency);

            // Assert
            halRootObject.ShouldHaveModel(expectedInsolvency);
        }

        /// <summary>
        /// Configurings an insolvency as hal with a valid insolvency should set embedded links.
        /// </summary>
        [TestMethod]
        [TestCategory("Set embedded links")]
        public void ConfiguringAnInsolvencyAsHal_WithAValidInsolvency_ShouldSetEmbeddedLinks()
        {
            // Arrange
            var expectedLinks = new List<Link>
            {
                new Link(DomainResources.InsolvencyOrder, CommonLinks.Self),
                new Link(DomainResources.InsolvencyOrders, CommonLinks.Parent),
            };

            var expectedInsolvency = Helpers.TestHelpers.CreateInsolvency();
            var addressResolverMock = new Mock<IAddressResolver>();
            var includeReader = new Mock<IIncludeReader>();

            addressResolverMock.Setup(s => s.GetLink(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string routeName, string linkName) => new Link(routeName, linkName));

            var responseObject = new InsolvencyOrderResponse(addressResolverMock.Object, includeReader.Object);

            // Act
            var halRootObject = responseObject.FormatForHal(expectedInsolvency);

            // Assert
            halRootObject.ShouldHaveLinks(expectedLinks);
        }

        /// <summary>
        /// Configurings an insolvency as hal with zero disputes provided embeds empty dispute into hal response.
        /// </summary>
        [TestMethod]
        [TestCategory("HAL - embedded resource")]
        public void ConfiguringAnInsolvencyAsHal_WithZeroDisputesProvided_EmbedsEmptyDisputeIntoHalResponse()
        {
            // Arrange
            var expectedInsolvency = new InsolvencyOrderModel { Disputes = new List<DisputeModel>() };

            var addressResolverMock = new Mock<IAddressResolver>();
            var includeReader = new Mock<IIncludeReader>();
            includeReader.Setup(x => x.GetIncludes()).Returns(new string[] { "disputes" });

            var responseObject = new InsolvencyOrderResponse(addressResolverMock.Object, includeReader.Object);

            // Act
            var halResponse = responseObject.FormatForHal(expectedInsolvency);

            // Assert
            halResponse.ShouldHaveEmbeddedCollection(DomainResources.Disputes_camelCase, expectedInsolvency.Disputes);
        }

        /// <summary>
        /// Configurings an insolvency as hal with disputes provided embeds disputes into hal response.
        /// </summary>
        [TestMethod]
        [TestCategory("HAL - embedded resource")]
        public void ConfiguringAnInsolvencyAsHal_WithDisputesProvided_EmbedsDisputesIntoHalResponse()
        {
            // Arrange
            var expectedDispute = new DisputeModel
            {
                DateRaised = new DateTime(2017, 10, 12),
                Displayed = true,
                DisputeId = 1,
                InsolvencyOrderId = 2,
                ReferenceNumber = "fdshiu308j"
            };

            var expectedInsolvency = new InsolvencyOrderModel { Disputes = new List<DisputeModel> { expectedDispute } };

            var addressResolverMock = new Mock<IAddressResolver>();
            var includeReader = new Mock<IIncludeReader>();
            includeReader.Setup(x => x.GetIncludes()).Returns(new string[] { "disputes" });

            var responseObject = new InsolvencyOrderResponse(addressResolverMock.Object, includeReader.Object);

            // Act
            var halResponse = responseObject.FormatForHal(expectedInsolvency);

            // Assert
            halResponse.ShouldHaveEmbeddedCollection(DomainResources.Disputes_camelCase, expectedInsolvency.Disputes);
        }
    }
}
