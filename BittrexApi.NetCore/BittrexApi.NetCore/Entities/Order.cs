using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BittrexApi.NetCore.Entities
{
    public class Order
    {
        [JsonProperty(PropertyName = "OrderUuid")]
        public string orderId { get; set; }
        [JsonProperty(PropertyName = "Exchange")]
        public string pair { get; set; }
        [JsonProperty(PropertyName = "Type")]
        public string orderType { get; set; }
        [JsonProperty(PropertyName = "Quantity")]
        public decimal quantity { get; set; }
        [JsonProperty(PropertyName = "QuantityRemaining")]
        public decimal quantityRemaining { get; set; }
        [JsonProperty(PropertyName = "Limit")]
        public decimal limit { get; set; }
        [JsonProperty(PropertyName = "Commission")]
        public decimal commission { get; set; }
        [JsonProperty(PropertyName = "Price")]
        public decimal price { get; set; }
        [JsonProperty(PropertyName = "PricePerUnit")]
        public decimal? pricePerUnit { get; set; }
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
