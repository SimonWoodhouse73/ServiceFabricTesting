using Newtonsoft.Json;

namespace Callcredit.Domain.Insolvencies.Models
{
    public class CourtModel
    {
        [JsonIgnore]
        public int CourtId { get; set; }

        public string Code { get; set; }

        public string District { get; set; }

        public string Name { get; set; } // CR: "Court"

    }
}
