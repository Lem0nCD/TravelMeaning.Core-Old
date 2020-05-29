using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelMeaning.Models.DTO;
using TravelMeaning.Models.Model;

namespace TravelMeaning.IBLL
{
    public interface ICommentReviewManager
    {
        public Task<bool> CreateCommentReview(Guid commentId);
        public Task<bool> ModifyReviewStateById(Guid id, ReviewState state);
        public Task<bool> ModifyReviewStateByCommentId(Guid commentId, ReviewState state);
        public Task<int> GetAllCommentReviewCount();
        public Task<List<CommentReviewDTO>> GetAllCommentReview(int page, int size);
        public Task<List<CommentReviewDTO>> GetAllCommentReviewByState(int page, int size, ReviewState state);
        public Task<bool> ModiflyNoteByCommentId(Guid commentId, string content);
        public Task<bool> ModiflyNoteById(Guid id, string content);
    }
}
