using System;
using System.Collections.Generic;
using System.Text;

namespace TravelMeaning.Models.Model
{
    /// <summary>
    /// 消息
    /// </summary>
    public class Message : BaseEntity
    {
        /// <summary>
        /// 接收消息用户外键
        /// </summary>
        public Guid ToUserId { get; set; }
        public User ToUser { get; set; }
        /// <summary>
        /// 发送消息用户外键
        /// </summary>
        public Guid FromUserId { get; set; }
        public User FromUser { get; set; }
        /// <summary>
        /// 消息正文
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 正文类型
        /// </summary>
        public ContentType Type { get; set; }
        /// <summary>
        /// 查看标记
        /// </summary>
        public bool IsRead { get; set; } = false;
        /// <summary>
        /// 显示标记
        /// </summary>
        public bool IsShow { get; set; } = true;
    }
}
