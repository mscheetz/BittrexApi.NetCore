using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BittrexApi.NetCore.Entities
{
    public class Ticker
    {
        [JsonProperty(PropertyName = "Bid")]
        public decimal bid { get; set; }
        [JsonProperty(PropertyName = "Ask")]
        public decimal ask { get; set; }
        [JsonProperty(PropertyName = "Last")]
        public decimal last { get; set; }
    }
}
