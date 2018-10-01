using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BittrexApi.NetCore.Entities
{
    public class Currency
    {
        [JsonProperty(PropertyName = "Currency")]
        public string symbol { get; set; }
        [JsonProperty(PropertyName = "CurrencyLong")]
        public string currencyName { get; set; }
        [JsonProperty(PropertyName = "MinConfirmation")]
        public long minConfirmation { get; set; }
        [JsonProperty(PropertyName = "TxFee")]
        public decimal txFee { get; set; }
        [JsonProperty(PropertyName = "IsActive")]
        public bool active { get; set; }
        [JsonProperty(PropertyName = "CoinType")]
        public string coinType { get; set; }
        [JsonProperty(PropertyName = "BaseAddress")]
        public string baseAddress { get; set; }
    }
}
