using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TravelMeaning.IBLL;
using TravelMeaning.Models.DTO;
using TravelMeaning.Models.Model;
using TravelMeaning.Models.ResponseModels;
using TravelMeaning.Models.ResponseModels.User;
using TravelMeaning.Models.ViewModels.User;
using TravelMeaning.Web.Auth;

namespace TravelMeaning.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IRelationShipManager _relationShipManager;

        public UserController(IUserManager userManager, IHttpContextAccessor httpContext, IRelationShipManager relationShipManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _relationShipManager = relationShipManager ?? throw new ArgumentNullException(nameof(relationShipManager));
            _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
        }

        [Consumes("application/json")]
        [HttpPost(nameof(Login))]
        public async Task<ResponseModel<LogInModel>> Login(LoginViewModel viewModel)
        {
            var responseModel = new ResponseModel<LogInModel>
            {
                Data = new LogInModel(),
            };
            if (!string.IsNullOrEmpty(viewModel.Username) || !string.IsNullOrEmpty(viewModel.Password))
            {
                var user = await _userManager.Login(viewModel.Username, viewModel.Password);
                if (user != null)
                {
                    responseModel.Code = StateCode.Sucess;
                    var uesrInfo = await _userManager.Login(user.Id);
                    responseModel.Data = uesrInfo;
                    CustomPayloadModel tokenModel = new CustomPayloadModel
                    {
                        Id = user.Id,
                        Roles = uesrInfo.RolesStr
                    };
                    responseModel.Data.Token = JWTHelper.IssueJWT(tokenModel);

                }
            }

            return responseModel;
        }
        [HttpPost]
        [Consumes("application/json")]
        public async Task<ResponseModel<GenericModel>> Post(SignUpViewModel viewModel)
        {
            var responseModel = new ResponseModel<GenericModel>
            {
                Code = StateCode.Sucess,
                Data = new GenericModel
                {
                    IsSucess = false,
                    Message = string.Empty
                }
            };
            if (!await _userManager.HasUserByUsername(viewModel.Username))
            {
                await _userManager.SignUp(viewModel.Username, viewModel.Password, viewModel.PhoneNumber, "UserV1");
                responseModel.Data.IsSucess = await _userManager.HasUserByUsername(viewModel.Username);
            }
            return responseModel;
        }

        [HttpGet()]
        public async Task<ResponseModel<UserInfoDTO>> Get(string id)
        {
            var responseModel = new ResponseModel<UserInfoDTO>
            {
                Data = null
            };
            if (!Guid.TryParse(id, out Guid UserId))
            {
                return responseModel;
            }
            var userinfo = await _userManager.GetUserInfo(UserId);
            responseModel.Code = StateCode.Sucess;
            responseModel.Data = userinfo;
            return responseModel;
        }

        [HttpGet(nameof(GetUserDetailInfoById))]
        public async Task<ResponseModel<UserDetailInfoDTO>> GetUserDetailInfoById(string id)
        {
            Guid responseUserId = JWTHelper.SeriallzeUserId(_httpContext);
            RelationshipType type = RelationshipType.Detachment;
            var responseModel = new ResponseModel<UserDetailInfoDTO>
            {
                Data = null
            };
            if (Guid.TryParse(id, out Guid userId))
            {
                var userinfo = await _userManager.GetUserDetailInfo(userId);
                if (userId != Guid.Empty)
                {
                    type = await _relationShipManager.GetRelationshipType(responseUserId, userId);
                }
                responseModel.Data = userinfo;
                responseModel.Code = StateCode.Sucess;
                responseModel.Data.Type = type;
            }
            return responseModel;
        }
        [HttpPost(nameof(ModifyUserInfo))]
        public async Task<ResponseModel<GenericModel>> ModifyUserInfo(UserInfoDTO viewModel)
        {
            var responseModel = new ResponseModel<GenericModel>
            {
                Data = new GenericModel()
            };
            Guid userId = JWTHelper.SeriallzeUserId(_httpContext);
            responseModel.Data.IsSucess = await _userManager.ModifyUserInfo(userId, viewModel);
            return responseModel;
        }
    }
}