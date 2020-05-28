using AutoMapper;
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
using TravelMeaning.Models.ResponseModels.User;

namespace TravelMeaning.BLL
{
    public class UserManager : IUserManager
    {
        protected readonly IUserService _userSvc;
        protected readonly IRoleService _roleSvc;
        protected readonly IUserRoleService _userRoleSvc;
        protected readonly ITravelGuideService _guideSvc;
        protected readonly IMapper _mapper;

        public UserManager(IUserService userSvc, IUserRoleService userRoleSvc, IRoleService roleSvc, IMapper mapper, ITravelGuideService guideSvc)
        {
            _userSvc = userSvc ?? throw new ArgumentNullException(nameof(userSvc));
            _userRoleSvc = userRoleSvc ?? throw new ArgumentNullException(nameof(userRoleSvc));
            _roleSvc = roleSvc ?? throw new ArgumentNullException(nameof(roleSvc));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _guideSvc = guideSvc ?? throw new ArgumentNullException(nameof(guideSvc));
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
            var rolesStr = string.Join(',', await GetUserRoles(userId));
            var user = await _userSvc.GetAll().Where(m => m.Id == userId).FirstOrDefaultAsync();
            var userinfo = _mapper.Map<UserInfoDTO>(user);
            userinfo.RolesStr = rolesStr;
            return userinfo;
        }
        public async Task<string[]> GetUserRoles(Guid userId)
        {
            return await _userRoleSvc.GetRolesByUserId(userId);
        }
        public async Task<UserInfoDTO> GetUserInfo(string username)
        {
            var rolesStr = string.Join(',', await GetUserRoles((await FindUserByUserName(username)).Id));
            var user = await _userSvc.GetAll().Where(m => m.Username == username).FirstOrDefaultAsync();
            var userinfo = _mapper.Map<UserInfoDTO>(user);
            userinfo.RolesStr = rolesStr;
            return userinfo;
        }
        public async Task<Guid> SignUp(string username, string password, string phoneNumber, string roleName = "UserV1")
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

        public async Task<UserDetailInfoDTO> GetUserDetailInfo(Guid userId)
        {
            var user = await _userSvc.GetAll().Where(m => m.Id == userId).FirstOrDefaultAsync();
            var guideList = await _guideSvc.GetAll().Where(x => x.UserId == userId).ToListAsync();
            var rolesStr = string.Join(',', await GetUserRoles(userId));
            var detailInfo = _mapper.Map<UserDetailInfoDTO>(user);
            foreach (var guide in guideList)
            {
                detailInfo.GuideCount ++;
                detailInfo.GuidesUpVoteCount += guide.UpVoteCount;
            }
            detailInfo.RolesStr = rolesStr;
            return detailInfo;
        }

        public Task<UserDetailInfoDTO> GetUserDetailInfo(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<LogInModel> Login(Guid userId)
        {
            //var rolesStr = string.Join(',', await GetUserRoles(userId));
            var rolesStr = string.Join(',', await _userRoleSvc.GetRolesByUserId(userId));
            var user = await _userSvc.GetAll().Where(m => m.Id == userId).FirstOrDefaultAsync();
            var userinfo = _mapper.Map<LogInModel>(user);
            userinfo.RolesStr = rolesStr;
            return userinfo;
        }

        public async Task<bool> ModifyUserInfo(Guid id, UserInfoDTO userInfo)
        {
            var user =await  _userSvc.GetAll().Where(x => x.Id == id).FirstOrDefaultAsync();
            user.Gender = userInfo.Gender;
            user.Username = userInfo.Username;
            user.Location = userInfo.Location;
            user.Occupation = userInfo.Occupation;
            user.PhoneNumber = userInfo.PhoneNumber;
            return await _userSvc.EditAsync(user);
        }
    }
}
