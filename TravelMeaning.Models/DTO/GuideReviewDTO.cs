using System;
using System.Collections.Generic;
using System.Text;
using TravelMeaning.Models.Model;

namespace TravelMeaning.Models.DTO
{
    public class GuideReviewDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid GuideId { get; set; }
        /// <summary>
        /// 作者外键
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 作者名
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 正文 markdown
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
        /// <summary>
        /// 审核状态
        /// </summary>
        public ReviewState State { get; set; }
        /// <summary>
        /// 审核信息
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
