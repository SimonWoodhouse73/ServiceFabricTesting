using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Validation;

namespace Callcredit.EntityFramework.Secrets.KeyVault
{
    /// <summary>
    /// Provides extensions methods for registering a <see cref="DbContext"/> with refreshing connection strings.
    /// </summary>
    public static class EntityFrameworkExtensions
    {
        /// <summary>
        /// Registers a <see cref="DbContext"/> in the <see cref="IServiceCollection"/>, which uses Key Vault to retrieve the connection string.
        /// </summary>
        /// <typeparam name="TDbContext">The custom <see cref="DbContext"/> type.</typeparam>
        /// <param name="serviceCollection"><see cref="IServiceCollection"/> from the calling application.</param>
        /// <param name="connectionStringOptions">The connection string key in Key Vault.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddDbContext<TDbContext>(
            this IServiceCollection serviceCollection,
            IOptions<ConnectionStringSettings> connectionStringOptions)
            where TDbContext : DbContext
            => serviceCollection.AddDbContext<TDbContext>(connectionStringOptions, optionsBuilder => { });

        /// <summary>
        /// Registers a <see cref="DbContext"/> in the <see cref="IServiceCollection"/>, which uses Key Vault to retrieve the connection string.
        /// </summary>
        /// <typeparam name="TDbContext">The custom <see cref="DbContext"/> type.</typeparam>
        /// <param name="serviceCollection"><see cref="IServiceCollection"/> from the calling application.</param>
        /// <param name="connectionStringOptions">The connection string key in Key Vault.</param>
        /// <param name="sqlOptionsBuilder">Custom <see cref="SqlServerDbContextOptionsBuilder"/> delegate describing how to configure the Sql Server behaviour.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddDbContext<TDbContext>(
            this IServiceCollection serviceCollection,
            IOptions<ConnectionStringSettings> connectionStringOptions,
            Action<SqlServerDbContextOptionsBuilder> sqlOptionsBuilder)
            where TDbContext : DbContext
        {
            Requires.NotNull(serviceCollection, nameof(serviceCollection));
            Requires.NotNull(sqlOptionsBuilder, nameof(sqlOptionsBuilder));
            Requires.NotNull(connectionStringOptions, nameof(connectionStringOptions));
            Requires.NotNull(connectionStringOptions.Value, $"{nameof(connectionStringOptions)}.Value");

            serviceCollection.AddDbContext<TDbContext>(
                (services, builder) =>
                {
                    var connectionStringResolver = services.GetService<IConnectionStringRetriever>();
                    var connectionString = connectionStringResolver.RetrieveAsync(connectionStringOptions.Value.KeyVaultKey).GetAwaiter().GetResult();

                    builder.UseSqlServer(
                        connectionString,
                        sqlOptionsBuilder);
                },
                optionsLifetime: ServiceLifetime.Singleton);

            return serviceCollection;
        }
    }
}
