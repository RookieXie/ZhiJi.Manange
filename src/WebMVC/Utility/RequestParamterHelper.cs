using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebCore.Utility
{
    public class RequestParamterHelper
    {
       
        
        public Dictionary<string, List<string>> ParamDictionary { get; set; }

        public RequestParamterHelper()
        {
        }
        public RequestParamterHelper(string paramFormStr)
        {
            ParamDictionary = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
            GetParamDictionary(paramFormStr);
        }

        public bool GetParamDictionary(string paramFormStr)
        {
            if (string.IsNullOrEmpty(paramFormStr))
            {
                return false;
            }
            var paramList = paramFormStr.Split("&");
            foreach (var param in paramList)
            {
                var strList = param.Split("=");
                string key = strList[0];
                //url解码，form表单提交数据会被url编码
                string value = HttpUtility.UrlDecode(strList[1]);
                if (string.IsNullOrEmpty(value)|| string.IsNullOrEmpty(key))
                {
                    continue;
                }

                if (!ParamDictionary.ContainsKey(key))
                {
                    List<string> valueList = new List<string>();
                    valueList.Add(value);
                    ParamDictionary.Add(key, valueList);
                }
                else
                {
                    ParamDictionary[key].Add(value);
                }
            }
            return true;
        }

        /// <summary>
        /// 不区分大小写
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> GetParamValue(string key)
        {
            return ParamDictionary.GetValueOrDefault(key);
        }
     
    }

    
}
