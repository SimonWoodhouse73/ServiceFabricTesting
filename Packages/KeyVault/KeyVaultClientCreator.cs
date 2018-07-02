using System;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Validation;

namespace Callcredit.KeyVault
{
    /// <summary>
    /// Provides a method for creating Key Vault Clients.
    /// </summary>
    public class KeyVaultClientCreator : IKeyVaultClientCreator
    {
        private readonly KeyVaultConfig keyVaultConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyVaultClientCreator"/> class.
        /// </summary>
        /// <param name="keyVaultConfigOptions"><see cref="IOptions{TOptions}"/> which should be registered on the service, and contain the Key Vault authorization info.</param>
        public KeyVaultClientCreator(IOptions<KeyVaultConfig> keyVaultConfigOptions)
        {
            Requires.NotNull(keyVaultConfigOptions, nameof(keyVaultConfigOptions));
            Requires.NotNull(keyVaultConfigOptions.Value, $"{nameof(keyVaultConfigOptions)}.Value");

            keyVaultConfig = keyVaultConfigOptions.Value;
        }

        /// <summary>
        /// Creates a client to perform cryptographic key operations and vault operations against the Key Vault service.
        /// </summary>
        /// <returns><see cref="KeyVaultClient"/></returns>
        public IKeyVaultClient CreateClient()
            => new KeyVaultClient(GetToken);

        private async Task<string> GetToken(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);
            var clientCred = new ClientCredential(keyVaultConfig.ClientId, keyVaultConfig.ClientSecret);
            var result = await authContext.AcquireTokenAsync(resource, clientCred);

            if (result == null)
            {
                throw new InvalidOperationException("Failed to obtain the JWT token");
            }

            return result.AccessToken;
        }
    }
}
