using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelMeaning.Models.ViewModels.Test;
using TravelMeaning.Web.Auth;

namespace TravelMeaning.Web.Controllers
{
    //[Consumes("multipart/form-data")]
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
                Roles = "Admin"
            };
            var jwtStr = JWTHelper.IssueJWT(tokenModel);
            var suc = true;
            return Ok(new
            {
                sucess = suc,
                token = jwtStr
            });
        }
        /// <summary>
        /// 验证权限
        /// 格式：Authorization Bearer [token]
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = "UserV1")]
        public IActionResult AuthorizeRolesUserV1()
        {
            return Ok("UserV1");
        }

        [HttpGet]
        [HttpPost]
        public IActionResult TestConnect()
        {
            return Ok("testconnect");
        }

        //[HttpPost]
        //public async  Task<IActionResult> UpLoadFile(IFormFileCollection files)
        //{
        //    long size = files.Sum(f => f.Length);
        //    foreach (var formFile in files)
        //    {
        //        if (formFile.Length > 0)
        //        {
        //            var filePath = Path.GetTempFileName();
        //            using(var stream = System.IO.File.Create(filePath))
        //            {
        //                await formFile.CopyToAsync(stream);
        //            }
        //        }
        //    }
        //    return Ok(new { count = files.Count, size });
        //}        
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [RequestSizeLimit(long.MaxValue)]
        [HttpPost]
        public async Task<IActionResult> UpLoadFile()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", DateTime.Now.Ticks.ToString() + ".png");
            var files = Request.Form.Files;
            long size = files.Sum(f => f.Length);
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = System.IO.File.Create(path))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            return Ok(new { count = files.Count, size });
        }

        [Consumes("application/json")]
        [HttpPost]
        public IActionResult MdEditorContent(ContentViewModel viewModel)
        {
            Console.WriteLine(viewModel.Content);
            return Ok(new { data = viewModel.Content });
        }
    }
}