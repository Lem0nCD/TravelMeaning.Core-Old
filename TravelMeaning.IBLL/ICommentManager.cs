using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelMeaning.Models.DTO;

namespace TravelMeaning.IBLL
{
    public interface ICommentManager
    {
        Task<Guid> CreateComment(Guid userId, Guid guideId,string content);
        Task<bool> DeleteComment(Guid id);
        Task<List<UserInfoCommentDTO>> GetUserComments(Guid userId);
        Task<List<GuideCommentDTO>> GetGuideComments(Guid guideId,int page,int size);
        Task<int> GetGuideCommentsCount(Guid guideId);
        Task AddUpVoteCount(Guid commentId);

    }
}
