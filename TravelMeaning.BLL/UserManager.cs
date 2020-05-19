using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TravelMeaning.DAL;
using TravelMeaning.IBLL;
using TravelMeaning.IDAL;
using TravelMeaning.Models.DTO;
using TravelMeaning.Models.Model;

namespace TravelMeaning.BLL
{
    public class UserManager : BaseManager<User>, IUserManager
    {
        protected readonly IUserService _userSvc;
        protected readonly IRoleService _roleSvc;
        protected readonly IUserRoleService _userRoleSvc;

        public UserManager(IUserService userSvc, IUserRoleService userRoleSvc, IRoleService roleSvc) : base(userSvc)
        {
            _userSvc = userSvc ?? throw new ArgumentNullException(nameof(userSvc));
            _userRoleSvc = userRoleSvc ?? throw new ArgumentNullException(nameof(userRoleSvc));
            _roleSvc = roleSvc ?? throw new ArgumentNullException(nameof(roleSvc));
        }
        public UserManager(IUserService userSvc) : base(userSvc)
        {
            _userSvc = userSvc ?? throw new ArgumentNullException(nameof(userSvc));
        }
        public async Task<bool> HasUserByUsername(string username)
        {
            return (await _userSvc.GetAll().Where(x => x.Username == username).FirstOrDefaultAsync()) != null ? true : false;
        }

        public async Task<User> Login(string username, string password)
        {
            return await GetAll().Where(m => m.Username == username && m.Password == password).FirstAsync();
        }
        public async Task<UserInfoDTO> UserInfo(Guid userId)
        {
            var userRole = await _userSvc.GetUserRole(userId);
            //var role = await _roleSvc.GetOneByIdAsync(userRole.RoleId);
            return await GetAll().Where(m => m.Id == userId).Select(m => new UserInfoDTO
            {
                Avatar = m.Avatar,
                Gender = m.Gender,
                Location = m.Location,
                Occupation = m.Occupation,
                PhoneNumber = m.PhoneNumber,
                UId = m.UId,
                Username = m.Username
            }).FirstAsync();
        }
        public async Task<UserInfoDTO> UserInfo(string username)
        {
            return await GetAll().Where(m => m.Username == username).Select(m => new UserInfoDTO
            {
                Avatar = m.Avatar,
                Gender = m.Gender,
                Location = m.Location,
                Occupation = m.Occupation,
                PhoneNumber = m.PhoneNumber,
                UId = m.UId,
                Username = m.Username
            }).FirstAsync();
        }

        public UserInfoDTO UserInfo(User user)
        {
            return new UserInfoDTO
            {
                Avatar = user.Avatar,
                Gender = user.Gender,
                Location = user.Location,
                Occupation = user.Occupation,
                PhoneNumber = user.PhoneNumber,
                UId = user.UId,
                Username = user.Username
            };
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

    }
}
