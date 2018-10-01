using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BittrexApi.NetCore.Entities
{
    public class OpenOrder
    {
        [JsonProperty(PropertyName = "Uuid")]
        public string id { get; set; }
        [JsonProperty(PropertyName = "OrderUuid")]
        public string orderId { get; set; }
        [JsonProperty(PropertyName = "Exchange")]
        public string pair { get; set; }
        [JsonProperty(PropertyName = "OrderType")]
        public string orderType { get; set; }
        [JsonProperty(PropertyName = "Quantity")]
        public decimal quantity { get; set; }
        [JsonProperty(PropertyName = "QuantityRemaining")]
        public decimal quantityRemaining { get; set; }
        [JsonProperty(PropertyName = "CommissionPaid")]
        public decimal commissionPaid { get; set; }
        [JsonProperty(PropertyName = "Price")]
        public decimal price { get; set; }
        [JsonProperty(PropertyName = "PricePerUnit")]
        public decimal? pricePerUnit { get; set; }
        [JsonProperty(PropertyName = "Opened")]
        public DateTime opened { get; set; }
        [JsonProperty(PropertyName = "Closed")]
        public DateTime? closed { get; set; }
        [JsonProperty(PropertyName = "CancelInitiated")]
        public bool cancelInitiated { get; set; }
        [JsonProperty(PropertyName = "ImmediateOrCancel")]
        public bool immediateOrCancel { get; set; }
        [JsonProperty(PropertyName = "IsConditional")]
        public bool isConditional { get; set; }
        [JsonProperty(PropertyName = "Condition")]
        public string condition { get; set; }
        [JsonProperty(PropertyName = "ConditionTarget")]
        public string conditionTarget { get; set; }
    }
}
