namespace Callcredit.Authorization.OnPremise.Validation.Providers
{
    using System;
    using System.Text;
    using Callcredit.Authorization.OnPremise.Configuration;
    using Callcredit.Authorization.OnPremise.Validation;
    using Microsoft.IdentityModel.Tokens;

    public class X509SigningCredentialsProvider : ISigningCredentialsProvider
    {
        private readonly IX509CertificateProvider _certificateProvider;
        private readonly IX509StoreProvider _x509StoreProvider;

        public X509SigningCredentialsProvider(IX509CertificateProvider certificateProvider, IX509StoreProvider x509StoreProvider)
        {
            _certificateProvider = certificateProvider;
            _x509StoreProvider = x509StoreProvider;
        }

        public SigningCredentials SigningCredentials(CertificateOptions certificateOptions)
        {
            var certificate = _certificateProvider.FindByThumbprint(certificateOptions, _x509StoreProvider);

            if (certificate == null)
            {
                throw new ArgumentNullException(nameof(certificate), "Certificate with provided thumbprint was not found.");
            }

            return new SigningCredentials(
                new SymmetricSecurityKey(Encoding.Default.GetBytes(certificate.PrivateKey.SignatureAlgorithm)),
                SecurityAlgorithms.HmacSha256Signature);
        }
    }
}
