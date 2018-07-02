using System;
using Callcredit.Domain.Insolvencies.Helpers;
using Newtonsoft.Json;

namespace Callcredit.Domain.Insolvencies.Models
{
    public class InsolvencyOrderPersonModel
    {
       
        public int InsolvencyOrderPersonId { get; set; }

        [JsonIgnore]
        public int InsolvencyOrderId { get; set; }

        public string Title { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        [JsonConverter(typeof(DateOnlyDateTimeConverter))]
        public DateTime? DateOfBirth { get; set; }
    }
}
