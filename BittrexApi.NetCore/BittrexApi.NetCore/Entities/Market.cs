using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BittrexApi.NetCore.Entities
{
    public class Market
    {
        [JsonProperty(PropertyName = "MarketCurrency")]
        public string symbol { get; set; }
        [JsonProperty(PropertyName = "BaseCurrency")]
        public string baseCurrency { get; set; }
        [JsonProperty(PropertyName = "MarketCurrencyLong")]
        public string marketCurrencyName { get; set; }
        [JsonProperty(PropertyName = "BaseCurrencyLong")]
        public string baseCurrencyName { get; set; }
        [JsonProperty(PropertyName = "MinTradeSize")]
        public decimal minTradeSize { get; set; }
        [JsonProperty(PropertyName = "MarketName")]
        public string pair { get; set; }
        [JsonProperty(PropertyName = "IsActive")]
        public bool active { get; set; }
        [JsonProperty(PropertyName = "Created")]
        public DateTime created { get; set; }
    }
}
