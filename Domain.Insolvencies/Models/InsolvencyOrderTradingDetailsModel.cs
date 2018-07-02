using Newtonsoft.Json;

namespace Callcredit.Domain.Insolvencies.Models
{
    public class InsolvencyOrderTradingDetailsModel
    {
        public int InsolvencyOrderTradingDetailsId { get; set; }

        [JsonIgnore]
        public int InsolvencyOrderId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }
    }
}
