using System;
using System.Collections.Generic;
using System.Text;

namespace TravelMeaning.Models.Model
{
    /// <summary>
    /// 旅游指南
    /// </summary>
    public class TravelGuide : BaseEntity
    {
        /// <summary>
        /// 作者外键
        /// </summary>
        public Guid UserId { get; set; }
        public User User { get; set; }
        /// <summary>
        /// 正文 富文本
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 封面图片
        /// </summary>
        public string CoverImage { get; set; }
        /// <summary>
        /// 点赞数
        /// </summary>
        public int UpVoteCount { get; set; } = 0;
        /// <summary>
        /// 查看数
        /// </summary>
        public int ViewedCount { get; set; } = 0;
        public ICollection<Comment> Comments { get; set; }
        public TravelGuideReview TravelGuideReview { get; set; }
    }
}
