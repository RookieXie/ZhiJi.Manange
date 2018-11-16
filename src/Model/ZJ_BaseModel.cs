using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ZJ_BaseModel
    {
        /// <summary>
        /// mysql 自增长id
        /// </summary>
        public int Id { get; set; }
        public string FId { get; set; }
        public DateTime CreateTime { get; set; }
        public int IsDelete { get; set; }
    }
}
