using System;
using System.Collections.Generic;
using System.Text;

namespace TravelMeaning.Models.DTO
{
    public class TravelGuideDTO
    {
        /// <summary>
        /// 作者名
        /// </summary>
        public string UserName { get; set; }
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
        public int UpVoteCount { get; set; } = 0;
        /// <summary>
        /// 查看数
        /// </summary>
        public int ViewedCount { get; set; } = 0;
    }
}
