using Newtonsoft.Json;

namespace Callcredit.Domain.Insolvencies.Models
{
    public class InsolvencyOrderStatusModel
    {
        public short InsolvencyOrderStatusId { get; set; }

        public string Code { get; set; } // CR: "CurrentStatus" (most recent record)

        public string Description { get; set; }
    }
}
