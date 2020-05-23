using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelMeaning.IBLL;
using TravelMeaning.IDAL;
using TravelMeaning.Models.DTO;
using TravelMeaning.Models.Model;

namespace TravelMeaning.BLL
{
    public class TravelGuideManager : ITravelGuideManager
    {
        protected readonly ITravelGuideService _travelGuideSvc;
        protected readonly IMapper _mapper;

        public TravelGuideManager(ITravelGuideService travelGuideSvc, IMapper mapper)
        {
            _travelGuideSvc = travelGuideSvc ?? throw new ArgumentNullException(nameof(travelGuideSvc));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Guid> CreateGuideAsync(Guid userId, string content, string title, string imgurl)
        {
            var guide = new TravelGuide
            {
                Content = content,
                Title = title,
                CoverImage = imgurl,
                UserId = userId
            };
            if (await _travelGuideSvc.CreateAsync(guide))
            {
                return guide.Id;
            }
            return Guid.Empty;
        }

        public Task<bool> DeleteGuideAsync(string userId, string content)
        {
            throw new NotImplementedException();
        }

        public Task<List<TravelGuideDTO>> GetAllGuideAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TravelGuideDTO> GetGuideByIdAsync(string guideId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TravelGuideDTO>> GetGuideByPageAsync(int page, int take, bool desc = true)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateGuideAsync(string userId, string content)
        {
            throw new NotImplementedException();
        }
    }
}
