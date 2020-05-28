using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelMeaning.Web.Extensions
{
    public static class AuthorizatoinSetup
    {
        public static void AddAuthorizatoinSetup(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserV1", policy => policy.RequireRole("UserV1").Build());
                options.AddPolicy("Review", policy => policy.RequireRole("Editor", "Admin").Build());
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
            });
        }
    }
}
