using System;
using System.Collections.Generic;
using System.Text;

namespace BittrexApi.NetCore.Entities
{
    public class Response<T>
    {
        public bool success { get; set; }
        public string message { get; set; }
        public T result { get; set; }
    }
}
