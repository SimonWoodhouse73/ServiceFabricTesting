// ***********************************************************************
// Assembly         : Api
// Author           : Team 7
// Created          : 03-02-2018
//
// Last Modified By : Team 7
// Last Modified On : 05-22-2018
// ***********************************************************************
// <copyright file="DataDomain.cs" company="Callcredit Information Group.">
//     Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>Defines definition and scope of the data access objects used in the Insolvency Service</summary>
// ***********************************************************************
using System.Diagnostics.CodeAnalysis;
using Api.EntityFramework.Configuration;
using Api.EntityFramework.Entities;
using Api.EntityFramework.Repositories;
using Callcredit.Domain.Insolvencies.Filters.Dispute;
using Callcredit.Domain.Insolvencies.Filters.InsolvencyOrder;
using Callcredit.Domain.Insolvencies.Models;
using Callcredit.Domain.Insolvencies.Repositories;
using Callcredit.Domain.Repositories;
using Callcredit.Domain.Repositories.GDPR;
using Callcredit.FirstInFirstOutFiltering;
using Callcredit.RESTful.DataAssets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.BootStrapping
{
    /// <summary>
    /// DataDomain class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class DataDomain
    {
        ////private const string InsolvenciesConnectionStringName = "Insolvencies";

        /// <summary>
        /// Adds the domain resources to the Insolvency Service.
        /// </summary>
        /// <param name="services">The services collection used to configure the Insolvency Service.</param>
        /// <param name="configuration">The configuration settings used in the Insolvency Service.</param>
        public static void AddDomainResources(this IServiceCollection services, IConfiguration configuration)
        {
            // Set up filters
            IConfigurationSection retentionConfigSection = configuration.GetSection("RetentionOptions");
            services.Configure<RetentionOptions>(retentionConfigSection);
            services.AddScoped<IOperationDateProvider, OperationDateProvider>();
            services.AddScoped<IFilteredBaseData<IInsolvencyOrderFilterBase>, InsolvencyOrderFilterContext>();
            services.AddScoped<IFilteredBaseData<IDisputeFilterBase>, DisputeFilterContext>();

            ModelMappingConfiguration.CreateModelMapping();

            // Insolvencies Repository
            services.AddScoped<IInsolvencyOrdersRepository<InsolvencyOrderModel, InsolvencyOrderEntity>, InsolvencyOrdersRepository>();
            services.AddScoped<IInsolvencyOrdersRepository<InsolvencyOrderModel, InsolvencyOrderFlattenedEntity>, InsolvencyOrdersFlattenedRepository>();
            services.AddScoped<IInsolvencyOrderPersonsRepository<InsolvencyOrderPersonModel, InsolvencyOrderPersonEntity>, InsolvencyOrderPersonsRepository>();
            services.AddScoped<IInsolvencyOrderAddressesRepository<InsolvencyOrderAddressModel, InsolvencyOrderAddressEntity>, InsolvencyOrderAddressesRepository>();
            services.AddScoped<IInsolvencyOrderHistoriesRepository<InsolvencyOrderHistoryModel, InsolvencyOrderHistoryEntity>, InsolvencyOrderHistoriesRepository>();
            services.AddScoped<IDisputesRepository<DisputeModel, DisputeEntity>, DisputesRepository>();
            services.AddScoped<IInsolvencyOrderTradingDetailsRepository<InsolvencyOrderTradingDetailsModel, InsolvencyTradingDetailsEntity>, InsolvencyOrderTradingDetailsRepository>();
        }
    }
}
