using System;
using System.Collections.Generic;
using System.Text;

namespace TravelMeaning.Models.DTO
{
    public class TravelGuideDTO
    {
        /// <summary>
        /// 文章Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 正文 markdown
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
        public int UpVoteCount { get; set; }
        /// <summary>
        /// 查看数
        /// </summary>
        public int ViewedCount { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
    }
}
