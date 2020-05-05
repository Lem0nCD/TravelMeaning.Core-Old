using System;
using System.Collections.Generic;
using System.Text;

namespace TravelMeaning.Models.Model
{
    /// <summary>
    /// 收藏旅游指南
    /// </summary>
    public class Favorite : BaseEntity
    {
        /// <summary>
        /// 用户外键
        /// </summary>
        public Guid UserId { get; set; }
        public User User { get; set; }
        /// <summary>
        /// 旅游指南外键
        /// </summary>
        public Guid TravelGuideId { get; set; }
        public TravelGuide TravelGuide { get; set; }

    }
}
