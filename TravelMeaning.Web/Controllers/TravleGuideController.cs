using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelMeaning.IBLL;
using TravelMeaning.Models.DTO;
using TravelMeaning.Models.ResponseModels;
using TravelMeaning.Models.ResponseModels.TravelGuide;
using TravelMeaning.Models.ViewModels.TravelGude;
using TravelMeaning.Web.Auth;

namespace TravelMeaning.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]

    public class TravleGuideController : ControllerBase
    {
        private readonly ITravelGuideManager _guideManager;
        private readonly ITravelGuideReviewManager _reviewManager;
        private readonly IHttpContextAccessor _httpContext;

        public TravleGuideController(ITravelGuideManager guideManager, IHttpContextAccessor httpContext, ITravelGuideReviewManager reviewManager)
        {
            _guideManager = guideManager ?? throw new ArgumentNullException(nameof(guideManager));
            _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
            _reviewManager = reviewManager ?? throw new ArgumentNullException(nameof(reviewManager));
        }

        // GET: api/TravleGuide
        [HttpGet]
        public async Task<ResponseModel<List<TravelGuideDTO>>> Get()
        {
            return new ResponseModel<List<TravelGuideDTO>>
            {
                Code = StateCode.Sucess,
                Data = await _guideManager.GetAllGuideAsync(),
            };
        }

        // GET: api/TravleGuide/5
        [HttpGet("{id}", Name = "GetTravelGuide")]
        public async Task<ResponseModel<TravelGuideDTO>> Get(string id)
        {
            var responseModel = new ResponseModel<TravelGuideDTO>();
            if (Guid.TryParse(id, out Guid guideId))
            {
                await _guideManager.AddViewedCount(guideId);
                var guide = await _guideManager.GetGuideByIdAsync(guideId);
                if (guide != null)
                {
                    responseModel.Code = StateCode.Sucess;
                    responseModel.Data = guide;
                }
            }
            return responseModel;
        }

        // POST: api/TravleGuide
        [HttpPost]
        [Consumes("application/json")]
        [Authorize(Policy = "UserV1")]
        public async Task<ResponseModel<CreatedGuideModel>> Post(CreateGuideViewModel viewModel)
        {
            var responseModel = new ResponseModel<CreatedGuideModel>();
            var userId = JWTHelper.SeriallzeJwt(((string)_httpContext.HttpContext.Request.Headers["Authorization"]).Replace("Bearer ", string.Empty)).Id;
            var guiideId = await _guideManager.CreateGuideAsync(userId, viewModel.Content, viewModel.Title, viewModel.CoverImageUrl);
            if (guiideId != Guid.Empty)
            {
                var reviewResult = await _reviewManager.CreateGuideReview(guiideId);
                responseModel.Data = new CreatedGuideModel
                {
                    Id = guiideId,
                };
                responseModel.Code = StateCode.Sucess;
            }
            return responseModel;
        }

        // PUT: api/TravleGuide/5
        [HttpPut("{id}")]
        public void Put(int id, string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost(nameof(UpVoteToGuide))]
        public async Task<ResponseModel<GenericModel>> UpVoteToGuide(string id)
        {
            var responseModel = new ResponseModel<GenericModel>();
            if (Guid.TryParse(id, out Guid guideId))
            {
                await _guideManager.AddUpVoteCount(guideId);
                responseModel.Data.IsSucess = true;
            }
            return responseModel;
        }
        [HttpGet(nameof(GetGuideByUserId))]
        public async Task<ResponseModel<List<TravelGuideDTO>>> GetGuideByUserId(string id)
        {
            var responseModel = new ResponseModel<List<TravelGuideDTO>>();
            if (Guid.TryParse(id, out Guid userId))
            {
                responseModel.Data = await _guideManager.GetGuideByUserIdAsync(userId);
                responseModel.Code = StateCode.Sucess;
            }
            return responseModel;
        }
    }
}
