using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using Callcredit.Domain.Insolvencies.Models;
using Callcredit.RESTful.DataAssets;
using Callcredit.RESTful.Standards.Inclusions;
using Microsoft.Extensions.DependencyInjection;

namespace Api.BootStrapping
{
    /// <summary>
    /// Class RestfulDataAsset.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class RestfulDataAsset
    {
        /// <summary>
        /// Adds the data access cradle.
        /// </summary>
        /// <param name="services">The services collection used to configure the Insolvency Service.</param>
        public static void AddDataAccessCradle(this IServiceCollection services)
        {
            services.AddScoped<IDataAccessCradle<InsolvencyOrderModel>, DataAccessCradle<InsolvencyOrderModel, SqlException>>();
            services.AddScoped<IDataAccessCradle<InsolvencyOrderPersonModel>, DataAccessCradle<InsolvencyOrderPersonModel, SqlException>>();
            services.AddScoped<IDataAccessCradle<InsolvencyOrderAddressModel>, DataAccessCradle<InsolvencyOrderAddressModel, SqlException>>();
            services.AddScoped<IDataAccessCradle<InsolvencyOrderHistoryModel>, DataAccessCradle<InsolvencyOrderHistoryModel, SqlException>>();
            services.AddScoped<IDataAccessCradle<DisputeModel>, DataAccessCradle<DisputeModel, SqlException>>();
            services.AddScoped<IDataAccessCradle<InsolvencyOrderTradingDetailsModel>, DataAccessCradle<InsolvencyOrderTradingDetailsModel, SqlException>>();
            services.AddScoped<IInclusionSizeValidator, InclusionSizeValidator>();
        }
    }
}
