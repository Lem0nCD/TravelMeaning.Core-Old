using System;
using System.Collections.Generic;
using System.Text;

namespace TravelMeaning.Models.Model
{
    /// <summary>
    /// 好友关系
    /// </summary>
    public class RelationShip : BaseEntity
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
        /// 关系类型
        /// </summary>
        public RelationshipType Type { get; set; } = RelationshipType.Detachment;

    }
}
