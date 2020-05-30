using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        protected readonly IGuideRouteService _guideRouteSvc;

        protected readonly IUserService _userSvc;
        protected readonly IMapper _mapper;

        public TravelGuideManager(ITravelGuideService travelGuideSvc, IMapper mapper, IUserService userSvc, IGuideRouteService guideRouteSvc)
        {
            _travelGuideSvc = travelGuideSvc ?? throw new ArgumentNullException(nameof(travelGuideSvc));
            _guideRouteSvc = guideRouteSvc ?? throw new ArgumentNullException(nameof(guideRouteSvc));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userSvc = userSvc ?? throw new ArgumentNullException(nameof(userSvc));
        }

        public async Task AddUpVoteCount(Guid id)
        {
            var guide = _travelGuideSvc.GetAll().Where(x => x.Id == id).FirstOrDefault();
            guide.UpVoteCount++;
            await _travelGuideSvc.EditAsync(guide);
        }

        public async Task AddViewedCount(Guid id)
        {
            var guide = _travelGuideSvc.GetAll().Where(x => x.Id == id).FirstOrDefault();
            guide.ViewedCount++;
            await _travelGuideSvc.EditAsync(guide);
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

        public Task<bool> DeleteGuideAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> FavoriteGuide(Guid Id, Guid UserId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TravelGuideDTO>> GetAllGuideAsync()
        {
            var list = await _travelGuideSvc.GetAll().OrderBy(x => x.CreateTime).Take(30).ToListAsync();
            return _mapper.Map<List<TravelGuideDTO>>(list);
        }

        public async Task<TravelGuideDTO> GetGuideByIdAsync(Guid id)
        {
            var guide = await _travelGuideSvc.GetAll().Where(x => x.Id == id).Include(x => x.User).FirstAsync();
            var guideDTO = _mapper.Map<TravelGuideDTO>(guide);
            guideDTO.UserId = guide.User.Id;
            guideDTO.Username = guide.User.Username;
            guideDTO.Avatar = guide.User.Avatar;
            return guideDTO;
        }

        public Task<List<TravelGuideDTO>> GetGuideByPageAsync(int page, int take, bool desc = true)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TravelGuideDTO>> GetGuideByUserIdAsync(Guid userId)
        {
            var list = await _travelGuideSvc.GetAll().OrderBy(x => x.CreateTime).Where(x => x.UserId == userId).Take(30).ToListAsync();
            return _mapper.Map<List<TravelGuideDTO>>(list);
        }

        public Task<bool> UpdateGuideAsync(Guid userId, string content)
        {
            throw new NotImplementedException();
        }
    }
}
