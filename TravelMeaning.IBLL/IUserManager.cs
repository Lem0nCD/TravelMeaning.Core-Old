﻿using System;
using System.Threading.Tasks;
using TravelMeaning.Models.Model;

namespace TravelMeaning.IBLL
{
    public interface IUserManager : IBaseManager<User>
    {
        Task<Guid> SignUp(string username, string password, string phoneNumber, string roleName);
        Task<User> Login(string username, string password);
        Task<bool> HasUserByUsername(string username);
        Task<Role> sig
    }
}
