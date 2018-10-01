using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BittrexApi.NetCore.Entities
{
    public class OrderDetail : Order
    {
        [JsonProperty(PropertyName = "AccountId")]
        public string accountId { get; set; }
        [JsonProperty(PropertyName = "Reserved")]
        public decimal reserved { get; set; }
        [JsonProperty(PropertyName = "ReserveRemaining")]
        public decimal reserveRemaining { get; set; }
        [JsonProperty(PropertyName = "CommissionReserved")]
        public decimal commissionReserved { get; set; }
        [JsonProperty(PropertyName = "CommissionReserveRemaining")]
        public decimal commissionReserveRemaining { get; set; }
        [JsonProperty(PropertyName = "CommissionPaid")]
        public decimal commissionPaid { get; set; }
        [JsonProperty(PropertyName = "Opened")]
        public DateTime opened { get; set; }
        [JsonProperty(PropertyName = "Closed")]
        public DateTime? closed { get; set; }
        [JsonProperty(PropertyName = "IsOpen")]
        public bool isOpen { get; set; }
        [JsonProperty(PropertyName = "Sentinel")]
        public string sentinel { get; set; }
        [JsonProperty(PropertyName = "CancelInitiated")]
        public bool cancelInitiated { get; set; }
    }
}
