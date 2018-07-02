// ***********************************************************************
// Assembly         : Api
// Author           : markco
// Created          : 05-01-2018
//
// Last Modified By : markco
// Last Modified On : 05-22-2018
// ***********************************************************************
// <copyright file="InsolvencyOrderTradingDetailsResponse.cs" company="Callcredit Information Group.">
//     Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>InsolvencyOrderTradingDetailsResponse class</summary>
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
    public class InsolvencyOrderTradingDetailsResponse : IHalFormatter<InsolvencyOrderTradingDetailsModel>
    {
        /// <summary>
        /// The address resolver
        /// </summary>
        private readonly IAddressResolver addressResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="InsolvencyOrderTradingDetailsResponse" /> class.
        /// </summary>
        /// <param name="addressResolver">The <see cref="IAddressResolver" /> used for resolving address routes.</param>
        public InsolvencyOrderTradingDetailsResponse(IAddressResolver addressResolver)
        {
            Requires.NotNull(addressResolver, nameof(addressResolver));

            this.addressResolver = addressResolver;
        }

        /// <summary>
        /// Format HAL response
        /// </summary>
        /// <param name="item">Item to format</param>
        /// <returns>formatted item</returns>
        public HALResponse FormatForHal(InsolvencyOrderTradingDetailsModel item)
        {
            Requires.NotNull(item, nameof(item));

            var hal =
                new HALResponse(
                    item,
                    new HALModelConfig { ForceHAL = true })
                    .AddLinks(new[]
                    {
                        addressResolver.GetLink(DomainResources.InsolvencyOrderTradingDetail, CommonLinks.Self),
                        addressResolver.GetLink(DomainResources.InsolvencyOrderTradingDetails, CommonLinks.Parent)
                    });

            return hal;
        }
    }
}
