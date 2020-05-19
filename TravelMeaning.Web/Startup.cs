using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TravelMeaning.Models.Data;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using Microsoft.DotNet.PlatformAbstractions;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using TravelMeaning.DAL;
using TravelMeaning.IDAL;
using TravelMeaning.BLL;
using TravelMeaning.IBLL;
using TravelMeaning.Web.IOCRegister;

namespace TravelMeaning.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TMContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddControllers();
            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new OpenApiInfo
                {
                    Title = "TravelMeaning API",
                    Version = "V1",
                    Description = "API说明文档"
                });
                var xmlFile1 = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath1 = Path.Combine(AppContext.BaseDirectory, xmlFile1);
                var xmlFile2 = "TravelMeaning.Models.xml";
                var xmlPath2 = Path.Combine(ApplicationEnvironment.ApplicationBasePath, xmlFile2);
                c.IncludeXmlComments(xmlPath1, true);
                c.IncludeXmlComments(xmlPath2);
                //开启验证
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT验证 Bearer {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
            });
            #endregion
            #region CORS
            services.AddCors(options =>
            {
                options.AddPolicy("LimitRquest", policy =>
                {
                    policy
                    .WithOrigins("http://127.0.0.1:8080", "http://localhost:8080")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            #endregion
            #region Authorization
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
            });
            #endregion
            #region Custom Authentication
            var audienceConfig = Configuration.GetSection("Audience");
            var symmetricKeyAsbase64 = audienceConfig["Secret"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsbase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidIssuer = audienceConfig["Issuer"],
                ValidateAudience = true,
                ValidAudience = audienceConfig["Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(30),
                RequireExpirationTime = true,
            };
            services.AddAuthentication("Bearer").AddJwtBearer(o =>
            {
                o.TokenValidationParameters = tokenValidationParameters;
                o.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "True");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            #endregion
            #region IOC Container
            //BLLDALRegister bLLDALRegister = new BLLDALRegister();
            //bLLDALRegister.Register(services);
            services.AddScoped(typeof( IUserManager),typeof( UserManager));
            services.AddScoped(typeof(IUserService),typeof(UserService));
            //services.AddScoped<IUserService, UserService>();
            //services.AddSingleton<ILogger>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //logger.LogInformation("In Development environment");
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePages();
            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/V1/swagger.json", "TravelMeaning API V1");
                //c.RoutePrefix = string.Empty;
            });
            app.UseRouting();
            #endregion
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("LimitRquest");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}
