using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MyCompany.Domain;
using MyCompany.Domain.Repositories.Abstract;
using MyCompany.Domain.Repositories.EntityFramework;

namespace MyCompany.Service
{
    public static class Extensions
    {
        /// <summary>
        /// The method removes the word "Controller" from the string.
        /// </summary>
        /// <param name="str">The string to remove the word "Controller".</param>
        /// <returns>A string without the word "Controller".</returns>
        public static string CutController(this string str)
        {
            return str.Replace("Controller", "");
        }

        /// <summary>
        /// Setting up the Identity system.
        /// </summary>
        /// <param name="services"></param>
        public static void AddIdentity(this IServiceCollection services)
        {
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
        }

        /// <summary>
        /// Set up an authentication cookie.
        /// </summary>
        /// <param name="services"></param>
        public static void AddConfigureAppCookie(this IServiceCollection services)
        {
            // Set up an authentication cookie.
            services.ConfigureApplicationCookie(option =>
            {
                option.Cookie.Name = "myCompanyAuth";
                option.Cookie.HttpOnly = true; // not available on the client side.
                option.LoginPath = "/account/login";
                option.AccessDeniedPath = "/account/accessdenied";
                option.SlidingExpiration = true;
            });
        }

        /// <summary>
        /// Management of the repository.
        /// </summary>
        /// <param name="services"></param>
        public static void AddDataManager(this IServiceCollection services)
        {
            // Associate an interface with an implementation of that interface.
            // At any time, we can replace it with another implementation
            // (then there will be another DbContext, another Provider).
            // AddTransient - created on request.
            services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();
            services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
            services.AddTransient<DataManager>();

        }
    }
}
