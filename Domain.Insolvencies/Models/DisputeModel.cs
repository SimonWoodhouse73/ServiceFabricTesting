using System;
using Callcredit.Domain.Insolvencies.Filters.Dispute;
using Callcredit.Domain.Insolvencies.Helpers;
using Newtonsoft.Json;

namespace Callcredit.Domain.Insolvencies.Models
{
    public class DisputeModel : IDisputeFilterBase
    {
        public int DisputeId { get; set; }

        [JsonIgnore]
        public int InsolvencyOrderId { get; set; }

        public string ReferenceNumber { get; set; }

        [JsonConverter(typeof(DateOnlyDateTimeConverter))]
        public DateTime? DateRaised { get; set; }

        public bool? Displayed { get; set; }
    }
}
