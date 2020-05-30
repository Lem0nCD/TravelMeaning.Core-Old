using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelMeaning.IBLL;
using TravelMeaning.Models.DTO;
using TravelMeaning.Models.ResponseModels;
using TravelMeaning.Models.ResponseModels.Comment;
using TravelMeaning.Models.ViewModels.Comment;
using TravelMeaning.Web.Auth;

namespace TravelMeaning.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentManager _commentManger;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ICommentReviewManager _commentReviewManager;
        public CommentController(ICommentManager commentManger, IHttpContextAccessor httpContext, ICommentReviewManager commentReviewManager)
        {
            _commentManger = commentManger ?? throw new ArgumentNullException(nameof(commentManger));
            _commentReviewManager = commentReviewManager ?? throw new ArgumentNullException(nameof(commentReviewManager));
            _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
        }



        // GET: api/Comment
        [HttpGet("{id}")]
        public async Task<ResponseModel<GuideCommentsModel>> GetGuideComments([FromRoute]string id, int page, int size)
        {
            var responseModel = new ResponseModel<GuideCommentsModel>();
            page = page < 0 ? 0 : page - 1;
            size = size <= 0 ? 1 : size;
            if (Guid.TryParse(id, out Guid guideId))
            {
                responseModel.Code = StateCode.Sucess;
                responseModel.Data = new GuideCommentsModel
                {
                    Count = await _commentManger.GetGuideCommentsCount(guideId),
                    Comments = await _commentManger.GetGuideComments(guideId, page, size),
                };
            }
            return responseModel;
        }
        [Route("{id}")]
        [HttpGet]
        public async Task<ResponseModel<List<UserInfoCommentDTO>>> GetUserComments(string id)
        {
            var responseModel = new ResponseModel<List<UserInfoCommentDTO>>();
            if (Guid.TryParse(id, out Guid userId))
            {
                responseModel.Code = StateCode.Sucess;
                responseModel.Data = await _commentManger.GetUserComments(userId);
            }
            return responseModel;
        }

        // POST: api/Comment
        [HttpPost]
        [Authorize(policy:"UserV1")]
        public async Task<ResponseModel<GenericModel>> ReplyGuide(CreateCommentViewModel viewModel)
        {
            Guid userId = JWTHelper.SeriallzeUserId(_httpContext);
            var responseModel = new ResponseModel<GenericModel>();
            if (Guid.TryParse(viewModel.GuideId, out Guid guideId) && userId != Guid.Empty)
            {
                Guid commentId = await _commentManger.CreateComment(userId, guideId, viewModel.Content);
                await _commentReviewManager.CreateCommentReview(commentId);
                responseModel.Code = StateCode.Sucess;
                responseModel.Data = new GenericModel
                {
                    IsSucess = true
                };
            }
            return responseModel;
        }
        [HttpPost]
        [Authorize(policy:"UserV1")]
        public async Task<ResponseModel<GenericModel>> UpVoteToComment(string id)
        {
            var responseModel = new ResponseModel<GenericModel>();
            if (Guid.TryParse(id,out Guid commentId))
            {
                await _commentManger.AddUpVoteCount(commentId);
                responseModel.Data.IsSucess = true;
            }
            return responseModel;
        }


        // PUT: api/Comment/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
        }
    }
}
