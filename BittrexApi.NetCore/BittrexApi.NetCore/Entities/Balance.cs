using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BittrexApi.NetCore.Entities
{
    public class Balance
    {
        [JsonProperty(PropertyName = "Currency")]
        public string symbol { get; set; }
        [JsonProperty(PropertyName = "Balance")]
        public decimal balance { get; set; }
        [JsonProperty(PropertyName = "Available")]
        public decimal available { get; set; }
        [JsonProperty(PropertyName = "Pending")]
        public decimal pending { get; set; }
        [JsonProperty(PropertyName = "CryptoAddress")]
        public string address { get; set; }
        [JsonProperty(PropertyName = "Requested")]
        public bool requested { get; set; }
        [JsonProperty(PropertyName = "Uuid")]
        public string id { get; set; }
    }
}
