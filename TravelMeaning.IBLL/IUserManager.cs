using System;
using System.Threading.Tasks;
using TravelMeaning.Models.DTO;
using TravelMeaning.Models.Model;

namespace TravelMeaning.IBLL
{
    public interface IUserManager : IBaseManager<User>
    {
        Task<Guid> SignUp(string username, string password, string phoneNumber, string roleName);
        Task<User> Login(string username, string password);
        Task<UserInfoDTO> UserInfo(Guid userId);
        Task<UserInfoDTO> UserInfo(string username);
        UserInfoDTO UserInfo(User user);
        Task<bool> HasUserByUsername(string username);

    }
}
