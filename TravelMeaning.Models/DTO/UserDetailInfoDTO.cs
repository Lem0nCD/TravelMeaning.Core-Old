using TravelMeaning.Models.Model;

namespace TravelMeaning.Models.DTO
{
    public class UserDetailInfoDTO
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
        /// <summary>
        /// 收到的赞
        /// </summary>
        public int GuidesUpVoteCount { get; set; }
        /// <summary>
        /// 粉丝数
        /// </summary>
        public int FansCount { get; set; }
        /// <summary>
        /// 文章总数
        /// </summary>
        public int GuideCount { get; set; }
        /// <summary>
        /// 评论总数
        /// </summary>
        public int CommentCount { get; set; }
        /// <summary>
        /// 关系
        /// </summary>
        public RelationshipType Type { get; set; }
    }
}

