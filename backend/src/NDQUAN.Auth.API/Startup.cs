using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NDQUAN.Auth.API.Helpers;
using NDQUAN.Auth.API.IService;
using NDQUAN.Auth.API.Service;
using NDQUAN.Core.DL.IServices;
using NDQUAN.Core.DL.Services;
using System;

namespace NDQUAN.Auth.API
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
            services.AddControllers();
            BaseStartupConfig.ConfigureServices(ref services);
            //config strongly typed settings object


            //(new BaseStartupConfig()).ConfigureServices(services, Configuration);

            //Configute DI for application service 
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDatabaseService, DatabaseService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(options => {
                if (env.IsDevelopment())
                {
                    options.SetIsOriginAllowed(origin => new Uri(origin).Host.Contains("localhost"))
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                }
                else
                {

                }
            });


            //app.UseAuthorization();
            app.UseMiddleware<JwtMiddleware>();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
