using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ZJ_Balance : ZJ_BaseModel
    {
        public string UserId { get; set; }
        public string AdverseId { get; set; }
        /// <summary>
        /// 0 答疑收款 1 提现
        /// </summary>
        public int ChangeType { get; set; }
        public decimal ChangeMoney { get; set; }
        public decimal BeforeMoney { get; set; }
        public decimal AfterMoney { get; set; }
    }
}
