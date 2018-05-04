using System;
using GMS.Data;
using GMS.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GMS.ASPNet.Core
{
    /// <summary>
    /// ASP Net Core application startup routine.
    /// Configure the Web App as needed.
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        { 
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Use this method to configure services that are going to be used by the web app.
        /// Eg: Entity Framework Database Service, ASP Net Identity Service
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add GMS.Data.DataContext to servicesn as the Entity Framework Database Model
            // and pass the MySql Connection string to it.
            services.AddDbContext<DataContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("GoogleSQLConnection")));


            // Add GMS.Data.AppUser as the ASP Net Identity user class, passing IdentityRole<Guid> as the
            // default user role. Pass GMS.Data.DataContext as the Entity Framework store to be used to
            // store user details Use the default token provider to provide tokens for password reset etc....
            services.AddIdentity<AppUser, IdentityRole<Guid>>().AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            
            // Configure ASP Net Identity to behave as required
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            // Configure cookies to maintain Identity Settings
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/Session/Login";
                options.AccessDeniedPath = "/Session/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddMvc();
        }


        /// <summary>
        /// Configure non-service related web app settings such as routing and exception handling
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Display full stack trace for exceptions when in development mode
            // Display simple error page for exceptions under deployment
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Allows static html files to be server
            app.UseStaticFiles();

            // Allows ASP Net Identity to be used for user authentication
            app.UseAuthentication();


            // Allows the MVC architecture to be used
            // Specifies the default page to be displayed. i.e. Home page
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "admin-users",
                    template: "Admin/Users/List",
                    defaults: new {controller = "Admin", action = "List"});

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
