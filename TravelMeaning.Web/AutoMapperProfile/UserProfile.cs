using AutoMapper;
using TravelMeaning.Models.DTO;
using TravelMeaning.Models.Model;
using TravelMeaning.Models.ResponseModels.User;

namespace TravelMeaning.Web.AutoMapperProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserInfoDTO>();
            CreateMap<User, UserDetailInfoDTO>();
            CreateMap<User, LogInModel>();
        }
    }
}
