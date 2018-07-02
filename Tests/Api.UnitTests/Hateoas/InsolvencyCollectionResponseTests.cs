// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 03-27-2018
//
// Last Modified By : MartinG
// Last Modified On : 04-23-2018
// ***********************************************************************
// <copyright file="InsolvencyCollectionResponseTests.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>InsolvencyCollectionResponseTests</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Api.Hateoas;
using Callcredit.Domain.Insolvencies.Models;
using Callcredit.RESTful.Services;
using Callcredit.RESTful.Services.Hal;
using Callcredit.RESTful.Services.Includes;
using Callcredit.RESTful.Services.Readers;
using Callcredit.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Api.UnitTests.Hateoas
{
    /// <summary>
    /// Class InsolvencyCollectionResponseTests.
    /// </summary>
    [TestClass]
    public class InsolvencyCollectionResponseTests
    {
        /// <summary>
        /// Constructings a insolvency order collection response with null address resolver throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingAInsolvencyOrderCollectionResponse_WithNullAddressResolver_ThrowsArgumentNullException()
        {
            // Arrange
            const IAddressResolver addressResolver = null;
            var pagingLinksMock = new Mock<IPagingLinks>();
            var requestReaderMock = new Mock<IRequestReader>();
            var includeReader = new Mock<IIncludeReader>();

            // Act
            void ConstructResponseObject() =>
                new InsolvencyOrderCollectionResponse(addressResolver, pagingLinksMock.Object, requestReaderMock.Object, includeReader.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)ConstructResponseObject);
        }

        /// <summary>
        /// Constructings a insolvency order collection response with null address resolver has correct parameter name in exception.
        /// </summary>
        [TestMethod]
        public void ConstructingAInsolvencyOrderCollectionResponse_WithNullAddressResolver_HasCorrectParameterNameInException()
        {
            // Arrange
            const string expectedParameterName = "addressResolver";
            const IAddressResolver addressResolver = null;
            var includeReader = new Mock<IIncludeReader>();
            var pagingLinksMock = new Mock<IPagingLinks>();
            var requestReaderMock = new Mock<IRequestReader>();

            // Act
            void ConstructResponseObject() =>
                new InsolvencyOrderCollectionResponse(addressResolver, pagingLinksMock.Object, requestReaderMock.Object, includeReader.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(
                ConstructResponseObject,
                expectedParameterName);
        }

        /// <summary>
        /// Constructings a insolvency order collection response with null paging link throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingAInsolvencyOrderCollectionResponse_WithNullPagingLink_ThrowsArgumentNullException()
        {
            // Arrange
            const IPagingLinks pagingLinks = null;

            var addressResolverMock = new Mock<IAddressResolver>();
            var requestReaderMock = new Mock<IRequestReader>();
            var includeReader = new Mock<IIncludeReader>();

            // Act
            void ConstructResponseObject() =>
                new InsolvencyOrderCollectionResponse(addressResolverMock.Object, pagingLinks, requestReaderMock.Object, includeReader.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)ConstructResponseObject);
        }

        /// <summary>
        /// Constructings a insolvency order collection response with null paging link has correct parameter name in exception.
        /// </summary>
        [TestMethod]
        public void ConstructingAInsolvencyOrderCollectionResponse_WithNullPagingLink_HasCorrectParameterNameInException()
        {
            // Arrange
            const string expectedParameterName = "pagingLinks";
            const IPagingLinks pagingLinks = null;

            var addressResolverMock = new Mock<IAddressResolver>();
            var requestReaderMock = new Mock<IRequestReader>();
            var includeReader = new Mock<IIncludeReader>();

            // Act
            void ConstructResponseObject() =>
                new InsolvencyOrderCollectionResponse(addressResolverMock.Object, pagingLinks, requestReaderMock.Object, includeReader.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(
                ConstructResponseObject,
                expectedParameterName);
        }

        /// <summary>
        /// Constructings a insolvency order collection response with null request reader throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConstructingAInsolvencyOrderCollectionResponse_WithNullRequestReader_ThrowsArgumentNullException()
        {
            // Arrange
            const IRequestReader requestReader = null;

            var addressResolverMock = new Mock<IAddressResolver>();
            var mockPagingLinks = new Mock<IPagingLinks>();
            var includeReader = new Mock<IIncludeReader>();

            // Act
            void ConstructResponseObject() =>
                new InsolvencyOrderCollectionResponse(addressResolverMock.Object, mockPagingLinks.Object, requestReader, includeReader.Object);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)ConstructResponseObject);
        }

        /// <summary>
        /// Constructings a insolvency order collection response with null request reader has correct parameter name in exception.
        /// </summary>
        [TestMethod]
        public void ConstructingAInsolvencyOrderCollectionResponse_WithNullRequestReader_HasCorrectParameterNameInException()
        {
            // Arrange
            const string expectedParameterName = "requestReader";
            const IRequestReader requestReader = null;

            var addressResolverMock = new Mock<IAddressResolver>();
            var mockPagingLinks = new Mock<IPagingLinks>();
            var includeReader = new Mock<IIncludeReader>();

            // Act
            void ConstructResponseObject() =>
                new InsolvencyOrderCollectionResponse(addressResolverMock.Object, mockPagingLinks.Object, requestReader, includeReader.Object);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(ConstructResponseObject, expectedParameterName);
        }

        /// <summary>
        /// Constructings a insolvency order collection response with address resolver provided constructs insolvency order collection response.
        /// </summary>
        [TestMethod]
        public void ConstructingAInsolvencyOrderCollectionResponse_WithAddressResolverProvided_ConstructsInsolvencyOrderCollectionResponse()
        {
            // Arrange
            var addressResolverMock = new Mock<IAddressResolver>();
            var pagingLinksMock = new Mock<IPagingLinks>();
            var requestReaderMock = new Mock<IRequestReader>();
            var includeReader = new Mock<IIncludeReader>();

            // Act
            var responseObject =
                new InsolvencyOrderCollectionResponse(
                    addressResolverMock.Object,
                    pagingLinksMock.Object,
                    requestReaderMock.Object,
                    includeReader.Object);

            // Assert
            Assert.IsNotNull(responseObject);
        }

        /// <summary>
        /// Configurings an insolvency collection as hal with null data provided throws argument null exception.
        /// </summary>
        [TestMethod]
        [TestCategory("Null data - throws")]
        public void ConfiguringAnInsolvencyCollectionAsHal_WithNullDataProvided_ThrowsArgumentNullException()
        {
            // Arrange
            CollectionResource<InsolvencyOrderModel> insolvencyCollection = null;
            var addressResolverMock = new Mock<IAddressResolver>();
            var pagingLinksMock = new Mock<IPagingLinks>();
            var requestReaderMock = new Mock<IRequestReader>();
            var includeReader = new Mock<IIncludeReader>();

            var responseObject =
                new InsolvencyOrderCollectionResponse(
                    addressResolverMock.Object,
                    pagingLinksMock.Object,
                    requestReaderMock.Object,
                    includeReader.Object);

            // Act
            void CreateHalResponse() => responseObject.FormatForHal(insolvencyCollection);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)CreateHalResponse);
        }

        /// <summary>
        /// Configurings an insolvency collection as hal with null data provided has correct parameter name in exception.
        /// </summary>
        [TestMethod]
        public void ConfiguringAnInsolvencyCollectionAsHal_WithNullDataProvided_HasCorrectParameterNameInException()
        {
            // Arrange
            const string expectedParameterName = "collection";

            CollectionResource<InsolvencyOrderModel> insolvencyCollection = null;
            var addressResolverMock = new Mock<IAddressResolver>();
            var pagingLinksMock = new Mock<IPagingLinks>();
            var requestReaderMock = new Mock<IRequestReader>();
            var includeReader = new Mock<IIncludeReader>();

            var responseObject =
                new InsolvencyOrderCollectionResponse(
                    addressResolverMock.Object,
                    pagingLinksMock.Object,
                    requestReaderMock.Object,
                    includeReader.Object);

            // Act
            void CreateHalResponse() => responseObject.FormatForHal(insolvencyCollection);

            // Assert
            ExceptionAssert.HasCorrectParameterNameForArgumentException(
                CreateHalResponse,
                expectedParameterName);
        }

        /// <summary>
        /// Configurings an insolvency collection as hal with null URI provided throws argument null exception.
        /// </summary>
        [TestMethod]
        public void ConfiguringAnInsolvencyCollectionAsHal_WithNullUriProvided_ThrowsArgumentNullException()
        {
            // Arrange
            const int numberOfItemsInCollection = 1000;
            const int numberOfPagesInCollection = 100;
            const int currentPage = 20;

            var addressResolverMock = new Mock<IAddressResolver>();
            var pagingLinksMock = new Mock<IPagingLinks>();
            var requestReaderMock = new Mock<IRequestReader>();
            var includeReader = new Mock<IIncludeReader>();

            var responseObject =
                new InsolvencyOrderCollectionResponse(
                    addressResolverMock.Object,
                    pagingLinksMock.Object,
                    requestReaderMock.Object,
                    includeReader.Object);

            var insolvencyOrders = new List<InsolvencyOrderModel>();

            var insolvencyCollection = new CollectionResource<InsolvencyOrderModel>(
                new CollectionResourceInfo(numberOfItemsInCollection, numberOfPagesInCollection, currentPage), insolvencyOrders);

            // Act
            void CreateHalResponse() => responseObject.FormatForHal(insolvencyCollection);

            // Assert
            Assert.ThrowsException<ArgumentNullException>((Action)CreateHalResponse);
        }
    }
}
