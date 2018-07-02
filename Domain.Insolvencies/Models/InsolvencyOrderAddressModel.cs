using Newtonsoft.Json;

namespace Callcredit.Domain.Insolvencies.Models
{
    public class InsolvencyOrderAddressModel
    {
        public int InsolvencyOrderAddressId { get; set; } // CR: select top 1 InsolvencyOrderAddressId ... order by InsolvencyOrderAddressId desc

        [JsonIgnore]
        public int InsolvencyOrderId { get; set; }

        public string Address { get; set; } // CR: isnull(InsOA.LastKnownAddress,'') + ' ' + isnull(InsOA.LastKnownPostCode,'')

        public string PostCode { get; set; }
    }
}
