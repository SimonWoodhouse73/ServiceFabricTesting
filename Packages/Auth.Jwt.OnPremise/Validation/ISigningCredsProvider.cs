namespace Callcredit.Authorization.OnPremise.Validation
{
    using System;
    using Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System.Security.Cryptography.X509Certificates;
    

    public interface ISigningCredentialsProvider
    {
        SigningCredentials SigningCredentials(CertificateOptions certificateOptions);
    }
    
    

    public interface IX509CertificateProvider
    {
        X509Certificate2 FindByThumbprint(CertificateOptions certificateOptions, IX509StoreProvider x509StoreProvider);
    }
    
    
    

    public interface IX509StoreProvider : IDisposable
    {
        void Initialize(StoreName storeName, StoreLocation storeLocation);

        X509Certificate2Collection Certificates { get; }

        void Open(OpenFlags flags);
    }
}
