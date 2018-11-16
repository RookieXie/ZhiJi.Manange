using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ZJ_Chat : ZJ_BaseModel
    {
        /// <summary>
        /// 状态 0 待回答 1 沟通中 2 已结束 3 拒绝 
        /// </summary>
        public int Status { get; set; }

        public string QuestionId { get; set; }
        public string AnswerId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
