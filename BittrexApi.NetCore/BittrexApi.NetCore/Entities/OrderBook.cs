using System;
using System.Collections.Generic;
using System.Text;

namespace BittrexApi.NetCore.Entities
{
    public class OrderBook
    {
        public OrderInterval[] buy { get; set; }
        public OrderInterval[] sell { get; set; }
    }
}
