// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 03-02-2018
//
// Last Modified By : MartinG
// Last Modified On : 03-02-2018
// ***********************************************************************
// <copyright file="TestAssertions.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>TestAssertions</summary>
// ***********************************************************************
using System.Diagnostics.CodeAnalysis;
using Callcredit.RESTful.Services;
using Moq;

namespace Api.UnitTests.Helpers
{
    /// <summary>
    /// Class TestAssertions.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class TestAssertions
    {
        /// <summary>
        /// Validates the calls to add links in hal configuration.
        /// </summary>
        /// <param name="addressResolverMock">The address resolver mock.</param>
        /// <param name="callsToGetLinkToCollection">The calls to get link to collection.</param>
        /// <param name="callsToGetLink">The calls to get link.</param>
        /// <param name="callsToGetLinkToEmbeddedCollection">The calls to get link to embedded collection.</param>
        /// <param name="callsToGetLinkToEmbeddedItem">The calls to get link to embedded item.</param>
        /// <param name="callsToGetLinkToSelf">The calls to get link to self.</param>
        /// <param name="callsToGetParent">The calls to get parent.</param>
        /// <param name="callsToGetPostLink">The calls to get post link.</param>
        public static void ValidateCallsToAddLinksInHalConfiguration(
            Mock<IAddressResolver> addressResolverMock,
            Times callsToGetLinkToCollection,
            Times callsToGetLink,
            Times callsToGetLinkToEmbeddedCollection,
            Times callsToGetLinkToEmbeddedItem,
            Times callsToGetLinkToSelf,
            Times callsToGetParent,
            Times callsToGetPostLink)
        {
            addressResolverMock.Verify(
                mock => mock.GetLinkToCollection(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()),
                callsToGetLinkToCollection);

            addressResolverMock.Verify(
                mock => mock.GetLink(It.IsAny<string>(), It.IsAny<string>()),
                callsToGetLink);

            addressResolverMock.Verify(
                mock => mock.GetLinkToEmbeddedCollection(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()),
                callsToGetLinkToEmbeddedCollection);

            addressResolverMock.Verify(
                mock => mock.GetLinkToEmbeddedItem(It.IsAny<string>()),
                callsToGetLinkToEmbeddedItem);

            addressResolverMock.Verify(
                mock => mock.GetLinkToSelf(),
                callsToGetLinkToSelf);

            addressResolverMock.Verify(
                mock => mock.GetParent(It.IsAny<string>()),
                callsToGetParent);

            addressResolverMock.Verify(
                mock => mock.GetPostLink(It.IsAny<string>(), It.IsAny<string>()),
                callsToGetPostLink);
        }
    }
}
