using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TravelMeaning.Models.Data;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using Microsoft.DotNet.PlatformAbstractions;
using Swashbuckle.AspNetCore.Filters;

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
            services.AddDbContext<TMContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
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
                    .WithOrigins("http://127.0.0.1:5500", "http://127.0.0.1:5500")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/V1/swagger.json", "TravelMeaning API V1");
            });
            app.UseRouting();

            app.UseAuthorization();
            app.UseCors("LimitRquest");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
