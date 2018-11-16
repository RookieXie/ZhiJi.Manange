using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Models
{
    public class UserInfo
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 账户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        //public int Gender { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 性别 1男  2女
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        public string PwdFlag { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 状态 默认0    未启用:0，启用1,
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 状态时间
        /// </summary>
        public DateTime StatusTime { get; set; } = DateTime.Now;

        public List<AdminRole> Roles { get; set; }

        public AdminRole CurrentRole { get; set; }
    }
}
