using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Utility
{
    public class CookieConfig
    {
        public static readonly string oaUserId = "oauserid";
        public static readonly string oaToken = "oatoken";
        public static readonly string domainTopLevel = "zhijiwen.com";
       

        public Dictionary<string, string> DomainDic { get; set; }
        public CookieConfig(string host)
        {
            //if (EnvConfig.IsDevelopment)
            //{
            //    if (host=="localhost")
            //    {

            //    }
            //    domainTopLevel =  + host;
            //}
            //else
            //{

            //}
        }
    }
}
