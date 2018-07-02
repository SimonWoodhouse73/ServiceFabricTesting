using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Validation;

namespace Callcredit.KeyVault
{
    /// <summary>
    /// Provides a method for retrieving secrets from the key vault.
    /// </summary>
    public class KeyVaultRepository : IKeyVaultRepository
    {
        private readonly IKeyVaultClientCreator keyVaultClientCreator;
        private readonly KeyVaultConfig keyVaultConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyVaultRepository"/> class.
        /// </summary>
        /// <param name="keyVaultClientCreator">Implementation of <see cref="IKeyVaultClientCreator"/> which is used to create a Key Vault Client.</param>
        /// <param name="keyVaultConfigOptions"><see cref="IOptions{TOptions}"/> which should be registered on the service, and contain the Key Vault authorization info.</param>
        public KeyVaultRepository(IKeyVaultClientCreator keyVaultClientCreator, IOptions<KeyVaultConfig> keyVaultConfigOptions)
        {
            Requires.NotNull(keyVaultClientCreator, nameof(keyVaultClientCreator));
            Requires.NotNull(keyVaultConfigOptions, nameof(keyVaultConfigOptions));
            Requires.NotNull(keyVaultConfigOptions.Value, $"{nameof(keyVaultConfigOptions)}.Value");

            this.keyVaultClientCreator = keyVaultClientCreator;
            keyVaultConfig = keyVaultConfigOptions.Value;
        }

        /// <summary>
        /// Retrieves the specified secret from the key vault.
        /// </summary>
        /// <param name="secretKey">The key used to access the secret in the key vault.</param>
        /// <returns>A <see cref="Task"/> representing the secret from the key vault.</returns>
        public async Task<string> GetSecretAsync(string secretKey)
        {
            Requires.NotNullOrEmpty(secretKey, nameof(secretKey));

            using (var keyVault = keyVaultClientCreator.CreateClient())
            {
                using (var secret = await keyVault.GetSecretWithHttpMessagesAsync(keyVaultConfig.BaseUrl, secretKey, string.Empty))
                {
                    return secret.Body.Value;
                }
            }
        }
    }
}
