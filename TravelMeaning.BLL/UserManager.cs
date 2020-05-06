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
    public class UserManager : BaseManager<User>,IUserManager
    {
        protected readonly IUserService _userSvc;
        

        public UserManager(IUserService userSvc) : base(userSvc)
        {
            _userSvc = userSvc ?? throw new ArgumentNullException(nameof(userSvc));
        }

        public async Task<bool> Login(string username, string password)
        {
            var user = await base.GetAll().FirstAsync(m => m.Username == username && m.Password == password);
            return user != null;
        }

        public async Task<bool> Regiseter(string username, string password, string phoneNumber)
        {
            var user = await base.GetAll().FirstAsync(m => m.Username == username);
            var newUser = new User
            {
                Username = username,
                Password = password,
                PhoneNumber = phoneNumber
            };
            await base.CreateAsync(newUser);
            return (await base.GetOneByIdAsync(newUser.Id)) != null;
        }
    }
}
