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
        private readonly IHttpContextAccessor _httpContext;

        public TravleGuideController(ITravelGuideManager guideManager, IHttpContextAccessor httpContext)
        {
            _guideManager = guideManager ?? throw new ArgumentNullException(nameof(guideManager));
            _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
        }

        // GET: api/TravleGuide
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/TravleGuide/5
        [HttpGet("{id}", Name = "Get")]
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
            var userId = JWTHelper.SeriallzeJwt(((string)_httpContext.HttpContext.Request.Headers["Authorization"]).Replace("Bearer ", string.Empty)).Id;
            var result = await _guideManager.CreateGuideAsync(userId, viewModel.Content, viewModel.Title, viewModel.CoverImageUrl);
            return new ResponseModel<CreatedGuideModel>
            {
                Code = StateCode.Sucess,
                Data = new CreatedGuideModel
                {
                    Id = result,
                    isSucess = result == Guid.Empty || result == null
                }
            };
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

        [HttpPost]
        public async Task<ResponseModel<dynamic>> UpVoteToGuide(UpVoteToGuideViewModel viewModel)
        {
            var responseModel = new ResponseModel<dynamic>
            {
                Data = new
                {
                    result = false,
                }
            };
            if (Guid.TryParse(viewModel.Id, out Guid guideId) && Guid.TryParse(viewModel.UserId, out Guid userId))
            {
                await _guideManager.AddUpVoteCount(guideId, userId);
                responseModel.Data.result = true;
            }
            return responseModel;
        }
    }
}
