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
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Stb.Api.JwtToken;
using Microsoft.Extensions.Options;
using Stb.Api.Services;
using Swashbuckle.Swagger.Model;
using Microsoft.Extensions.PlatformAbstractions;

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
                //builder.AddUserSecrets();
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
              options.UseSqlServer(Configuration.GetConnectionString("StbConnection"), b => b.UseRowNumberForPaging()));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentity<PlatformUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentity<Platoon, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();

            services.AddIdentity<Worker, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();

            services.AddIdentity<EndUser, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();

            //services.AddAuthorization(options =>
            //{
            //    //options.AddPolicy("Authenticated", policy => policy.RequireClaim(ClaimTypes.Name));
            //    options.AddPolicy(Policies.AdministratorOnly, policy => policy.RequireClaim(ClaimTypes.Role, "系统管理员"));
            //});

            services.AddMvc();

            services.AddSwaggerGen();
            // Add the detail information for the API.
            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Stb Api",
                    Description = "师徒帮APP后台接口",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "房鹤"/*, Email = "wyshmily@126.com", Url = "http://www.ivmchina.com/platform"*/ },
                    //License = new License { Name = "Use under LICX", Url = "http://url.com" }
                });

                //Determine base path for the application.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;

                //Set the comments path for the swagger json and ui.
                var xmlPath = System.IO.Path.Combine(basePath, "Stb.xml");
                options.IncludeXmlComments(xmlPath);
            });


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

            string secretKey = Configuration.GetValue<string>("TokenSecretKey");
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            services.Configure<TokenProviderOptions>(options =>
            {
                options.Issuer = "www.ivmchina.com";
                options.Audience = "www.ivmchina.com/app";
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
                options.Expiration = TimeSpan.FromSeconds(1);
            });

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();


            services.AddTransient<PlatoonAuthService>();
            services.AddTransient<WorkerAuthService>();
            services.AddTransient<UserService>();
            services.AddTransient<OrderService>();
        }




        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<PlatformUser> userManager, IOptions<TokenProviderOptions> tokenOptions)
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
                app.UseExceptionHandler("/platform/Home/Error");
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUi();


            app.UseStaticFiles();

            app.UseIdentity();

            //app.UseCookieAuthentication(new CookieAuthenticationOptions()
            //{
            //    AuthenticationScheme = "Cookie",
            //    LoginPath = new PathString("/platform/Account/login/"),
            //    AccessDeniedPath = new PathString("/platform/Account/Forbidden/"),
            //    AutomaticAuthenticate = true,
            //    AutomaticChallenge = true
            //});
            // The secret key every token will be signed with.
            // In production, you should store this securely in environment variables
            // or a key management tool. Don't hardcode this into your application!


            string secretKey = Configuration.GetValue<string>("TokenSecretKey");
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = tokenOptions.Value.Issuer,

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = tokenOptions.Value.Audience,

                // Validate the token expiry
                ValidateLifetime = false,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.FromSeconds(30)
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = false,
                AutomaticChallenge = false,
                AuthenticationScheme = "Bearer",
                TokenValidationParameters = tokenValidationParameters,
            });

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "platform",
                    template: "platform/{controller=Home}/{action=Index}/{id?}",
                    defaults: new { area = "platform" });
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "official",
                    template: "{controller=Home}/{action=Index}/{id?}",
                    defaults: new { area = "official" });
            });

            DbInitializer.Initialize(context, roleManager, userManager);

        }
    }
}
