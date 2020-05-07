using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelMeaning.Web.Auth;

namespace TravelMeaning.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// 获取Jwt串
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetJwtStr(string name, string pass)
        {
            CustomPayloadModel tokenModel = new CustomPayloadModel
            {
                Id = Guid.NewGuid(),
                Role = "Admin"
            };
            var jwtStr = JWTHelper.IssueJWT(tokenModel);
            var suc = true;
            return Ok(new
            {
                sucess = suc,
                token = jwtStr
            });
        }
        [HttpGet]
        [Authorize("Admin")]
        public IActionResult AuthorizeRolesAdmin()
        {
            return Ok();
        }
    }
}