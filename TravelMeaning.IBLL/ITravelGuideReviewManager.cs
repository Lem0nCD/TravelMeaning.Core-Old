using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelMeaning.Models.DTO;
using TravelMeaning.Models.Model;

namespace TravelMeaning.IBLL
{
    public interface ITravelGuideReviewManager
    {
        public Task<bool> CreateGuideReview(Guid guideId);
        public Task<bool> ModifyReviewStateById(Guid id,ReviewState state, string note = "");
        public Task<bool> ModifyReviewStateByGuideId(Guid guideId,ReviewState state, string note = "");
        public Task<List<GuideReviewDTO>> GetAllGuideReview(int page, int size);
        public Task<List<GuideReviewDTO>> GetAllGuideReviewByState(int page, int size,ReviewState state);
        public Task<bool> ModiflyNoteByGuideId(Guid guideId, string content);
        public Task<bool> ModiflyNoteById(Guid id, string content);
    }
}
