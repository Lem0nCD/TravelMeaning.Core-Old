using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelMeaning.IBLL;
using TravelMeaning.Models.DTO;
using TravelMeaning.Models.Model;
using TravelMeaning.Models.ResponseModels;
using TravelMeaning.Models.ViewModels.GuideReview;

namespace TravelMeaning.Web.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize(policy: "Review")]
    //[Authorize(policy: "Editor")]
    [ApiController]
    public class GuideReviewController : ControllerBase
    {
        private readonly ITravelGuideReviewManager _reviewManager;

        public GuideReviewController(ITravelGuideReviewManager reviewManager)
        {
            _reviewManager = reviewManager ?? throw new ArgumentNullException(nameof(reviewManager));
        }

        [HttpGet]
        public async Task<ResponseModel<List<GuideReviewDTO>>> Get(int page, int size, int state)
        {
            var responseModel = new ResponseModel<List<GuideReviewDTO>>();
            page = page <= 0 ? 0 : page - 1;
            size = size < 0 ? 5 : size;
            responseModel.Data = await _reviewManager.GetAllGuideReview(page,size);
            responseModel.Code = StateCode.Sucess;
            return responseModel;
        }

        // PUT: api/Review/5
        [HttpPut("{id}")]
        public async Task<ResponseModel<GenericModel>> Put(string id,string state)
        {
            var responseModel = new ResponseModel<GenericModel>
            {
                Data = new GenericModel()
            };
            if (Guid.TryParse(id, out Guid guideId) && Enum.TryParse<ReviewState>(state,out ReviewState reviewState))
            {
                 responseModel.Data.IsSucess = await _reviewManager.ModifyReviewStateByGuideId(guideId, reviewState);
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
            if (Guid.TryParse(viewModel.Id, out Guid guideId))
            {
                responseModel.Data.IsSucess = await _reviewManager.ModiflyNoteByGuideId(guideId, viewModel.Note);
                responseModel.Code = StateCode.Sucess;
            }
            return responseModel;

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
