using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TravelMeaning.DAL;
using TravelMeaning.IBLL;
using TravelMeaning.IDAL;
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
            return await GetAll().FirstAsync(m => m.Username == username && m.Password == password);
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
