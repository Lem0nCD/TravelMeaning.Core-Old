using AutoMapper;
using TravelMeaning.Models.DTO;
using TravelMeaning.Models.Model;

namespace TravelMeaning.Web.AutoMapperProfile
{
    public class TravelGuideRevieweProfile : Profile
    {
        public TravelGuideRevieweProfile()
        {
            CreateMap<TravelGuide, GuideReviewDTO>()
                .ForMember(dest => dest.GuideId,opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId,opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Username,opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.State,opt => opt.MapFrom(src => src.TravelGuideReview.State))
                .ForMember(dest => dest.Note,opt => opt.MapFrom(src => src.TravelGuideReview.Note));
            CreateMap<TravelGuideReview, GuideReviewDTO>();
        }
    }
}
