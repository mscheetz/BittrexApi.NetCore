using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BittrexApi.NetCore.Entities
{
    public class DepositWithdrawal
    {
        [JsonProperty(PropertyName = "PaymentUuid")]
        public string paymentId { get; set; }
        [JsonProperty(PropertyName = "Currency")]
        public string symbol { get; set; }
        [JsonProperty(PropertyName = "Amount")]
        public decimal quantitt { get; set; }
        [JsonProperty(PropertyName = "Address")]
        public string address { get; set; }
        [JsonProperty(PropertyName = "Opened")]
        public DateTime opened { get; set; }
        [JsonProperty(PropertyName = "Authorized")]
        public bool authorized { get; set; }
        [JsonProperty(PropertyName = "PendingPayment")]
        public string pending { get; set; }
        [JsonProperty(PropertyName = "TxCost")]
        public decimal txCost { get; set; }
        [JsonProperty(PropertyName = "TxId")]
        public string txId { get; set; }
        [JsonProperty(PropertyName = "Canceled")]
        public bool canceled { get; set; }
        [JsonProperty(PropertyName = "InvalidAddress")]
        public bool invalidAddress { get; set; }
    }
}
