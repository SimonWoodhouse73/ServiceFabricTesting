using System.Threading.Tasks;

namespace Callcredit.EntityFramework.Secrets.KeyVault
{
    /// <summary>
    /// Provides a mechanism for updating connection strings from Key Vault.
    /// </summary>
    public interface IConnectionStringUpdater
    {
        /// <summary>
        /// Updates the connection string.
        /// </summary>
        /// <param name="connectionStringKey">The Key of the connection string in Key Vault.</param>
        /// <returns>A <see cref="Task"/> that represents the update operation.</returns>
        Task UpdateAsync(string connectionStringKey);
    }
}
