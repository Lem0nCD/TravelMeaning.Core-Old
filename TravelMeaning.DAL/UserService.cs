using System;
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
    }
}
