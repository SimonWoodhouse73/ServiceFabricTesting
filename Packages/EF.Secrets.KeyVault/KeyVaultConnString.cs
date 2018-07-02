using System.Threading.Tasks;
using Callcredit.KeyVault;
using Validation;

namespace EntityFramework.Secrets.KeyVault
{
    /// <summary>
    /// Provides an implementation of the <see cref="IConnectionStringRetriever"/> and <see cref="IConnectionStringUpdater"/> interfaces.
    /// </summary>
    public class KeyVaultConnectionStringRetriever : IConnectionStringRetriever, IConnectionStringUpdater
    {
        private readonly IKeyVaultRepository keyVaultRepository;

        private static string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyVaultConnectionStringRetriever"/> class.
        /// </summary>
        /// <param name="keyVaultRepository"><see cref="IKeyVaultRepository"/> implementation.</param>
        public KeyVaultConnectionStringRetriever(IKeyVaultRepository keyVaultRepository)
        {
            Requires.NotNull(keyVaultRepository, nameof(keyVaultRepository));

            this.keyVaultRepository = keyVaultRepository;
        }

        /// <inheritdoc />
        public async Task<string> RetrieveAsync(string connectionStringKey)
        {
            Requires.NotNullOrEmpty(connectionStringKey, nameof(connectionStringKey));

            if (string.IsNullOrEmpty(connectionString))
            {
                await UpdateAsync(connectionStringKey);
            }

            return connectionString;
        }

        /// <inheritdoc />
        public async Task UpdateAsync(string connectionStringKey)
        {
            Requires.NotNullOrEmpty(connectionStringKey, nameof(connectionStringKey));

            connectionString = await keyVaultRepository.GetSecretAsync(connectionStringKey);
        }
    }
}
