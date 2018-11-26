using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ZJ_PayOrder : ZJ_BaseModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 聊天Id
        /// </summary>
        public string ChatId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 流水号
        /// </summary>
        public string SerialNo { get; set; }
        public int TransType { get; set; }
        /// <summary>
        /// 支付方式（1-支付宝，2-微信）
        /// </summary>
        public int PayType { get; set; }
        /// <summary>
        /// 计费方式
        /// </summary>
        public int FeeType { get; set; }
        public decimal Money { get; set; }
        /// <summary>
        /// 状态（0-失败，1-成功 2 取消  3 失败）
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndTime { get; set; }
        public string PrivateInfo { get; set; }

        public string Remarks { get; set; }
        public string Qudao { get; set; }

        public int Version { get; set; }

    }
}
