using Newtonsoft.Json;

namespace Callcredit.Domain.Insolvencies.Models
{
    public class InsolvencyOrderHistoryModel
    {
        public InsolvencyOrderHistoryModel()
        {
            this.InsolvencyOrderStatus = new InsolvencyOrderStatusModel();
            this.Court = new CourtModel();
        }

        public int InsolvencyOrderHistoryId { get; set; }

        [JsonIgnore]
        public int InsolvencyOrderId { get; set; }

        [JsonIgnore]
        public short? InsolvencyOrderStatusId { get; set; }

        // todo: logical model suggests order status can be property on parent Insolvency class.
        //       would only work if both history and order status are 'last' / 'current' i.e. single record
        public InsolvencyOrderStatusModel InsolvencyOrderStatus { get; set; }

        [JsonIgnore]
        public int? CourtId { get; set; }

        public CourtModel Court { get; set; }

        public string CaseReference { get; set; }

        public short? CaseYear { get; set; }
    }
}
