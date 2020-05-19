using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelMeaning.IDAL;
using TravelMeaning.Models.Data;
using TravelMeaning.Models.Model;

namespace TravelMeaning.DAL
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(TMContext db) : base(db)
        {
        }

        public async Task<List<UserRole>> GetUserRole(Guid userId)
        {
            throw new ArgumentNullException();
            //return await GetAll().Where(x => x.;
        }
    }
}
