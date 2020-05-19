using System;
using System.Collections.Generic;
using System.Text;
using TravelMeaning.Models.DTO;

namespace TravelMeaning.Models.ResponseModels.User
{
    public class LogInModel
    {
        public string Token { get; set; }
        public UserInfoDTO UserInfo { get; set; }
    }
}
