using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelMeaning.IBLL;
using TravelMeaning.Models.Model;
using TravelMeaning.Models.ResponseModels;
using TravelMeaning.Models.ViewModels.Relationship;
using TravelMeaning.Web.Auth;

namespace TravelMeaning.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelationshipController : ControllerBase
    {
        private readonly IRelationShipManager _relationShipManager;
        private readonly IHttpContextAccessor _httpContext;


        public RelationshipController(IRelationShipManager relationShipManager, IHttpContextAccessor httpContext)
        {
            _relationShipManager = relationShipManager ?? throw new ArgumentNullException(nameof(relationShipManager));
            _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
        }

        // GET: api/Relationship
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Relationship/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        [Authorize(policy: "UserV1")]
        // PUT: api/Relationship/5
        [HttpPatch]
        public async Task<ResponseModel<GenericModel>> Patch(ChangeTypeViewModel viewModel)
        {
            Guid userId = JWTHelper.SeriallzeUserId(_httpContext);
            var responseModel = new ResponseModel<GenericModel>();
            if (Guid.TryParse(viewModel.Id, out Guid toUserId) && Enum.TryParse(viewModel.Type.ToString(), out RelationshipType type))
            {
                await _relationShipManager.ChangeType(userId, toUserId, type);
                responseModel.Data = new GenericModel
                {
                    IsSucess = true,
                };
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
