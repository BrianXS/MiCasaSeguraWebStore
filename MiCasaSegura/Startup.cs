using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiCasaSegura.Filters;
using MiCasaSegura.Models.Identity;
using MiCasaSegura.Services.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MiCasaSegura
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MiCasaSeguraDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("local"));
            });

            services.AddIdentity<User, Role>(options => {
                //password options
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;

                //email options
                options.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<MiCasaSeguraDbContext>();

            services.ConfigureApplicationCookie(options => {
                options.LoginPath = "/Auth/Login";
            });

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
            }).AddFacebook(facebookOptions => {
                facebookOptions.AppId = "985083918501303";
                facebookOptions.AppSecret= "e006547f389831fc72440efb8d7fa27f";
            }).AddMicrosoftAccount(microsoftOptions => {
                microsoftOptions.ClientId = "e6414f73-426d-4a2c-9da3-6dc52630fe63";
                microsoftOptions.ClientSecret = "z:P[MyKwWTHmQ2Xkmz4X-H3yUgf4SC:7";
            }).AddCookie();

            services.AddScoped<AnonymousOnly>();
            services.AddScoped<MissingUserInfo>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();

            DatabaseInitialize.SeedData(userManager, roleManager);

            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{Controller=Home}/{Action=Index}/{id?}");
            });
        }
    }
}
