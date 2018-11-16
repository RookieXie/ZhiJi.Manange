using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class Base64Helper
    {
        /// <summary>
        /// base64编码
        /// </summary>
        /// <returns></returns>
        public string EnCode(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(bytes);
            
        }

        public string DeCode(string str)
        {
            byte[] bytes = Convert.FromBase64String(str);
           return Encoding.UTF8.GetString(bytes);
        }
    }
}
