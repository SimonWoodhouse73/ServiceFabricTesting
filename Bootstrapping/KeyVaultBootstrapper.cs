using System.Diagnostics.CodeAnalysis;
using Api.EntityFramework;
using Callcredit.EntityFramework.Secrets.KeyVault;
using Callcredit.KeyVault;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Api.BootStrapping
{
    /// <summary>
    /// Class KeyVaultBootstrapper.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class KeyVaultBootstrapper
    {
        /// <summary>
        /// Adds the key vault components.
        /// </summary>
        /// <param name="services">The services collection used to configure the Insolvency Service.</param>
        /// <param name="configuration">The configuration settings used in the Insolvency Service.</param>
        public static void AddKeyVaultComponents(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStringSettings = configuration.GetSection("ConnectionStringSettings").Get<ConnectionStringSettings>();
            var connectionStringOptions = Options.Create(connectionStringSettings);

            services.AddSingleton(connectionStringOptions);

            services.Configure<KeyVaultConfig>(configuration.GetSection("KeyVaultConfig"));

            services.AddScoped<IKeyVaultRepository, KeyVaultRepository>();
            services.AddScoped<IKeyVaultClientCreator, KeyVaultClientCreator>();

            services.AddScoped<IConnectionStringRetriever, KeyVaultConnectionStringRetriever>();
            services.AddScoped<IConnectionStringUpdater, KeyVaultConnectionStringRetriever>();

            services.AddDbContext<DatabaseContext>(
                connectionStringOptions,
                sqlOptionsBuilder => sqlOptionsBuilder.UseRowNumberForPaging());
        }
    }
}
