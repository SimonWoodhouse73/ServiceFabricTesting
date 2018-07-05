using System.Collections.Generic;

namespace Callcredit.Authorization.OnPremise.Configuration
{
    public class CustomClaim
    {
        public string ClaimType { get; set; }

        public string Value { get; set; }
    }
    
    

    public class SecurityConfiguration
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public List<CustomClaim> CustomClaims { get; set; }

        public int ExpiryMinutes { get; set; }

        public CertificateOptions CertificateOptions { get; set; }
    }
}
