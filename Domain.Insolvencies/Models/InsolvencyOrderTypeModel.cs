using Newtonsoft.Json;

namespace Callcredit.Domain.Insolvencies.Models
{
    public class InsolvencyOrderTypeModel
    {
        public short InsolvencyOrderTypeId { get; set; }

        public string Code { get; set; } // CR: "udtOrderType"

        public string Description { get; set; }

    }
}
