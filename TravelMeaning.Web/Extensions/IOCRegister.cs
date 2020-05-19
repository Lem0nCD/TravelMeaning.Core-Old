using Microsoft.Extensions.DependencyInjection;
using TravelMeaning.BLL;
using TravelMeaning.DAL;
using TravelMeaning.IBLL;
using TravelMeaning.IDAL;

namespace TravelMeaning.Web.Extensions
{
    public static class IOCRegister
    {
        public static void RegisterDBService(this IServiceCollection services)
        {

            services.AddScoped(typeof(IUserManager), typeof(UserManager));
            services.AddScoped(typeof(IUserService), typeof(UserService));
            services.AddScoped(typeof(IRoleManager), typeof(RoleManager));
            services.AddScoped(typeof(IRoleService), typeof(RoleService));
            services.AddScoped(typeof(IUserRoleService), typeof(UserRoleService));
        }
    }
}
