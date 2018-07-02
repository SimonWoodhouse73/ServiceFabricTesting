using System;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using Validation;

namespace EntityFramework.Secrets.KeyVault
{
    public class KeyVaultExecutionStrategy : IExecutionStrategy
    {
        private static readonly int[] SqlConnectionErrorCodes =
        {
            4060,
            11004,
            18456
        };

        private readonly IConnectionStringUpdater connectionStringUpdater;
        private readonly IDatabaseContextEventSource databaseContextEventSource;
        private readonly ConnectionStringSettings connectionStringSettings;
        private readonly DbContext dbContext;

        /// <summary>
        /// Gets the Retries on Failure, indicating whether the operation will be retried.
        /// </summary>
        /// <remarks>This is not used, and only serves the purpose of satisfying the implementation requirement of the <see cref="IExecutionStrategy"/> interface.</remarks>
        [ExcludeFromCodeCoverage]
        public bool RetriesOnFailure { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyVaultExecutionStrategy"/> class.
        /// </summary>
        /// <param name="databaseContextEventSource">The <see cref="IDatabaseContextEventSource"/> used to write errors to the event source.</param>
        /// <param name="connectionStringUpdater">The <see cref="IConnectionStringUpdater"/> used to update the connection string.</param>
        /// <param name="connectionStringSettings">The <see cref="IOptions{TConnectionStringSettings}"/> instance containing the <see cref="ConnectionStringSettings"/>.</param>
        /// <param name="dbContext">The <see cref="DbContext"/> executing the operation.</param>
        public KeyVaultExecutionStrategy(
            IDatabaseContextEventSource databaseContextEventSource,
            IConnectionStringUpdater connectionStringUpdater,
            IOptions<ConnectionStringSettings> connectionStringSettings,
            DbContext dbContext)
        {
            Requires.NotNull(databaseContextEventSource, nameof(databaseContextEventSource));
            Requires.NotNull(connectionStringUpdater, nameof(connectionStringUpdater));
            Requires.NotNull(connectionStringSettings, nameof(connectionStringSettings));
            Requires.NotNull(connectionStringSettings.Value, $"{nameof(connectionStringSettings)}.Value");
            Requires.NotNull(dbContext, nameof(dbContext));

            this.dbContext = dbContext;
            this.connectionStringSettings = connectionStringSettings.Value;
            this.connectionStringUpdater = connectionStringUpdater;
            this.databaseContextEventSource = databaseContextEventSource;
        }

        /// <inheritdoc />
        public TResult Execute<TState, TResult>(
            TState state,
            Func<DbContext, TState, TResult> operation,
            Func<DbContext, TState, ExecutionResult<TResult>> verifySucceeded)
        {
            try
            {
                return operation(dbContext, state);
            }
            catch (SqlException sqlException)
            {
                LogSqlProblem(sqlException);
                if (IsConnectionStringError(sqlException))
                {
                    UpdateConnectionStringAsync().GetAwaiter().GetResult();
                }

                throw;
            }
        }

        /// <inheritdoc />
        public async Task<TResult> ExecuteAsync<TState, TResult>(
            TState state,
            Func<DbContext, TState, CancellationToken, Task<TResult>> operation,
            Func<DbContext, TState, CancellationToken, Task<ExecutionResult<TResult>>> verifySucceeded,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                return await operation(dbContext, state, cancellationToken);
            }
            catch (SqlException sqlException)
            {
                LogSqlProblem(sqlException);
                if (IsConnectionStringError(sqlException))
                {
                    await UpdateConnectionStringAsync();
                }

                throw;
            }
        }

        private static bool IsConnectionStringError(SqlException exception)
            => SqlConnectionErrorCodes.Contains(exception.Number);

        private Task UpdateConnectionStringAsync()
            => connectionStringUpdater.UpdateAsync(connectionStringSettings.KeyVaultKey);

        private void LogSqlProblem(SqlException exception) =>
            databaseContextEventSource.SqlProblemOccured(dbContext.GetType().FullName, exception.Message);
    }
}
