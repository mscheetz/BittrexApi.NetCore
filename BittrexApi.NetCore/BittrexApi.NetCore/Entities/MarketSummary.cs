using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BittrexApi.NetCore.Entities
{
    public class MarketSummary
    {
        [JsonProperty(PropertyName = "MarketName")]
        public string pair { get; set; }
        [JsonProperty(PropertyName = "High")]
        public decimal high { get; set; }
        [JsonProperty(PropertyName = "Low")]
        public decimal low { get; set; }
        [JsonProperty(PropertyName = "Volume")]
        public decimal volume { get; set; }
        [JsonProperty(PropertyName = "Last")]
        public decimal last { get; set; }
        [JsonProperty(PropertyName = "BaseVolume")]
        public decimal baseVolume { get; set; }
        [JsonProperty(PropertyName = "TimeStamp")]
        public DateTime timestamp { get; set; }
        [JsonProperty(PropertyName = "Bid")]
        public decimal bid { get; set; }
        [JsonProperty(PropertyName = "Ask")]
        public decimal ask { get; set; }
        [JsonProperty(PropertyName = "OpenBuyOrders")]
        public int openBuys { get; set; }
        [JsonProperty(PropertyName = "OpenSellOrders")]
        public int openSells { get; set; }
        [JsonProperty(PropertyName = "PrevDay")]
        public decimal prevDay { get; set; }
        [JsonProperty(PropertyName = "Created")]
        public DateTime created { get; set; }
        [JsonProperty(PropertyName = "DisplayMarketName")]
        public string marketName { get; set; }
    }
}
