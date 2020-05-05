using System;
using System.Collections.Generic;
using System.Text;

namespace TravelMeaning.Models.Model
{
    /// <summary>
    /// 评论
    /// </summary>
    public class Comment : BaseEntity
    {
        /// <summary>
        /// 发表人外键
        /// </summary>
        public Guid UserId { get; set; }
        public User User { get; set; }
        /// <summary>
        /// 旅游攻略外键
        /// </summary>
        public Guid TravelGuideId { get; set; }
        public TravelGuide TravelGuide { get; set; }
        /// <summary>
        /// 评论正文
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 点赞数
        /// </summary>
        public int UpVoteCount { get; set; }
        /// <summary>
        /// 回复评论Id
        /// </summary>
        public Guid? ReplyCommentId { get; set; }

        public CommentReview CommentReview { get; set; }
    }
}
