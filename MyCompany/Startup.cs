using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyCompany.Domain;
using MyCompany.Domain.Repositories.Abstract;
using MyCompany.Domain.Repositories.EntityFramework;
using MyCompany.Service;

namespace MyCompany
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Binding the Config class with info from appsettings.json
            Configuration.Bind("Project", new Config());


            // We connect the necessary functionality of the application as services.
            // Associate an interface with an implementation of that interface.
            // At any time, we can replace it with another implementation
            // (then there will be another DbContext, another Provider).
            // AddTransient - created on request.
            services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();
            services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
            services.AddTransient<DataManager>();

            // Connecting the database context.
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Config.ConnectionString));

            // Setting up the Identity system.
            services.AddIdentity<IdentityUser, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true; // Email confirmation.
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            // Set up an authentication cookie.
            services.ConfigureApplicationCookie(option =>
            {
                option.Cookie.Name = "myCompanyAuth";
                option.Cookie.HttpOnly = true; // not available on the client side.
                option.LoginPath = "/account/login";
                option.AccessDeniedPath = "/account/accessdenied";
                option.SlidingExpiration = true;
            });

            // Set up an authorization policy for the Admin area.
            services.AddAuthorization(x =>
            {
                // In the "AdminArea" policy, we require the "admin" role from the user.
                x.AddPolicy("AdminArea", policy => { policy.RequireRole("admin"); });
            });

            // Add Services for Controllers and Views (MVC)
            services.AddControllersWithViews(x =>
                {
                    // Add the "Admin" area to the Agreement with the "AdminArea" policy defined above.
                    x.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea"));
                })
                // We set compatibility with asp.net core 3.0
                // to be sure that nothing broke during the upgrade.
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0)
                .AddSessionStateTempDataProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // !!! The order in which the middleware is registered is very important.

            // During the development process,
            // it is important for us to see detailed information about errors.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // We connect support for static files in the application (css, js, etc.)
            // which are in the wwwroot folder.
            app.UseStaticFiles();

            // We connect the routing system.
            app.UseRouting();

            // We connect authentication and authorization.
            // !!! AFTER the routing system, but BEFORE the routes are defined.
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();


            // We register the routes we need (Endpoints).
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("admin", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
