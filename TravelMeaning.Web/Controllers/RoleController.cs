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
using TravelMeaning.Models.ViewModels.Role;

namespace TravelMeaning.Web.Controllers
{
    [Authorize(policy:"Admin")]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleManager _roleManager;

        public RoleController(IRoleManager roleManager)
        {
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        // GET: api/Role
        [HttpGet]
        public async Task<ResponseModel<List<Role>>> Get()
        {
            var responseModel = new ResponseModel<List<Role>>();
            responseModel.Data = await _roleManager.GetAllRoles();
            responseModel.Code = StateCode.Sucess;
            return responseModel;
        }

        // GET: api/Role/5
        [HttpGet("{id}")]
        public async Task<ResponseModel<Role>> Get(string id)
        {
            var responseModel = new ResponseModel<Role>();
            if (!string.IsNullOrEmpty(id))
            {
                if (Guid.TryParse(id,out Guid roleId))
                {
                    responseModel.Data = await _roleManager.GetOneRoleById(roleId);
                }
                else
                {
                    responseModel.Data = await _roleManager.GetOneRoleByName(id);
                }
                responseModel.Code = StateCode.Sucess;
            }
            return responseModel;
        }

        // POST: api/Role
        [HttpPost]
        public async Task<ResponseModel<GenericModel>> Post(CreateRoleViewModel viewModel)
        {
            var responseModel = new ResponseModel<GenericModel>
            {
                Data = new GenericModel()
            };
            if (!string.IsNullOrEmpty(viewModel.RoleName))
            {
                responseModel.Data.IsSucess = await _roleManager.CreateRole(viewModel.RoleName, viewModel.Description);
                responseModel.Code = StateCode.Sucess;
            }
            return responseModel;
        }

        // PUT: api/Role/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{name}")]
        public async Task<ResponseModel<GenericModel>> Delete(string name)
        {
            var responseModel = new ResponseModel<GenericModel>
            {
                Data = new GenericModel()
            };
            if (!string.IsNullOrWhiteSpace(name))
            {
                responseModel.Data.IsSucess = await _roleManager.DropRole(name);
                responseModel.Code = StateCode.Sucess;
            }
            return responseModel;
        }
    }
}
