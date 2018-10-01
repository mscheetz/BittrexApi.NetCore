using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BittrexApi.NetCore.Entities
{
    public class OrderInterval
    {
        [JsonProperty(PropertyName = "Quantity")]
        public decimal quantity { get; set; }
        [JsonProperty(PropertyName = "Rate")]
        public decimal rate { get; set; }
    }
}
