using System.Threading.Tasks;

namespace Callcredit.KeyVault
{
    /// <summary>
    /// Provides a mechanism for fetching secrets from a Key Vault.
    /// </summary>
    public interface IKeyVaultRepository
    {
        /// <summary>
        /// Gets a secret from a Key Vault.
        /// </summary>
        /// <param name="secretKey">The ID of the secret to retrieve.</param>
        /// <returns><see cref="Task{TEntity}"/> containing the secret value.</returns>
        Task<string> GetSecretAsync(string secretKey);
    }
}
