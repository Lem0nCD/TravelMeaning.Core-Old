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
    public class CommentReviewManager : ICommentReviewManager
    {
        protected readonly ICommentReviewService _commentReviewSvc;
        protected readonly ICommentService _commentSvc;
        protected readonly IMapper mapper;

        public CommentReviewManager(ICommentReviewService commentReviewSvc, ICommentService commentSvc, IMapper mapper)
        {
            _commentReviewSvc = commentReviewSvc ?? throw new ArgumentNullException(nameof(commentReviewSvc));
            _commentSvc = commentSvc ?? throw new ArgumentNullException(nameof(commentSvc));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> CreateCommentReview(Guid commentId)
        {
            var commentReview = new CommentReview
            {
                CommentId = commentId
            };
            return await _commentReviewSvc.CreateAsync(commentReview);
        }

        public async Task<List<CommentReviewDTO>> GetAllCommentReview(int page, int size)
        {
            var list = await _commentSvc.GetAll().Skip(page).Take(size).Include(x => x.User).Include(x => x.CommentReview).ToListAsync();
            return mapper.Map<List<CommentReviewDTO>>(list);
        }

        public async Task<List<CommentReviewDTO>> GetAllCommentReviewByState(int page, int size, ReviewState state)
        {
            var list = await _commentSvc.GetAll().Skip(page).Take(size).Include(x => x.User).Include(x => x.CommentReview).Where(x => x.CommentReview.State == state).ToListAsync();
            return mapper.Map<List<CommentReviewDTO>>(list);
        }

        public async Task<int> GetAllCommentReviewCount()
        {
            return await _commentReviewSvc.GetAll().CountAsync();
        }

        public async Task<bool> ModiflyNoteByCommentId(Guid commentId, string content)
        {
            var review = await _commentReviewSvc.GetAll().Where(x => x.CommentId == commentId).FirstOrDefaultAsync();
            review.Note = content;
            return await _commentReviewSvc.EditAsync(review);
        }

        public async Task<bool> ModiflyNoteById(Guid id, string content)
        {
            var review = await _commentReviewSvc.GetOneByIdAsync(id);
            review.Note = content;
            return await _commentReviewSvc.EditAsync(review);
        }

        public async Task<bool> ModifyReviewStateByCommentId(Guid commentId, ReviewState state)
        {
            var review = await _commentReviewSvc.GetAll().Where(x => x.CommentId == commentId).FirstOrDefaultAsync(); ;
            review.State = state;
            return await _commentReviewSvc.EditAsync(review); ;
        }

        public async Task<bool> ModifyReviewStateById(Guid id, ReviewState state)
        {
            var review = await _commentReviewSvc.GetOneByIdAsync(id);
            review.State = state;
            return await _commentReviewSvc.EditAsync(review); ;
        }
    }
}
