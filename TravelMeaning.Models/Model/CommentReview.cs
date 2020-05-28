using System;
using System.Collections.Generic;
using System.Text;

namespace TravelMeaning.Models.Model
{
    /// <summary>
    /// 评论审核
    /// </summary>
    public class CommentReview : BaseEntity
    {
        /// <summary>
        /// 评论外键
        /// </summary>
        public Guid CommentId { get; set; }
        public Comment Comment { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public ReviewState State { get; set; } = ReviewState.Reviewing;
        /// <summary>
        /// 审核信息
        /// </summary>
        public string Note { get; set; }
    }
}
