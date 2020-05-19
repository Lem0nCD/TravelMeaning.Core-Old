using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelMeaning.Web.Auth
{
    /// <summary>
    /// 令牌自定义信息
    /// </summary>
    public class CustomPayloadModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 角色集合，逗号分隔
        /// </summary>
        public string Roles { get; set; }
        /// <summary>
        /// 职能
        /// </summary>
        public string Work { get; set; }
    }
}
