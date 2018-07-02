using Newtonsoft.Json;

namespace Callcredit.Domain.Insolvencies.Models
{
    public class InsolvencyOrderRestrictionsTypeModel
    {
        [JsonIgnore]
        public short RestrictionsTypeId { get; set; }

        public string Code { get; set; } // CR: "udtRestrictionType"

        public string Description { get; set; }

       
    }
}
