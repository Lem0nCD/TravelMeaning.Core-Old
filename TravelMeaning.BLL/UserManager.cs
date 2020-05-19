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


        public UserManager(IUserService userSvc) : base(userSvc)
        {
            _userSvc = userSvc ?? throw new ArgumentNullException(nameof(userSvc));
        }

        public async Task<bool> HasUserByUsername(string username)
        {
            return (await _userSvc.GetAll().Where(x => x.Username == username).FirstOrDefaultAsync()) != null ? true : false;
        }

        public async Task<bool> Login(string username, string password)
        {
            var user = await GetAll().FirstAsync(m => m.Username == username && m.Password == password);
            return user != null;
        }

        public async Task<Guid> SignUp(string username, string password, string phoneNumber)
        {
            var newUser = new User
            {
                Username = username,
                Password = password,
                PhoneNumber = phoneNumber
            };
            Guid id = newUser.Id;
            if (await CreateAsync(newUser))
            {
                return id;
            }
            throw new Exception("Create Fail!");
        }
    }
}
