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
    public class CommentManager : ICommentManager
    {
        protected readonly IMapper _mapper;
        protected readonly ICommentService _commentSvc;

        public CommentManager(IMapper mapper, ICommentService commentSvc)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _commentSvc = commentSvc ?? throw new ArgumentNullException(nameof(commentSvc));
        }

        public async Task AddUpVoteCount(Guid commentId)
        {
            var comment = await _commentSvc.GetOneByIdAsync(commentId);
            comment.UpVoteCount++;
            await _commentSvc.EditAsync(comment);
        }

        public async Task<Guid> CreateComment(Guid userId, Guid guideId, string content)
        {
            var comment = new Comment
            {
                Content = content,
                TravelGuideId = guideId,
                UserId = userId,
            };
            if (await _commentSvc.CreateAsync(comment))
            {
                return comment.Id;
            }
            throw new Exception(nameof(CreateComment) + " Fail!");
        }

        public async Task<bool> DeleteComment(Guid id)
        {
            return await _commentSvc.RemoveAsync(id);
        }

        public async Task<List<GuideCommentDTO>> GetGuideComments(Guid guideId, int page, int size)
        {
            var guideCommentList = await _commentSvc.GetAll().Where(x => x.TravelGuideId == guideId)
                .Skip(page * size).Take(size)
                .Include(x => x.User).ToListAsync();
            return _mapper.Map<List<GuideCommentDTO>>(guideCommentList);
        }

        public async Task<int> GetGuideCommentsCount(Guid guideId)
        {
            return await _commentSvc.GetAll().Where(x => x.TravelGuideId == guideId).CountAsync();
        }

        public async Task<List<UserInfoCommentDTO>> GetUserComments(Guid userId)
        {
            var userCommentList = await _commentSvc.GetAll().Where(x => x.UserId == userId).Include(x => x.TravelGuide).ToListAsync();
            return _mapper.Map<List<UserInfoCommentDTO>>(userCommentList);
        }

    }
}
