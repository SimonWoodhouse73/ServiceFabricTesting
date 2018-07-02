using System;
using System.Collections.Generic;
using Callcredit.Domain.Insolvencies.Helpers;
using Newtonsoft.Json;

namespace Callcredit.Domain.Insolvencies.Models
{
    public class InsolvencyOrderModel
    {
        public InsolvencyOrderModel()
        {
            this.InsolvencyOrderType = new InsolvencyOrderTypeModel();
            this.RestrictionsType = new InsolvencyOrderRestrictionsTypeModel();

            this.Disputes = new List<DisputeModel>();
            this.InsolvencyOrderAddresses = new List<InsolvencyOrderAddressModel>();
            this.InsolvencyOrderHistories = new List<InsolvencyOrderHistoryModel>();
            this.InsolvencyOrderPersons = new List<InsolvencyOrderPersonModel>();
            this.InsolvencyOrderTradingDetails = new List<InsolvencyOrderTradingDetailsModel>();
        }

        public int InsolvencyOrderId { get; set; } // CR: "udtBAICasePersonId"

        [JsonIgnore]
        public short? InsolvencyOrderTypeId { get; set; }

        public InsolvencyOrderTypeModel InsolvencyOrderType { get; set; }

        public int? ResidenceId { get; set; } // CR: "udtResidenceId"

        [JsonConverter(typeof(DateOnlyDateTimeConverter))]
        public DateTime? OrderDate { get; set; } // CR: "udtOrderDate"

        [JsonConverter(typeof(DateOnlyDateTimeConverter))]
        public DateTime? DischargeDate { get; set; } // CR: "udtDischargeDate"

        public string LineOfBusiness { get; set; } // CR: "udtLineOfBusiness"

        public int? InsolvencyServiceCaseId { get; set; }

        [JsonIgnore]
        public short? RestrictionsTypeId { get; set; }

        public InsolvencyOrderRestrictionsTypeModel RestrictionsType { get; set; }

        [JsonConverter(typeof(DateOnlyDateTimeConverter))]
        public DateTime? RestrictionsStartDate { get; set; } // CR: "udtRestrictionStartDate"

        [JsonConverter(typeof(DateOnlyDateTimeConverter))]
        public DateTime? RestrictionsEndDate { get; set; } // CR: "udtRestrictionEndDate"

        public decimal? ValueOfDebt { get; set; } // CR: "udtAmount"

        [JsonIgnore]
        public bool OnlineSuppressed { get; set; }

        [JsonIgnore]
        public List<DisputeModel> Disputes { get; set; }

        [JsonIgnore]
        public List<InsolvencyOrderAddressModel> InsolvencyOrderAddresses { get; set; }

        [JsonIgnore]
        public List<InsolvencyOrderHistoryModel> InsolvencyOrderHistories { get; set; }

        [JsonIgnore]
        public List<InsolvencyOrderPersonModel> InsolvencyOrderPersons { get; set; }

        [JsonIgnore]
        public List<InsolvencyOrderTradingDetailsModel> InsolvencyOrderTradingDetails { get; set; }
    }
}
