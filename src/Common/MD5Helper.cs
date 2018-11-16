using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public class MD5Helper
    {
        /// <summary>
        /// MD5加密 32位大写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5Crypto32(string str)
        {
            MD5CryptoServiceProvider mD5Crypto = new MD5CryptoServiceProvider();
            var bytes = Encoding.UTF8.GetBytes(str);
            var resBytes = mD5Crypto.ComputeHash(bytes);
            var resultStr = BitConverter.ToString(resBytes).Replace("-","");
            return resultStr;
        }

        /// <summary>
        /// MD5加密 16位 截取前16位
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5Crypto16(string str)
        {
            MD5CryptoServiceProvider mD5Crypto = new MD5CryptoServiceProvider();
            var bytes = Encoding.UTF8.GetBytes(str);
            var resBytes = mD5Crypto.ComputeHash(bytes);
            var resultStr = BitConverter.ToString(resBytes,0,8).Replace("-", "");
            return resultStr;
        }
    }
}
