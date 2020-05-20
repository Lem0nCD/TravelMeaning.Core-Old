using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelMeaning.Models.Model;

namespace TravelMeaning.IDAL
{
    public interface IUserRoleService : IBaseService<UserRole>
    {
        public Task<string[]> GetRolesByUserId(Guid userId);
    }
}
