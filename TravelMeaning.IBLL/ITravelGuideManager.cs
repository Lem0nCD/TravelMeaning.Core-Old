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
        public Task<bool> UpdateGuideAsync(string userId, string content);
        public Task<bool> DeleteGuideAsync(string userId, string content);
        public Task<TravelGuideDTO> GetGuideByIdAsync(string guideId);
        public Task<List<TravelGuideDTO>> GetAllGuideAsync();
        public Task<List<TravelGuideDTO>> GetGuideByPageAsync(int page,int take,bool desc = true);
    }
}
