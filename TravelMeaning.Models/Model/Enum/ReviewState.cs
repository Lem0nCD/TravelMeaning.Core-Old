using System;
using System.Collections.Generic;
using System.Text;

namespace TravelMeaning.Models.Model
{
    /// <summary>
    /// 审核状态
    /// </summary>
    public enum ReviewState
    {
        /// <summary>
        /// 审核中
        /// </summary>
        Reviewing = 0,
        /// <summary>
        /// 未通过
        /// </summary>
        NotApproved,
        /// <summary>
        /// 已通过
        /// </summary>
        Published
    }
}
