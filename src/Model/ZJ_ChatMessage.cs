using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ZJ_ChatMessage : ZJ_BaseModel
    {
        public string ChatId { get; set; }

        public string SendId { get; set; }
        public string ReciveId { get; set; }
        /// <summary>
        /// 0 用户创建提问 1 答主回复 2 答主拒绝 3 正常
        /// </summary>
        public int SendType { get; set; }
        /// <summary>
        /// text 文字  image 图片 audio 语音
        /// </summary>
        public string MsgType { get; set; }

        public string MsgContent { get; set; }
        public int AudioTimes { get; set; }
    }
}
