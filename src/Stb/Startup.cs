using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Stb.Data;
using Stb.Data.Models;
using Stb.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using Stb.Api.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Stb.Platform;

namespace Stb
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("StbConnection")));

            services.AddIdentity<PlatformUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentity<Platoon, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();

            //services.AddAuthorization(options =>
            //{
            //    //options.AddPolicy("Authenticated", policy => policy.RequireClaim(ClaimTypes.Name));
            //    options.AddPolicy(Policies.AdministratorOnly, policy => policy.RequireClaim(ClaimTypes.Role, "系统管理员"));
            //});

            services.AddMvc();


            //services.Configure<RazorViewEngineOptions>(options =>
            //{
            //    options.AreaViewLocationFormats.Clear();
            //    options.AreaViewLocationFormats.Add("{2}/Views/{1}/{0}.cshtml");
            //    options.AreaViewLocationFormats.Add("{2}/Views/Shared/{0}.cshtml");
            //    //options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            //});

            services.Configure<Settings>(Configuration);

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // Lockout settings
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                //options.Lockout.MaxFailedAccessAttempts = 10;

                // Cookie settings
                options.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(1);
                options.Cookies.ApplicationCookie.LoginPath = "/Platform/Account/LogIn";
                //options.Cookies.ApplicationCookie.LogoutPath = "/Account/LogOff";
                options.Cookies.ApplicationCookie.AccessDeniedPath = "/Platform/Account/Forbidden";

                // User settings
                options.User.RequireUniqueEmail = false;
            });

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<PlatformUser> userManager)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            //app.UseCookieAuthentication(new CookieAuthenticationOptions()
            //{
            //    AuthenticationScheme = "Cookie",
            //    LoginPath = new PathString("/Account/login/"),
            //    AccessDeniedPath = new PathString("/Account/Forbidden/"),
            //    AutomaticAuthenticate = true,
            //    AutomaticChallenge = true
            //});

            //app.UseBearerAuthentication(options =>
            //{
            //    options.AuthenticationScheme = "Bearer";
            //    options.AutomaticAuthenticate = false;
            //});

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "platform/{controller=Home}/{action=Index}/{id?}");
            });

            DbInitializer.Initialize(context, roleManager);
        }
    }
}
