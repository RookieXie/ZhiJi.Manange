using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ZJ_Balance : ZJ_BaseModel
    {
        public string UserId { get; set; }
        public string AdverseId { get; set; }
        public decimal ChangeMoney { get; set; }
        public decimal BeforeMoney { get; set; }
        public decimal AfterMoney { get; set; }
    }
}
