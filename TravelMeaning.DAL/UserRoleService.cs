using TravelMeaning.IDAL;
using TravelMeaning.Models.Data;
using TravelMeaning.Models.Model;

namespace TravelMeaning.DAL
{
    public class UserRoleService : BaseService<UserRole>, IUserRoleService
    {
        public UserRoleService(TMContext db) : base(db)
        {
        }


    }
}
