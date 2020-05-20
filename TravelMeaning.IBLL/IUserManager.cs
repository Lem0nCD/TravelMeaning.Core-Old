using System;
using System.Threading.Tasks;
using TravelMeaning.Models.DTO;
using TravelMeaning.Models.Model;

namespace TravelMeaning.IBLL
{
    public interface IUserManager
    {
        Task<Guid> SignUp(string username, string password, string phoneNumber, string roleName);
        Task<User> Login(string username, string password);
        Task<UserInfoDTO> GetUserInfo(Guid userId);
        Task<UserInfoDTO> GetUserInfo(string username);
        Task<bool> HasUserByUsername(string username);
        Task<User> FindUserByUserName(string username);

    }
}
