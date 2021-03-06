// ***********************************************************************
// Assembly         : Api
// Author           : markco
// Created          : 05-01-2018
//
// Last Modified By : markco
// Last Modified On : 05-22-2018
// ***********************************************************************
// <copyright file="InsolvencyOrderAddressResponse.cs" company="Callcredit Information Group.">
//     Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>InsolvencyOrderAddressResponse class</summary>
// ***********************************************************************
using Callcredit.Domain.Insolvencies.Models;
using Callcredit.Domain.Insolvencies.Resources;
using Callcredit.RESTful.Services;
using Callcredit.RESTful.Services.Hal;
using Halcyon.HAL;
using Validation;

namespace Api.Hateoas
{
    /// <summary>
    /// HAL response
    /// </summary>
    /// <seealso cref="Callcredit.RESTful.Services.Hal.IHalFormatter{T}" />
    public class InsolvencyOrderAddressResponse : IHalFormatter<InsolvencyOrderAddressModel>
    {
        /// <summary>
        /// The address resolver
        /// </summary>
        private readonly IAddressResolver addressResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="InsolvencyOrderAddressResponse" /> class.
        /// </summary>
        /// <param name="addressResolver">The <see cref="IAddressResolver" /> used for resolving address routes.</param>
        public InsolvencyOrderAddressResponse(IAddressResolver addressResolver)
        {
            Requires.NotNull(addressResolver, nameof(addressResolver));

            this.addressResolver = addressResolver;
        }

        /// <summary>
        /// Format HAL response
        /// </summary>
        /// <param name="item">Item to format</param>
        /// <returns>formatted item</returns>
        public HALResponse FormatForHal(InsolvencyOrderAddressModel item)
        {
            Requires.NotNull(item, nameof(item));

            var hal =
                new HALResponse(
                    item,
                    new HALModelConfig { ForceHAL = true })
                .AddLinks(new[]
                {
                    ////addressResolver.GetLinkToSelf(),
                    addressResolver.GetLink(DomainResources.InsolvencyOrderAddress, CommonLinks.Self),
                    addressResolver.GetLink(DomainResources.InsolvencyOrderAddresses, CommonLinks.Parent)
                });

            return hal;
        }
    }
}
