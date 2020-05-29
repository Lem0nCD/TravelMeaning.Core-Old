using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using TravelMeaning.BLL;
using TravelMeaning.DAL;
using TravelMeaning.IBLL;
using TravelMeaning.IDAL;

namespace TravelMeaning.Web.Extensions
{
    public static class DIRegister
    {
        public static void AddDBService(this IServiceCollection services)
        {
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleManager, RoleManager>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<ITravelGuideManager, TravelGuideManager>();
            services.AddScoped<ITravelGuideService, TravelGuideService>();
            services.AddScoped<ICommentManager, CommentManager>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ITravelGuideReviewManager, TravelGuideReviewManager>();
            services.AddScoped<ITravelGuideReviewService, TravelGuideReviewService>();
            services.AddScoped<ICommentReviewManager, CommentReviewManager>();
            services.AddScoped<ICommentReviewService, CommentReviewService>();
        }
        public static void AddHttpContextService(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
