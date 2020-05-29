using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelMeaning.Models.DTO;
using TravelMeaning.Models.Model;

namespace TravelMeaning.Web.AutoMapperProfile
{
    public class CommentReviewPorfile : Profile
    {
        public CommentReviewPorfile()
        {
            CreateMap<Comment, CommentReviewDTO>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.CommentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.CommentReview.State))
                .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.CommentReview.Note));
        }
    }
}
