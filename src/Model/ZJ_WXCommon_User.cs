using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ZJ_WXCommon_User : ZJ_BaseModel
    {
        /// <summary>
        /// 微信唯一标识
        /// </summary>
        public string UnionID { get; set; }
        /// <summary>
        /// 公众号openId
        /// </summary>
        public string CommonOpenId { get; set; }
        /// <summary>
        /// 小程序openId
        /// </summary>
        public string WXAppOpenId { get; set; }
    }
}
