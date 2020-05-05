using System;
using System.Collections.Generic;
using System.Text;

namespace TravelMeaning.Models.Model
{
    /// <summary>
    /// 旅游指南审核
    /// </summary>
    public class TravelGuideReview : BaseEntity
    {
        /// <summary>
        /// 旅游指南外键
        /// </summary>
        public Guid TravelGuideId { get; set; }
        public TravelGuide TravelGuide { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public ReviewState State { get; set; } = ReviewState.InReview;
        /// <summary>
        /// 审核信息
        /// </summary>
        public string Note { get; set; }
    }
}
