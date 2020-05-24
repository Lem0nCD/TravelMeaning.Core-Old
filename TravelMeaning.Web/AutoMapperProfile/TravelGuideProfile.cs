using AutoMapper;
using TravelMeaning.Models.DTO;
using TravelMeaning.Models.Model;

namespace TravelMeaning.Web.AutoMapperProfile
{
    public class TravelGuideProfile : Profile
    {
        public TravelGuideProfile()
        {
            CreateMap<TravelGuide, TravelGuideDTO>();
        }
    }
}
