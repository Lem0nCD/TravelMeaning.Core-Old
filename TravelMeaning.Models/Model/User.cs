using System;
using System.Collections.Generic;
using System.Text;

namespace TravelMeaning.Models.Model
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; } = "Default";
        /// <summary>
        /// 地址
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 职业
        /// </summary>
        public string Occupation { get; set; }
        /// <summary>
        /// 用户号(非ID)
        /// </summary>
        public int UId { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 性别 female 1,male 2
        /// </summary>
        public Gender Gender { get; set; } = Gender.None;

        public ChatMessage ChatMessage { get; set; }
        public UserRole UserRole { get; set; }
        public ICollection<BindingTypeUser> Bindings { get; set; }
        public ICollection<Conversation> ToConversations { get; set; }
        public ICollection<Conversation> FromConversations { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<TravelGuide> TravelGuides { get; set; }
        public ICollection<RelationShip> ToRelationShips { get; set; }
        public ICollection<RelationShip> FromRelationShips { get; set; }
        public ICollection<Message> ToUserMessages { get; set; }
        public ICollection<Message> FromUserMessages { get; set; }

    }
}
