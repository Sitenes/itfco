using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Toplearn.Core.Senders;
using Toplearn.Core.Services;
using Toplearn.Core.Services.Interfaces;
using Toplearn.DataLayer.Context;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace Toplearn
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            #region MVC & RazorPages
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });
            services.AddRazorPages();
            #endregion
            #region Caching
            services.AddResponseCaching();
            services.AddMemoryCache();
            #endregion

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "API"
                });
            });

            services.Configure<FormOptions>(option => option.MultipartBodyLengthLimit = 6000000);
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            #region IoC
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IViewRenderService, RenderViewToString>();
            services.AddTransient<IWalletService, WalletService>();
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddTransient<ICourseService, CourseService>();
            #endregion

            #region DataBase Context
            services.AddDbContext<ToplearnContext>(options =>
                options.UseSqlServer(
                    "Data Source=AMIN-LAPTOP\\SQLEXPRESS;Initial Catalog=Toplearn_DB;Integrated Security=true;MultipleActiveResultSets=true;",
                    b => b.MigrationsAssembly("Toplearn.Web")),
                    ServiceLifetime.Transient
            );
            #endregion

            #region Authorization
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
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

            #region Uses
            app.UseStaticFiles();
            app.UseRouting();
            app.UseResponseCaching();

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(n => n.SwaggerEndpoint("/swagger/v1/swagger.json", "API"));
            #endregion

            #region security
            app.UseAuthentication();
            app.UseAuthorization();
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapDefaultControllerRoute();
            });
            #endregion

        }
    }
}
