using System.Threading.Tasks;
using TravelMeaning.Models.Model;

namespace TravelMeaning.IDAL
{
    public interface IRoleService: IBaseService<Role>
    {
        public Task<Role> GetOnewByRoleName(string roleName);

    }
}
