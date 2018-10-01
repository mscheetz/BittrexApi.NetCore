using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BittrexApi.NetCore.Entities
{
    public class MarketHistory
    {
        [JsonProperty(PropertyName = "MarketName")]
        public long id { get; set; }
        [JsonProperty(PropertyName = "TimeStamp")]
        public DateTime timestamp { get; set; }
        [JsonProperty(PropertyName = "Quantity")]
        public decimal quantity { get; set; }
        [JsonProperty(PropertyName = "Price")]
        public decimal price { get; set; }
        [JsonProperty(PropertyName = "Total")]
        public decimal total { get; set; }
        [JsonProperty(PropertyName = "FillType")]
        public string fillType { get; set; }
        [JsonProperty(PropertyName = "OrderType")]
        public OrderType orderType { get; set; }

    }
}
