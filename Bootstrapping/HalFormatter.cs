using System.Diagnostics.CodeAnalysis;
using Api.Hateoas;
using Callcredit.Domain.Insolvencies.Models;
using Callcredit.RESTful.Services.Hal;
using Microsoft.Extensions.DependencyInjection;

namespace Api.BootStrapping
{
    /// <summary>
    /// Class HalFormatters.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class HalFormatters
    {
        /// <summary>
        /// Adds the hal formatting components to the Insolvency service.
        /// </summary>
        /// <param name="services">The services collection used to configure the service.</param>
        public static void AddHalFormatting(this IServiceCollection services)
        {
            services.AddScoped<IHalFormatter<InsolvencyOrderModel>, InsolvencyOrderResponse>();
            services.AddScoped<IHalCollectionFormatter<InsolvencyOrderModel>, InsolvencyOrderCollectionResponse>();

            services.AddScoped<IHalFormatter<InsolvencyOrderPersonModel>, InsolvencyOrderPersonResponse>();
            services.AddScoped<IHalCollectionFormatter<InsolvencyOrderPersonModel>, InsolvencyOrderPersonCollectionResponse>();

            services.AddScoped<IHalFormatter<InsolvencyOrderAddressModel>, InsolvencyOrderAddressResponse>();
            services.AddScoped<IHalCollectionFormatter<InsolvencyOrderAddressModel>, InsolvencyOrderAddressCollectionResponse>();

            services.AddScoped<IHalFormatter<InsolvencyOrderHistoryModel>, InsolvencyOrderHistoryResponse>();
            services.AddScoped<IHalCollectionFormatter<InsolvencyOrderHistoryModel>, InsolvencyOrderHistoryCollectionResponse>();

            services.AddScoped<IHalFormatter<DisputeModel>, DisputeResponse>();
            services.AddScoped<IHalCollectionFormatter<DisputeModel>, DisputeCollectionResponse>();

            services.AddScoped<IHalFormatter<InsolvencyOrderTradingDetailsModel>, InsolvencyOrderTradingDetailsResponse>();
            services.AddScoped<IHalCollectionFormatter<InsolvencyOrderTradingDetailsModel>, InsolvencyOrderTradingDetailsCollectionResponse>();
        }
    }
}
