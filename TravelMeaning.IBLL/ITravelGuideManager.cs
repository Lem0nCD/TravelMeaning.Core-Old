using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelMeaning.Models.DTO;

namespace TravelMeaning.IBLL
{
    public interface ITravelGuideManager
    {
        public Task<Guid> CreateGuideAsync(Guid userId, string content, string title, string imgurl);
        public Task<bool> UpdateGuideAsync(Guid userId, string content);
        public Task<bool> DeleteGuideAsync(Guid userId);
        public Task<TravelGuideDTO> GetGuideByIdAsync(Guid id);
        public Task<List<TravelGuideDTO>> GetGuideByUserIdAsync(Guid userId);
        public Task<List<TravelGuideDTO>> GetAllGuideAsync();
        public Task<List<TravelGuideDTO>> GetGuideByPageAsync(int page,int take,bool desc = true);
        public Task<Guid> FavoriteGuide(Guid Id,Guid UserId);
        public Task AddViewedCount(Guid id);
        public Task AddUpVoteCount(Guid id,Guid userId);

    }
}
