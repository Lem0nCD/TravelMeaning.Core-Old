using System;
using System.Collections.Generic;
using System.Text;

namespace TravelMeaning.Models.Model
{
    public class ChatMessage : BaseEntity
    {
        /// <summary>
        /// 会话外键
        /// </summary>
        public Guid ConversationId { get; set; }
        public Conversation Conversation { get; set; }
        /// <summary>
        /// 发送消息用户外键
        /// </summary>
        public Guid UserId { get; set; }
        public User User { get; set; }
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
    }
}
