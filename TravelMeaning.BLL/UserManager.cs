using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelMeaning.DAL;
using TravelMeaning.IBLL;
using TravelMeaning.IDAL;
using TravelMeaning.Models.DTO;
using TravelMeaning.Models.Model;

namespace TravelMeaning.BLL
{
    public class UserManager : IUserManager
    {
        protected readonly IUserService _userSvc;
        protected readonly IRoleService _roleSvc;
        protected readonly IUserRoleService _userRoleSvc;

        public UserManager(IUserService userSvc, IUserRoleService userRoleSvc, IRoleService roleSvc)
        {
            _userSvc = userSvc ?? throw new ArgumentNullException(nameof(userSvc));
            _userRoleSvc = userRoleSvc ?? throw new ArgumentNullException(nameof(userRoleSvc));
            _roleSvc = roleSvc ?? throw new ArgumentNullException(nameof(roleSvc));
        }
        public UserManager(IUserService userSvc)
        {
            _userSvc = userSvc ?? throw new ArgumentNullException(nameof(userSvc));
        }
        public async Task<bool> HasUserByUsername(string username)
        {
            return (await _userSvc.GetAll().Where(x => x.Username == username).FirstOrDefaultAsync()) != null ? true : false;
        }

        public async Task<User> Login(string username, string password)
        {
            return await _userSvc.GetAll().Where(m => m.Username == username && m.Password == password).FirstAsync();
        }
        public async Task<UserInfoDTO> GetUserInfo(Guid userId)
        {
            var rolesStr = string.Join(',',await GetUserRoles(userId));
            return await _userSvc.GetAll().Where(m => m.Id == userId).Select(m => new UserInfoDTO
            {
                Avatar = m.Avatar,
                Gender = m.Gender,
                Location = m.Location,
                Occupation = m.Occupation,
                PhoneNumber = m.PhoneNumber,
                UId = m.UId,
                Username = m.Username,
                RolesStr = rolesStr
            }).FirstAsync();
        }
        public async Task<string[]> GetUserRoles(Guid userId)
        {
            return await _userRoleSvc.GetRolesByUserId(userId);
        }
        public async Task<UserInfoDTO> GetUserInfo(string username)
        {
            var rolesStr = string.Join(',', await GetUserRoles((await FindUserByUserName(username)).Id));
            return await _userSvc.GetAll().Where(m => m.Username == username).Select(m => new UserInfoDTO
            {
                Avatar = m.Avatar,
                Gender = m.Gender,
                Location = m.Location,
                Occupation = m.Occupation,
                PhoneNumber = m.PhoneNumber,
                UId = m.UId,
                Username = m.Username,
                RolesStr = rolesStr

            }).FirstAsync();
        }
        public async Task<Guid> SignUp(string username, string password, string phoneNumber, string roleName="UserV1")
        {
            var role = await _roleSvc.GetOnewByRoleName(roleName);
            if (role != null)
            {
                var newUser = new User
                {
                    Username = username,
                    Password = password,
                    PhoneNumber = phoneNumber
                };
                Guid newUserId = newUser.Id;
                var newUserRole = new UserRole
                {
                    UserId = newUserId,
                    RoleId = role.Id
                };
                if (await _userSvc.CreateAsync(newUser) && await _userRoleSvc.CreateAsync(newUserRole))
                {
                    return newUserId;
                }
            }
            throw new Exception("Create User Fail!");
        }

        public async Task<User> FindUserByUserName(string username)
        {
            return await _userSvc.GetAll().Where(x => x.Username == username).FirstOrDefaultAsync();
        }
    }
}
