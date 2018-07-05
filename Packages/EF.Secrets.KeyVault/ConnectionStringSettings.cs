using System.Diagnostics.CodeAnalysis;

namespace Callcredit.EntityFramework.Secrets.KeyVault
{
    [ExcludeFromCodeCoverage]
    public class ConnectionStringSettings
    {
        /// <summary>
        /// Gets or sets the Key of the Connection String secret as it is shown in Key Vault.
        /// </summary>
        public string KeyVaultKey { get; set; }
    }
}
