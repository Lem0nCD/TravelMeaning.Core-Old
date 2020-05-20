﻿using AutoMapper;
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
        protected readonly IMapper _mapper;

        public UserManager(IUserService userSvc, IUserRoleService userRoleSvc, IRoleService roleSvc, IMapper mapper)
        {
            _userSvc = userSvc ?? throw new ArgumentNullException(nameof(userSvc));
            _userRoleSvc = userRoleSvc ?? throw new ArgumentNullException(nameof(userRoleSvc));
            _roleSvc = roleSvc ?? throw new ArgumentNullException(nameof(roleSvc));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
    }
}
