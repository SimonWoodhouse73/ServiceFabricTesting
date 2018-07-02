// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 03-27-2018
//
// Last Modified By : MartinG
// Last Modified On : 04-20-2018
// ***********************************************************************
// <copyright file="DisputeCollectionResponseTests.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>DisputeCollectionResponseTests</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Api.Hateoas;
using Api.UnitTests.Helpers;
using Api.UnitTests.TestData;
using Callcredit.Domain.Insolvencies.Models;
using Callcredit.Domain.Insolvencies.Resources;
using Callcredit.RESTful.Services;
using Callcredit.RESTful.Services.Hal;
using Callcredit.RESTful.Services.Readers;
using Callcredit.TestHelpers;
using Callcredit.TestHelpers.Halcyon;
using Halcyon.HAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Api.UnitTests.Hateoas
{
    /// <summary>
    /// Class DisputeCollectionResponseTests.
    /// </summary>
    [TestClass]
    public class DisputeCollectionResponseTests
    {
        /// <summary>
        /// Constructings a dispute collection response with null address resolver throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingADisputeCollectionResponse_WithNullAddressResolver_ThrowsArgumentNullException()
        {
            // Arrange
            IAddressResolver addressResolver = null;
            var mockedPagingLinks = new Mock<IPagingLinks>();
            var mockedRequestReader = new Mock<IRequestReader>();

            // Act
            void ResponseObject() => new DisputeCollectionResponse(
                addressResolver,
                mockedPagingLinks.Object,
                mockedRequestReader.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)ResponseObject);
        }

        /// <summary>
        /// Constructings a dispute collection response with null address resolver has correct parameter name in exception.
        /// </summary>
        [TestMethod]
        public void ConstructingADisputeCollectionResponse_WithNullAddressResolver_HasCorrectParameterNameInException()
        {
            // Arrange
            const string expectedParameterName = "addressResolver";

            IAddressResolver addressResolver = null;
            var mockedPagingLinks = new Mock<IPagingLinks>();
            var mockedRequestReader = new Mock<IRequestReader>();

            // Act
            void ResponseObject() => new DisputeCollectionResponse(
                addressResolver,
                mockedPagingLinks.Object,
                mockedRequestReader.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(ResponseObject, expectedParameterName);
        }

        /// <summary>
        /// Constructings a dispute collection response with null paging link throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingADisputeCollectionResponse_WithNullPagingLink_ThrowsArgumentNullException()
        {
            // Arrange
            var mockedRequestReader = new Mock<IRequestReader>();
            var mockedAddressResolver = new Mock<IAddressResolver>();
            IPagingLinks pagingLinks = null;

            // Act
            void ResponseObject() => new DisputeCollectionResponse(
                mockedAddressResolver.Object,
                pagingLinks,
                mockedRequestReader.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)ResponseObject);
        }

        /// <summary>
        /// Constructings a dispute collection response with null paging link has correct parameter name in exception.
        /// </summary>
        [TestMethod]
        public void ConstructingADisputeCollectionResponse_WithNullPagingLink_HasCorrectParameterNameInException()
        {
            // Arrange
            const string expectedParameterName = "pagingLinks";

            var mockedRequestReader = new Mock<IRequestReader>();
            var mockedAddressResolver = new Mock<IAddressResolver>();
            IPagingLinks pagingLinks = null;

            // Act
            void ResponseObject() => new DisputeCollectionResponse(
                mockedAddressResolver.Object,
                pagingLinks,
                mockedRequestReader.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(ResponseObject, expectedParameterName);
        }

        /// <summary>
        /// Constructings a dispute collection response with null request reader throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingADisputeCollectionResponse_WithNullRequestReader_ThrowsArgumentNullException()
        {
            // Arrange
            const IRequestReader requestReader = null;

            var addressResolverMock = new Mock<IAddressResolver>();
            var mockPagingLinks = new Mock<IPagingLinks>();

            // Act
            void ConstructResponseObject() => new DisputeCollectionResponse(addressResolverMock.Object, mockPagingLinks.Object, requestReader);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)ConstructResponseObject);
        }

        /// <summary>
        /// Constructings a dispute collection response with null request reader has correct parameter name in exception.
        /// </summary>
        [TestMethod]
        public void ConstructingADisputeCollectionResponse_WithNullRequestReader_HasCorrectParameterNameInException()
        {
            // Arrange
            const string expectedParameterName = "requestReader";
            const IRequestReader requestReader = null;

            var addressResolverMock = new Mock<IAddressResolver>();
            var mockPagingLinks = new Mock<IPagingLinks>();

            // Act
            void ConstructResponseObject() => new DisputeCollectionResponse(addressResolverMock.Object, mockPagingLinks.Object, requestReader);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(ConstructResponseObject, expectedParameterName);
        }

        /// <summary>
        /// Constructings a dispute collection response with address resolver provided does not throw an exception.
        /// </summary>
        [TestMethod]
        public void ConstructingADisputeCollectionResponse_WithAddressResolverProvided_DoesNotThrowAnException()
        {
            // Arrange
            var mockedRequestReader = new Mock<IRequestReader>();
            var mockedAddressResolver = new Mock<IAddressResolver>();
            var mockedPagingLinks = new Mock<IPagingLinks>();

            // Act
            void ResponseObject() => new DisputeCollectionResponse(
                mockedAddressResolver.Object,
                mockedPagingLinks.Object,
                mockedRequestReader.Object);

            // Assert
            NoExceptionAssert.DoesNotThrowException<ArgumentNullException>(ResponseObject);
        }

        /// <summary>
        /// Formattings a dispute collection as hal with null data provided throws argument null exception.
        /// </summary>
        [TestMethod]
        [TestCategory("Null data - throws")]
        public void FormattingADisputeCollectionAsHal_WithNullDataProvided_ThrowsArgumentNullException()
        {
            // Arrange
            var mockedRequestReader = new Mock<IRequestReader>();
            var mockedAddressResolver = new Mock<IAddressResolver>();
            var mockedPagingLinks = new Mock<IPagingLinks>();
            var responseObject = new DisputeCollectionResponse(
                    mockedAddressResolver.Object,
                    mockedPagingLinks.Object,
                    mockedRequestReader.Object);

            CollectionResource<DisputeModel> disputesCollection = null;

            // Act
            void CreateHalResponse() => responseObject.FormatForHal(disputesCollection);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)CreateHalResponse);
        }

        /// <summary>
        /// Formattings a dispute collection as hal with null data provided has correct parameter name in exception.
        /// </summary>
        [TestMethod]
        public void FormattingADisputeCollectionAsHal_WithNullDataProvided_HasCorrectParameterNameInException()
        {
            // Arrange
            const string expectedParameterName = "collection";

            var mockedRequestReader = new Mock<IRequestReader>();
            var mockedAddressResolver = new Mock<IAddressResolver>();
            var mockedPagingLinks = new Mock<IPagingLinks>();
            var responseObject =
                new DisputeCollectionResponse(
                    mockedAddressResolver.Object,
                    mockedPagingLinks.Object,
                    mockedRequestReader.Object);

            CollectionResource<DisputeModel> disputesCollection = null;

            // Act
            void CreateHalResponse() => responseObject.FormatForHal(disputesCollection);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(CreateHalResponse, expectedParameterName);
        }

        /// <summary>
        /// Formattings a dispute collection as hal with data and URI provided creates hal configuration for the data.
        /// </summary>
        [TestMethod]
        public void FormattingADisputeCollectionAsHal_WithDataAndUriProvided_CreatesHalConfigurationForTheData()
        {
            // Arrange
            const int numberOfItemsInCollection = 1000;
            const int numberOfPagesInCollection = 100;
            const int currentPage = 20;

            var mockedRequestReader = new Mock<IRequestReader>();
            var mockedAddressResolver = new Mock<IAddressResolver>();
            var mockedPagingLinks = new Mock<IPagingLinks>();
            var responseObject =
                new DisputeCollectionResponse(
                    mockedAddressResolver.Object,
                    mockedPagingLinks.Object,
                    mockedRequestReader.Object);

            mockedPagingLinks
                .Setup(pagingLinks => pagingLinks.GetPagingLinks(It.IsAny<Uri>(), currentPage, numberOfPagesInCollection))
                .Returns(new List<Link>());

            var disputes = new List<DisputeModel>();
            var disputesCollection = new CollectionResource<DisputeModel>(
                new CollectionResourceInfo(numberOfItemsInCollection, numberOfPagesInCollection, currentPage), disputes);

            // Act
            var halResponse = responseObject.FormatForHal(disputesCollection);

            // Assert
            Assert.IsNotNull(halResponse);
        }

        /// <summary>
        /// Formattings a dispute collection as hal setting hal force true configures force hal true.
        /// </summary>
        [TestMethod]
        [TestCategory("HAL - force HAL")]
        public void FormattingADisputeCollectionAsHal_SettingHalForceTrue_ConfiguresForceHalTrue()
        {
            // Arrange
            const int numberOfItemsInCollection = 1000;
            const int numberOfPagesInCollection = 100;
            const int currentPage = 20;

            var mockedRequestReader = new Mock<IRequestReader>();
            var mockedAddressResolver = new Mock<IAddressResolver>();
            var mockedPagingLinks = new Mock<IPagingLinks>();
            var responseObject =
                new DisputeCollectionResponse(
                    mockedAddressResolver.Object,
                    mockedPagingLinks.Object,
                    mockedRequestReader.Object);

            mockedPagingLinks
                .Setup(pagingLinks => pagingLinks.GetPagingLinks(It.IsAny<Uri>(), currentPage, numberOfPagesInCollection))
                .Returns(new List<Link>());

            var disputes = new List<DisputeModel>();
            var disputesCollection =
                new CollectionResource<DisputeModel>(
                    new CollectionResourceInfo(numberOfItemsInCollection, numberOfPagesInCollection, currentPage),
                    disputes);

            // Act
            var halResponse = responseObject.FormatForHal(disputesCollection);

            // Assert
            Assert.IsTrue(halResponse.Config.ForceHAL);
        }

        /// <summary>
        /// Formattings a dispute collection as hal with data provided creates hal response model equal to the collection information.
        /// </summary>
        [TestMethod]
        public void FormattingADisputeCollectionAsHal_WithDataProvided_CreatesHalResponseModelEqualToTheCollectionInformation()
        {
            // Arrange
            const int numberOfItemsInCollection = 1000;
            const int numberOfPagesInCollection = 100;
            const int currentPage = 20;

            var mockedRequestReader = new Mock<IRequestReader>();
            var mockedAddressResolver = new Mock<IAddressResolver>();
            var mockedPagingLinks = new Mock<IPagingLinks>();
            var responseObject =
                new DisputeCollectionResponse(
                    mockedAddressResolver.Object,
                    mockedPagingLinks.Object,
                    mockedRequestReader.Object);

            mockedPagingLinks
                .Setup(pagingLinks => pagingLinks.GetPagingLinks(It.IsAny<Uri>(), currentPage, numberOfPagesInCollection))
                .Returns(new List<Link>());

            var disputes = new List<DisputeModel>();
            var disputesCollection = new CollectionResource<DisputeModel>(
                new CollectionResourceInfo(numberOfItemsInCollection, numberOfPagesInCollection, currentPage), disputes);

            // Act
            var halResponse = responseObject.FormatForHal(disputesCollection);

            // Assert
            halResponse.ShouldHaveModel(disputesCollection.Information);
        }

        /// <summary>
        /// Formattings a dispute collection as hal with a call to get paging data in the configuration calls get paging links.
        /// </summary>
        [TestMethod]
        public void FormattingADisputeCollectionAsHal_WithACallToGetPagingDataInTheConfiguration_CallsGetPagingLinks()
        {
            // Arrange
            const int numberOfItemsInCollection = 1000;
            const int numberOfPagesInCollection = 100;
            const int currentPage = 20;

            var mockedRequestReader = new Mock<IRequestReader>();
            var mockedAddressResolver = new Mock<IAddressResolver>();
            var mockedPagingLinks = new Mock<IPagingLinks>();
            var responseObject =
                new DisputeCollectionResponse(
                    mockedAddressResolver.Object,
                    mockedPagingLinks.Object,
                    mockedRequestReader.Object);

            mockedPagingLinks
                .Setup(pagingLinks => pagingLinks.GetPagingLinks(It.IsAny<Uri>(), currentPage, numberOfPagesInCollection))
                .Returns(new List<Link>());

            var disputes = new List<DisputeModel>();
            var disputesCollection = new CollectionResource<DisputeModel>(
                new CollectionResourceInfo(numberOfItemsInCollection, numberOfPagesInCollection, currentPage), disputes);

            // Act
            responseObject.FormatForHal(disputesCollection);

            // Assert
            mockedPagingLinks.Verify(mock => mock.GetPagingLinks(It.IsAny<Uri>(), currentPage, numberOfPagesInCollection), Times.Once);
        }

        /// <summary>
        /// Formattings a dispute collection as hal with a call to add self links to the disputes in the configuration calls get link.
        /// </summary>
        [TestMethod]
        public void FormattingADisputeCollectionAsHal_WithACallToAddSelfLinksToTheDisputesInTheConfiguration_CallsGetLink()
        {
            // Arrange
            const int numberOfItemsInCollection = 1000;
            const int numberOfPagesInCollection = 100;
            const int currentPage = 20;
            const string routeName = DomainResources.Dispute;
            const string linkName = CommonLinks.Self;

            var mockedRequestReader = new Mock<IRequestReader>();
            var mockedAddressResolver = new Mock<IAddressResolver>();
            var mockedPagingLinks = new Mock<IPagingLinks>();
            var responseObject =
                new DisputeCollectionResponse(
                    mockedAddressResolver.Object,
                    mockedPagingLinks.Object,
                    mockedRequestReader.Object);

            mockedPagingLinks
                .Setup(pagingLinks => pagingLinks.GetPagingLinks(It.IsAny<Uri>(), currentPage, numberOfPagesInCollection))
                .Returns(new List<Link>());

            var disputes = new List<DisputeModel>();
            var disputesCollection =
                new CollectionResource<DisputeModel>(
                    new CollectionResourceInfo(numberOfItemsInCollection, numberOfPagesInCollection, currentPage), disputes);

            // Act
            responseObject.FormatForHal(disputesCollection);

            // Assert
            mockedAddressResolver.Verify(mock => mock.GetLink(routeName, linkName), Times.Once);
        }

        /// <summary>
        /// Formattings a dispute collection as hal with a call to get a link to the parent record in the configuration calls get link.
        /// </summary>
        [TestMethod]
        public void FormattingADisputeCollectionAsHal_WithACallToGetALinkToTheParentRecordInTheConfiguration_CallsGetLink()
        {
            // Arrange
            const int numberOfItemsInCollection = 1000;
            const int numberOfPagesInCollection = 100;
            const int currentPage = 20;
            const string routeName = DomainResources.Disputes;
            const string linkName = CommonLinks.Parent;

            var mockedRequestReader = new Mock<IRequestReader>();
            var mockedAddressResolver = new Mock<IAddressResolver>();
            var mockedPagingLinks = new Mock<IPagingLinks>();
            var responseObject =
                new DisputeCollectionResponse(
                    mockedAddressResolver.Object,
                    mockedPagingLinks.Object,
                    mockedRequestReader.Object);

            mockedPagingLinks
                .Setup(pagingLinks => pagingLinks.GetPagingLinks(It.IsAny<Uri>(), currentPage, numberOfPagesInCollection))
                .Returns(new List<Link>());

            var disputes = new List<DisputeModel>();
            var disputesCollection = new CollectionResource<DisputeModel>(
                new CollectionResourceInfo(numberOfItemsInCollection, numberOfPagesInCollection, currentPage), disputes);

            // Act
            responseObject.FormatForHal(disputesCollection);

            // Assert
            mockedAddressResolver.Verify(mock => mock.GetLink(routeName, linkName), Times.Once);
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

            mockedAddressResolver
                .Setup(s => s.GetLink(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string routeName, string linkName) => new Link(routeName, linkName));

            var responseObject = new DisputeResponse(mockedAddressResolver.Object);

            // Act
            var halFormattedResponse = responseObject.FormatForHal(expectedDispute);

            // Assert
            halFormattedResponse.ShouldHaveLinks(expectedLinks);
        }

        /// <summary>
        /// Formattings a dispute as hal with a valid dispute should have an embedded collection
        /// </summary>
        [TestMethod]
        public void FormattingADisputeCollectionAsHal_WithValidDisputesModelCollection_ReturnsExpectedCollection()
        {
            // Arrange
            const int numberOfItemsInCollection = 1000;
            const int numberOfPagesInCollection = 100;
            const int currentPage = 20;

            var collectionKey = DomainResources.Disputes.ToLowerInvariant();
            var mockedRequestReader = new Mock<IRequestReader>();
            var mockedAddressResolver = new Mock<IAddressResolver>();
            var mockedPagingLinks = new Mock<IPagingLinks>();

            var disputes = new List<DisputeModel>();
            var disputesCollection = new CollectionResource<DisputeModel>(
                                        new CollectionResourceInfo(numberOfItemsInCollection, numberOfPagesInCollection, currentPage), disputes);
            mockedPagingLinks
                .Setup(pagingLinks => pagingLinks.GetPagingLinks(It.IsAny<Uri>(), currentPage, numberOfPagesInCollection))
                .Returns(new List<Link>());

            var responseObject =
                new DisputeCollectionResponse(
                                              mockedAddressResolver.Object,
                                              mockedPagingLinks.Object,
                                              mockedRequestReader.Object);

            var resultsToFormat = DisputeTestData.CreateDisputeActuals();

            // Act
            var halFormattedResponse = responseObject.FormatForHal(disputesCollection);

            // Assert
            halFormattedResponse.ShouldHaveEmbeddedCollection(collectionKey, resultsToFormat);
        }
    }
}
