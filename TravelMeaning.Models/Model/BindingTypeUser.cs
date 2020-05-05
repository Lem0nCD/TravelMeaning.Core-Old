using System;
using System.Collections.Generic;
using System.Text;

namespace TravelMeaning.Models.Model
{
    /// <summary>
    /// 绑定类型-用户
    /// </summary>
    public class BindingTypeUser : BaseEntity
    {
        /// <summary>
        /// 用户外键
        /// </summary>
        public Guid UserId { get; set; }
        public User User { get; set; }
        /// <summary>
        /// 绑定类型
        /// </summary>
        public BindingType Type { get; set; }
    }
}
