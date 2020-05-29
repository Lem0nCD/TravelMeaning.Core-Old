using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelMeaning.IBLL;
using TravelMeaning.Models.Model;
using TravelMeaning.Models.ResponseModels;
using TravelMeaning.Models.ResponseModels.CommentReview;
using TravelMeaning.Models.ViewModels.Review;

namespace TravelMeaning.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentReviewController : ControllerBase
    {
        private readonly ICommentReviewManager _commentReviewManager;

        public CommentReviewController(ICommentReviewManager commentReviewManager)
        {
            _commentReviewManager = commentReviewManager ?? throw new ArgumentNullException(nameof(commentReviewManager));
        }

        // GET: api/CommentReview
        [HttpGet]
        public async Task<ResponseModel<CommentReviewListModel>> Get(int page, int size, string state)
        {
            var responseModel = new ResponseModel<CommentReviewListModel>
            {
                Data = new CommentReviewListModel()
            };
            page = page <= 0 ? 0 : page - 1;
            size = size < 0 ? 0 : size;
            responseModel.Data.Comments = await _commentReviewManager.GetAllCommentReview(page, size);
            responseModel.Data.Count = await _commentReviewManager.GetAllCommentReviewCount();
            responseModel.Code = StateCode.Sucess;
            return responseModel;
        }

        // GET: api/CommentReview/5
        [HttpPut("[action]")]
        public async Task<ResponseModel<GenericModel>> ModifyState(ModifyStateViewModel viewModel)
        {
            var responseModel = new ResponseModel<GenericModel>
            {
                Data = new GenericModel()
            };
            if (Guid.TryParse(viewModel.Id, out Guid commentId) && Enum.TryParse<ReviewState>(viewModel.State.ToString(), out ReviewState reviewState))
            {
                responseModel.Data.IsSucess = await _commentReviewManager.ModifyReviewStateByCommentId(commentId, reviewState);
                responseModel.Code = StateCode.Sucess;
            }
            return responseModel;
        }

        [HttpPut("[action]")]
        public async Task<ResponseModel<GenericModel>> ModifyNote(ModifyNoteViewModel viewModel)
        {
            var responseModel = new ResponseModel<GenericModel>
            {
                Data = new GenericModel()
            };
            if (Guid.TryParse(viewModel.Id, out Guid commentId))
            {
                responseModel.Data.IsSucess = await _commentReviewManager.ModiflyNoteByCommentId(commentId, viewModel.Note);
                responseModel.Code = StateCode.Sucess;
            }
            return responseModel;
        }

        // PUT: api/CommentReview/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
