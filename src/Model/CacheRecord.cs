using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class CacheRecord
    {
        public int Id { get; set; }

        [MaxLength(64)]
        public string KeyName { get; set; }

       
        public int Type { get; set; }

        /// <summary>
        /// 状态1:未删除/未处理;  2 以删除/以处理
        /// </summary>
        public int Status { get; set; }

        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime CompleteTime { get; set; }

    }
}
