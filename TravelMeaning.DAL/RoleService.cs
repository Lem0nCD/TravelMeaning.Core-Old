using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TravelMeaning.IDAL;
using TravelMeaning.Models.Data;
using TravelMeaning.Models.Model;

namespace TravelMeaning.DAL
{
    public class RoleService : BaseService<Role>, IRoleService
    {
        public RoleService(TMContext db):base(db)
        {

        }
        public async Task<Role> GetOnewByRoleName(string roleName)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.Name == roleName);
        }
    }
}
