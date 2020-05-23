using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelMeaning.IBLL;
using TravelMeaning.Models.ResponseModels;
using TravelMeaning.Models.ResponseModels.TravelGuide;
using TravelMeaning.Models.ViewModels.TravelGude;
using TravelMeaning.Web.Auth;

namespace TravelMeaning.Web.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
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
        public string Get(string id)
        {
            return "value";
        }

        // POST: api/TravleGuide
        [HttpPost]
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
    }
}
