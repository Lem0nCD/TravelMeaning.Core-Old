using System;
using System.Collections.Generic;
using System.Text;
using TravelMeaning.Models.DTO;

namespace TravelMeaning.Models.ResponseModels.Comment
{
    public class GuideCommentsModel
    {
        /// <summary>
        /// 评论总数
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 评论列表
        /// </summary>
        public List<GuideCommentDTO> Comments { get; set; }
    }
}
