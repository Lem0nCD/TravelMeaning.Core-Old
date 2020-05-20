using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelMeaning.IBLL;
using TravelMeaning.IDAL;
using TravelMeaning.Models.Model;

namespace TravelMeaning.BLL
{
    public class RoleManager :IRoleManager
    {
        protected readonly IRoleService _roleSvc;

        public RoleManager(IRoleService roleSvc)
        {
            _roleSvc = roleSvc ?? throw new ArgumentNullException(nameof(roleSvc));
        }

        public async Task<bool> CreateRole(string roleName, string descrption)
        {
            return await _roleSvc.CreateAsync(new Role
            {
                Name = roleName,
                Description = descrption,
            });
        }

        public async Task<bool> DropRole(string roleName)
        {
            var role = _roleSvc.GetAll().Where(x => x.Name == roleName).FirstOrDefault();
            if (role != null)
            {
            return await _roleSvc.RemoveAsync(role);
            }
            throw new NullReferenceException("没有该权限名");
        }

        public async Task<List<Role>> GetAllRole()
        {
            return await _roleSvc.GetAll().ToListAsync();
        }
    }
}
