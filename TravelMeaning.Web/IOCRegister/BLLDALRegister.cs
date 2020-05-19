using Microsoft.Extensions.DependencyInjection;
using TravelMeaning.BLL;
using TravelMeaning.DAL;
using TravelMeaning.IBLL;
using TravelMeaning.IDAL;

namespace TravelMeaning.Web.IOCRegister
{
    public class BLLDALRegister
    {
        public void Register(IServiceCollection services)
        {
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
