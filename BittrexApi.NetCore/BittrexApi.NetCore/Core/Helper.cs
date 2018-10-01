using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BittrexApi.NetCore.Core
{
    public class Helper
    {
        /// <summary>
        /// Convert a Dictionary to concatinated querystring
        /// </summary>
        /// <param name="parameters">Dictionary to convert</param>
        /// <returns>Concatinated querystring</returns>
        public string StringifyDictionary(Dictionary<string, object> parameters)
        {
            var qsValues = string.Empty;

            if (parameters != null)
            {
                qsValues = string.Join("&", parameters.Select(p => p.Key + "=" + p.Value));
            }

            return qsValues;
        }
    }
}
