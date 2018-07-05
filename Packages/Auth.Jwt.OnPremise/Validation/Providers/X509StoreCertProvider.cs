using Validation;

namespace Callcredit.Authorization.OnPremise.Validation.Providers
{
    using System;
    using System.Security.Cryptography.X509Certificates;
    using Configuration;

    public class X509StoreCertificateProvider : IX509CertificateProvider
    {
        public X509Certificate2 FindByThumbprint(CertificateOptions options, IX509StoreProvider x509StoreProvider)
        {
            Requires.That(
                Enum.TryParse(options.StoreName, true, out StoreName storeNameEnum),
                nameof(options.StoreName),
                "Not a valid store name");

            Requires.That(
                Enum.TryParse(options.StoreLocation, true, out StoreLocation storeLocationEnum),
                nameof(options.StoreLocation),
                "Not a valid store location");

            Requires.NotNullOrWhiteSpace(options.Thumbprint, nameof(options.Thumbprint));

            x509StoreProvider.Initialize(storeNameEnum, storeLocationEnum);

            using (var store = x509StoreProvider)
            {
                store.Open(OpenFlags.ReadOnly);

                var certificates = store.Certificates.Find(X509FindType.FindByThumbprint, options.Thumbprint, false);

                return certificates.Count >= 1 ? certificates[0] : null;
            }
        }
    }
}
