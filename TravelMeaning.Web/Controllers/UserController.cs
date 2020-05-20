﻿using Microsoft.AspNetCore.Mvc;
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
                Data = new LogInModel(),
            };

            if (user != null)
            {
                responseModel.Code = StateCode.Sucess;
                var uesrInfo = await _userManager.GetUserInfo(user.Id);
                responseModel.Data.UserInfo = uesrInfo;
                CustomPayloadModel tokenModel = new CustomPayloadModel
                {
                    Id = user.Id,
                    Roles = uesrInfo.RolesStr
                };
                responseModel.Data.Token = JWTHelper.IssueJWT(tokenModel);

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
                await _userManager.SignUp(viewModel.Username, viewModel.Password, viewModel.PhoneNumber, "UserV1");
                response.Data.IsSucess = await _userManager.HasUserByUsername(viewModel.Username);
            }
            return response;
        }
    }
}