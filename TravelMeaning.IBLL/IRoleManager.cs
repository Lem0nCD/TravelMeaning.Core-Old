using System.Collections.Generic;
using System.Threading.Tasks;
using TravelMeaning.Models.Model;

namespace TravelMeaning.IBLL
{
    public interface IRoleManager : IBaseManager<Role>
    {
        public Task<bool> CreateRole(string roleName, string descrption);
        public Task<bool> DropRole(string roleName);
    }
}
