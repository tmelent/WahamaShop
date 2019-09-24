using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Wahama.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using Wahama.Controllers;
using System.Linq;

namespace Wahama
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<WarhammerContext>(options => options.UseSqlServer(connection), ServiceLifetime.Transient);
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => //CookieAuthenticationOptions
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });
            services.AddMvc();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            WarhammerContext _context = new WarhammerContext();
            AccountController accountController = new AccountController(_context);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.Use(async (context, next) =>
        {

            string GuidId = "";
            if (context.Request.Cookies.ContainsKey("UnauthorizedID"))
            {
                GuidId = context.Request.Cookies["UnauthorizedID"];
            }
            else
            {
                GuidId = Guid.NewGuid().ToString();
                context.Response.Cookies.Append("UnauthorizedID", GuidId, new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddDays(14) });
            }
            if (!_context.Users.Any(p => p.Login == GuidId))
            {
                await accountController.Register(new ViewModels.RegisterModel
                {
                    Login = GuidId,
                    Password = accountController.HashPassword(GuidId),
                    FirstName = "Temporary",
                    LastName = "Account",
                    PhoneNumber = "+70000000",
                    ExpirationDate = DateTime.Now.AddDays(14)
                }, true);
            }
            
            await next();
        });
            accountController.Dispose();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}