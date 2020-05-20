using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<string[]> GetRolesByUserId(Guid userId)
        {
            return await GetAll().Where(x => x.UserId == userId).Select(x => x.Role.Name).ToArrayAsync();
        }

    }
}
