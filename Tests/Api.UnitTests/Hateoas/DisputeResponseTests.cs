// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 03-27-2018
//
// Last Modified By : MartinG
// Last Modified On : 04-20-2018
// ***********************************************************************
// <copyright file="DisputeResponseTests.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>DisputeResponseTests</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Api.Hateoas;
using Api.UnitTests.Helpers;
using Callcredit.Domain.Insolvencies.Models;
using Callcredit.Domain.Insolvencies.Resources;
using Callcredit.RESTful.Services;
using Callcredit.RESTful.Services.Hal;
using Callcredit.TestHelpers;
using Callcredit.TestHelpers.Halcyon;
using Halcyon.HAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Api.UnitTests.Hateoas
{
    /// <summary>
    /// Class DisputeResponseTests.
    /// </summary>
    [TestClass]
    public class DisputeResponseTests
    {
        /// <summary>
        /// Constructings a dispute response with null address resolver throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingADisputeResponse_WithNullAddressResolver_ThrowsArgumentNullException()
        {
            // Arrange
            IAddressResolver addressResolver = null;

            // Act
            void ResponseObject() => new DisputeResponse(addressResolver);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)ResponseObject);
        }

        /// <summary>
        /// Constructings a dispute response with null address resolver has correct parameter name in exception.
        /// </summary>
        [TestMethod]
        public void ConstructingADisputeResponse_WithNullAddressResolver_HasCorrectParameterNameInException()
        {
            // Arrange
            const string expectedParameterName = "addressResolver";

            IAddressResolver addressResolver = null;

            // Act
            void ResponseObject() => new DisputeResponse(addressResolver);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(ResponseObject, expectedParameterName);
        }

        /// <summary>
        /// Constructings a dispute response with null dispute list does not throw an exception.
        /// </summary>
        [TestMethod]
        public void ConstructingADisputeResponse_WithNullDisputeList_DoesNotThrowAnException()
        {
            // Arrange
            var mockedAddressResolver = new Mock<IAddressResolver>();

            // Act
            void ResponseObject() => new DisputeResponse(mockedAddressResolver.Object);

            // Assert
            NoExceptionAssert.DoesNotThrowException<ArgumentNullException>(ResponseObject);
        }

        /// <summary>
        /// Constructings a dispute response with address resolver provided constructs dispute response.
        /// </summary>
        [TestMethod]
        public void ConstructingADisputeResponse_WithAddressResolverProvided_ConstructsDisputeResponse()
        {
            // Arrange
            var mockedAddressResolver = new Mock<IAddressResolver>();

            // Act
            var responseObject = new DisputeResponse(mockedAddressResolver.Object);

            // Assert
            Assert.IsNotNull(responseObject);
        }

        /// <summary>
        /// Formattings a dispute as hal with null data provided throws argument null exception.
        /// </summary>
        [TestMethod]
        [TestCategory("Null data - throws")]
        public void FormattingADisputeAsHal_WithNullDataProvided_ThrowsArgumentNullException()
        {
            // Arrange
            DisputeModel dispute = null;
            var mockedAddressResolver = new Mock<IAddressResolver>();

            var responseObject = new DisputeResponse(mockedAddressResolver.Object);

            // Act
            void CreateHalResponse() => responseObject.FormatForHal(dispute);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)CreateHalResponse);
        }

        /// <summary>
        /// Formattings a disputet as hal with null data provided has correct parameter name in exception.
        /// </summary>
        [TestMethod]
        public void FormattingADisputetAsHal_WithNullDataProvided_HasCorrectParameterNameInException()
        {
            // Arrange
            const string expectedParameterName = "item";

            DisputeModel dispute = null;
            var mockedAddressResolver = new Mock<IAddressResolver>();

            var responseObject = new DisputeResponse(mockedAddressResolver.Object);

            // Act
            void CreateHalResponse() => responseObject.FormatForHal(dispute);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(CreateHalResponse, expectedParameterName);
        }

        /// <summary>
        /// Formattings a dispute as hal with data provided creates hal format response.
        /// </summary>
        [TestMethod]
        public void FormattingADisputeAsHal_WithDataProvided_CreatesHalFormatResponse()
        {
            // Arrange
            var mockedAddressResolver = new Mock<IAddressResolver>();

            var responseObject = new DisputeResponse(mockedAddressResolver.Object);
            var dispute = new DisputeModel();

            // Act
            var halResponse = responseObject.FormatForHal(dispute);

            // Assert
            Assert.IsNotNull(halResponse);
        }

        /// <summary>
        /// Formattings a dispute as hal setting hal force true configures force hal true.
        /// </summary>
        [TestMethod]
        [TestCategory("HAL - force HAL")]
        public void FormattingADisputeAsHal_SettingHalForceTrue_ConfiguresForceHalTrue()
        {
            // Arrange
            var mockedAddressResolver = new Mock<IAddressResolver>();

            var responseObject = new DisputeResponse(mockedAddressResolver.Object);
            var dispute = new DisputeModel();

            // Act
            var halResponse = responseObject.FormatForHal(dispute);

            // Assert
            Assert.IsTrue(halResponse.Config.ForceHAL);
        }

        /// <summary>
        /// Formattings a dispute as hal with data provided creates hal configuration model equal to the provided data.
        /// </summary>
        [TestMethod]
        public void FormattingADisputeAsHal_WithDataProvided_CreatesHalConfigurationModelEqualToTheProvidedData()
        {
            // Arrange
            var mockedAddressResolver = new Mock<IAddressResolver>();

            var responseObject = new DisputeResponse(mockedAddressResolver.Object);
            var dispute = new DisputeModel();

            // Act
            var halResponse = responseObject.FormatForHal(dispute);

            // Assert
            halResponse.ShouldHaveModel(dispute);
        }

        /// <summary>
        /// Formattings a dispute as hal with a call to get a link for the parent dispute collection in the configuration calls get link.
        /// </summary>
        [TestMethod]
        public void FormattingADisputeAsHal_WithACallToGetALinkForTheParentDisputeCollectionInTheConfiguration_CallsGetLink()
        {
            // Arrange
            const int disputeId = 12345;

            var mockedAddressResolver = new Mock<IAddressResolver>();

            var responseObject = new DisputeResponse(mockedAddressResolver.Object);
            var expectedDispute = new DisputeModel { DisputeId = disputeId };

            // Act
            responseObject.FormatForHal(expectedDispute);

            // Assert
            mockedAddressResolver.Verify(
                mock => mock.GetLink(DomainResources.Disputes, CommonLinks.Parent),
                Times.Once);
        }

        /// <summary>
        /// Formattings a dispute as hal with all link calls in the configuration doesnt ask for more links than it needs.
        /// </summary>
        [TestMethod]
        public void FormattingADisputeAsHal_WithAllLinkCallsInTheConfiguration_DoesntAskForMoreLinksThanItNeeds()
        {
            // Arrange
            const int disputeId = 12345;

            var mockedAddressResolver = new Mock<IAddressResolver>();

            var responseObject = new DisputeResponse(mockedAddressResolver.Object);
            var expectedDispute = new DisputeModel { DisputeId = disputeId };

            var callsToGetLinkToCollection = Times.Never();
            var callsToGetLink = Times.Exactly(2);
            var callsToGetLinkToEmbeddedCollection = Times.Never();
            var callsToGetLinkToEmbeddedItem = Times.Never();
            var callsToGetLinkToSelf = Times.Never();
            var callsToGetParent = Times.Never();
            var callsToGetPostLink = Times.Never();

            // Act
            responseObject.FormatForHal(expectedDispute);

            // Assert
            TestAssertions.ValidateCallsToAddLinksInHalConfiguration(
                 mockedAddressResolver,
                 callsToGetLinkToCollection,
                 callsToGetLink,
                 callsToGetLinkToEmbeddedCollection,
                 callsToGetLinkToEmbeddedItem,
                 callsToGetLinkToSelf,
                 callsToGetParent,
                 callsToGetPostLink);
        }

        /// <summary>
        /// Formattings a dispute as hal with a valid dispute should set embedded links.
        /// </summary>
        [TestMethod]
        [TestCategory("Set embedded links")]
        public void FormattingADisputeAsHal_WithAValidDispute_ShouldSetEmbeddedLinks()
        {
            // Arrange
            var expectedLinks = new List<Link>
            {
                // new Link(DomainResources.Dispute, CommonLinks.Self),
                new Link(DomainResources.Disputes, CommonLinks.Parent)
            };
            var expectedDispute = TestHelpers.CreateDispute();
            var mockedAddressResolver = new Mock<IAddressResolver>();

            mockedAddressResolver.Setup(s => s.GetLink(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string routeName, string linkName) => new Link(routeName, linkName));

            var responseObject = new DisputeResponse(mockedAddressResolver.Object);

            // Act
            var halFormattedResponse = responseObject.FormatForHal(expectedDispute);

            // Assert
            halFormattedResponse.ShouldHaveLinks(expectedLinks);
        }
    }
}
