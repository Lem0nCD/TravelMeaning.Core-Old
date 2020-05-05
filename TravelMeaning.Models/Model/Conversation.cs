using System;
using System.Collections.Generic;
using System.Text;

namespace TravelMeaning.Models.Model
{
    public class Conversation : BaseEntity
    {
        /// <summary>
        /// 用户1外键
        /// </summary>
        public Guid User1Id { get; set; }
        public User User1 { get; set; }
        /// <summary>
        /// 用户1外键
        /// </summary>
        public Guid User2Id { get; set; }
        public User User2 { get; set; }
        /// <summary>
        /// 显示标记
        /// </summary>
        public bool IsShow { get; set; } = true;
        public virtual ICollection<ChatMessage> ChatMessages { get; set; }
    }
}
