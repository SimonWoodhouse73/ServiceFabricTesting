using System.Threading.Tasks;

namespace Callcredit.EntityFramework.Secrets.KeyVault
{
    /// <summary>
    /// Provides a mechanism for retrieving and updating connection strings.
    /// </summary>
    public interface IConnectionStringRetriever
    {
        /// <summary>
        /// Retrieves the connection string.
        /// </summary>
        /// <param name="connectionStringKey">The Key of the connection string in Key Vault.</param>
        /// <returns>A <see cref="Task{TString}"/> that represents the retrieval operation, the result contains the connection string.</returns>
        Task<string> RetrieveAsync(string connectionStringKey);
    }
}
