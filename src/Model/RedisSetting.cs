using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class RedisSetting
    {
        /// <summary>
        /// redis服务器地址
        /// </summary>
        public string RedisExchangeHosts { get; set; }

        /// <summary>
        /// redis服务器密码
        /// </summary>
        public string RedisExchangePwd { get; set; }
    }
}
