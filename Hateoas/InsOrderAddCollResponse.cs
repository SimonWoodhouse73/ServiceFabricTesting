// ***********************************************************************
// Assembly         : Api
// Author           : markco
// Created          : 05-01-2018
//
// Last Modified By : markco
// Last Modified On : 05-22-2018
// ***********************************************************************
// <copyright file="InsolvencyOrderAddressCollectionResponse.cs" company="Callcredit Information Group.">
//     Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>InsolvencyOrderAddressCollectionResponse class</summary>
// ***********************************************************************
using Callcredit.Domain.Insolvencies.Models;
using Callcredit.Domain.Insolvencies.Resources;
using Callcredit.RESTful.Services;
using Callcredit.RESTful.Services.Hal;
using Callcredit.RESTful.Services.Readers;
using Halcyon.HAL;
using Validation;

namespace Api.Hateoas
{
    /// <summary>
    /// HAL collection response
    /// </summary>
    /// <seealso cref="Callcredit.RESTful.Services.Hal.IHalCollectionFormatter{T}" />
    public class InsolvencyOrderAddressCollectionResponse : IHalCollectionFormatter<InsolvencyOrderAddressModel>
    {
        /// <summary>
        /// The address resolver
        /// </summary>
        private readonly IAddressResolver addressResolver;

        /// <summary>
        /// The paging links
        /// </summary>
        private readonly IPagingLinks pagingLinks;

        /// <summary>
        /// The request reader
        /// </summary>
        private readonly IRequestReader requestReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="InsolvencyOrderAddressCollectionResponse" /> class.
        /// </summary>
        /// <param name="addressResolver">The <see cref="IAddressResolver" /> used for resolving address routes.</param>
        /// <param name="pagingLinks">The <see cref="IPagingLinks" /> used to generate a paged resource.</param>
        /// <param name="requestReader">The <see cref="IRequestReader" /> used to read the request.</param>
        public InsolvencyOrderAddressCollectionResponse(IAddressResolver addressResolver, IPagingLinks pagingLinks, IRequestReader requestReader)
        {
            Requires.NotNull(addressResolver, nameof(addressResolver));
            Requires.NotNull(pagingLinks, nameof(pagingLinks));
            Requires.NotNull(requestReader, nameof(requestReader));

            this.addressResolver = addressResolver;
            this.pagingLinks = pagingLinks;
            this.requestReader = requestReader;
        }

        /// <summary>
        /// Format HAL collection
        /// </summary>
        /// <param name="collection">collection to format</param>
        /// <returns>HAL json</returns>
        public HALResponse FormatForHal(CollectionResource<InsolvencyOrderAddressModel> collection)
        {
            Requires.NotNull(collection, nameof(collection));

            var uri = requestReader.GetRequestUri();
            Link[] links =
                    {
                        addressResolver.GetLink(DomainResources.InsolvencyOrderAddress, CommonLinks.Self),
                        addressResolver.GetLink(DomainResources.InsolvencyOrderAddresses, CommonLinks.Parent)
                    };

            var collectionResponse =
                new HALResponse(collection.Information, new HALModelConfig { ForceHAL = true })
                .AddLinks(pagingLinks.GetPagingLinks(uri, collection.Information.CurrentPage, collection.Information.NumberOfPages))
                .AddEmbeddedCollection(
                    collectionName: DomainResources.InsolvencyOrderAddresses_camelCase,
                    model: collection.Resources,
                    links: links);

            return collectionResponse;
        }
    }
}
