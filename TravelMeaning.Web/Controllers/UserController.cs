using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TravelMeaning.IBLL;
using TravelMeaning.Models.Model;
using TravelMeaning.Models.ViewModels.User;

namespace TravelMeaning.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            return new JsonResult(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
        {
            if (!await _userManager.HasUserByUsername(viewModel.Username))
            {
                await _userManager.SignUp(viewModel.Username, viewModel.Password, viewModel.PhoneNumber);
                return Ok(viewModel);
            }
            else
            {
                return Ok("repeated username");
            }
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