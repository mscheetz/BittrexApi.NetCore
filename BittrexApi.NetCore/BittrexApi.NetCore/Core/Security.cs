using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BittrexApi.NetCore.Core
{
    public class Security
    {
        /// <summary>
        /// Get HMAC Signature
        /// </summary>
        /// <param name="message">Message to sign</param>
        /// <param name="apiSecret">Api key secret</param>
        /// <returns>string of signed message</returns>
        public string GetHMACSignature(string message, string apiSecret)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] messageBytes = encoding.GetBytes(message);
            byte[] keyBytes = encoding.GetBytes(apiSecret);
            HMACSHA512 crypotgrapher = new HMACSHA512(keyBytes);

            byte[] bytes = crypotgrapher.ComputeHash(messageBytes);

            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
