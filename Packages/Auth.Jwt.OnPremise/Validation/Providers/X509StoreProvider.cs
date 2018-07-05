namespace Callcredit.Authorization.OnPremise.Validation.Providers
{
    using System.Security.Cryptography.X509Certificates;

    public class X509StoreProvider : IX509StoreProvider
    {
        private X509Store _store;

        public X509Certificate2Collection Certificates => _store?.Certificates;

        public void Initialize(StoreName storeName, StoreLocation storeLocation)
        {
            _store?.Dispose();
            _store = new X509Store(storeName, storeLocation);
        }

        public void Open(OpenFlags flags)
        {
            _store?.Open(flags);
        }

        public void Dispose()
        {
            _store?.Dispose();
        }
    }
}
