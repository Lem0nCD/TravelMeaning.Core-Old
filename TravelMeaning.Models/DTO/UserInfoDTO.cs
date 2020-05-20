using System;
using System.Collections.Generic;
using System.Text;
using TravelMeaning.Models.Model;

namespace TravelMeaning.Models.DTO
{
    public class UserInfoDTO
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 职业
        /// </summary>
        public string Occupation { get; set; }
        /// <summary>
        /// 用户号(非ID)
        /// </summary>
        public int UId { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 用户角色字符串（多种）
        /// </summary>
        public string RolesStr { get; set; }
        /// <summary>
        /// 性别 female 1,male 2
        /// </summary>
        public Gender Gender { get; set; }
    }
}
