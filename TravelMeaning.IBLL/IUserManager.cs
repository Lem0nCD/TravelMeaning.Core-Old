using System.Threading.Tasks;
using TravelMeaning.Models.Model;

namespace TravelMeaning.IBLL
{
    public interface IUserManager : IBaseManager<User>
    {
        Task<bool> Regiseter(string username, string password, string phoneNumber);
        Task<bool> Login(string username, string password);

    }
}
