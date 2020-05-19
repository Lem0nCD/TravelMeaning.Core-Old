using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TravelMeaning.IBLL;
using TravelMeaning.Models.Model;
using TravelMeaning.Models.ResponseModels;
using TravelMeaning.Models.ResponseModels.User;
using TravelMeaning.Models.ViewModels.User;
using TravelMeaning.Web.Auth;

namespace TravelMeaning.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [HttpPost]
        public async Task<ResponseModel<LogInModel>> Login(LoginViewModel viewModel)
        {
            var user = await _userManager.Login(viewModel.Username, viewModel.Password);
            var responseModel = new ResponseModel<LogInModel>
            {
                Code = StateCode.Illegal,
                Data = new LogInModel()
            };
            var roles = _userManager.
            if (user !=null)
            {
                CustomPayloadModel tokenModel = new CustomPayloadModel
                {
                    Id = user.Id
                };
                responseModel.Data.Token = JWTHelper.IssueJWT(tokenModel);
                responseModel.Code = StateCode.Sucess;
            }
            return responseModel;
        }
        [HttpPost]
        public async Task<ResponseModel<SignUpModel>> SignUp(SignUpViewModel viewModel)
        {
            var response = new ResponseModel<SignUpModel>
            {
                Code = StateCode.Sucess,
                Data = new SignUpModel
                {
                    IsSucess = false,
                    Message = string.Empty
                }
            };
            if (!await _userManager.HasUserByUsername(viewModel.Username))
            {
                await _userManager.SignUp(viewModel.Username, viewModel.Password, viewModel.PhoneNumber,"UserV1");
                response.Data.IsSucess = await _userManager.HasUserByUsername(viewModel.Username);
            }
            return response;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var list = _userManager.GetAll().ToList();
            User one = new User();
            for (int i = 0; i < 100; i++)
            {
                try
                {
                    one = list.FirstOrDefault();

                }
                catch (Exception)
                {

                    throw;
                }
            }
            return Ok(await _userManager.GetOneByIdAsync(one.Id));
        }
    }
}