using System;
using System.Collections.Generic;
using System.Text;

namespace Model.HomeListCach
{
    public class HomeUsers
    {
        public string UserId { get; set; }
        public string NickName { get; set; }
        public string HeadImg { get; set; }
        /// <summary>
        /// 性别（0-未知 1-男，2-女）
        /// </summary>
        public int Sex { get; set; }
        public int Age { get; set; }
        /// <summary>
        /// 专业
        /// </summary>
        public string Professional { get; set; }
        /// <summary>
        /// 个人介绍
        /// </summary>
        public string Introduction { get; set; }
        /// <summary>
        /// 用户类型 0 用户 1 答主
        /// </summary>
        public int UserType { get; set; }
        /// <summary>
        /// 问答价格
        /// </summary>
        public decimal AnswerPrice { get; set; }
        /// <summary>
        /// 问答时长
        /// </summary>
        public decimal AnswerTime { get; set; }
        /// <summary>
        /// 账户余额
        /// </summary>
        public decimal TotalMoney { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public int Sign { get; set; }
        /// <summary>
        /// 微信唯一标识
        /// </summary>
        public string UnionID { get; set; }
        /// <summary>
        /// 小程序标识
        /// </summary>
        public string OpenID { get; set; }
        /// <summary>
        /// 绑定来源（1-微信）
        /// </summary>
        public int BoundSource { get; set; }
        /// <summary>
        /// 知几号
        /// </summary>
        public string Numbers { get; set; }

        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 排序权重
        /// </summary>
        public int Order { get; set; }
        public HomeUsers()
        {

        }
        public HomeUsers(ZJ_User zJ_User)
        {
            OpenID = zJ_User.OpenID;
            UnionID = zJ_User.UnionID;
            BoundSource = zJ_User.BoundSource;
            NickName = zJ_User.NickName;
            CreateTime = zJ_User.CreateTime;
            HeadImg = zJ_User.HeadImg;
            UserType = zJ_User.UserType;
            Sex = zJ_User.Sex;
            UserId = zJ_User.FId;
            Numbers = zJ_User.Numbers;
            AnswerTime = zJ_User.AnswerTime;
            AnswerPrice = zJ_User.AnswerPrice;
            Sign = zJ_User.Sign;
            Introduction= zJ_User.Introduction;
            Age = zJ_User.Age;
            Professional = zJ_User.Professional;
        }
    }
}
