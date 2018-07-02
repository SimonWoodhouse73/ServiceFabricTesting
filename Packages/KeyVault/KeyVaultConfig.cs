namespace Callcredit.KeyVault
{
    /// <summary>
    /// Config model for Key Vault Base URL and Authentication values.
    /// </summary>
    public class KeyVaultConfig
    {
        /// <summary>
        /// Gets or sets the Client Id for the Key Vault Authentication.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the Client Secret for the Key Vault Authentication.
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets the Base URL for the Key Vault.
        /// </summary>
        public string BaseUrl { get; set; }
    }
}
