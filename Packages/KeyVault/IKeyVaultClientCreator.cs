using Microsoft.Azure.KeyVault;

namespace Callcredit.KeyVault
{
    /// <summary>
    /// Provides a mechanism for abstracting the creation of a Key Vault Client.
    /// </summary>
    public interface IKeyVaultClientCreator
    {
        /// <summary>
        /// Creates an instance of <see cref="IKeyVaultClient"/>.
        /// </summary>
        /// <returns>A <see cref="IKeyVaultClient"/>.</returns>
        IKeyVaultClient CreateClient();
    }
}
