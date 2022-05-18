using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyCompany.Domain;
using MyCompany.Domain.Repositories.Abstract;
using MyCompany.Domain.Repositories.EntityFramework;
using MyCompany.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            // ���������� ����� Config � ���� �� appsettings.json
            Configuration.Bind("Project", new Config());

            // ���������� ������ ���������� ���������� � �������� ��������.
            // ��������� ��������� � ����������� ����� ����������.
            // � ����� ������ ����� �������� �� ������ ���������� (����� �����
            // ������ DbContext, ������ Provider).
            // �������� �� �������.
            services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();
            services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
            services.AddTransient<DataManager>();

            // ���������� ������� ��.
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Config.ConnectionString));

            // ����������� Identity �������.
            services.AddIdentity<IdentityUser, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true; // ������������� e-mail �������.
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            // ����������� authentication cookie.
            services.ConfigureApplicationCookie(option =>
            {
                option.Cookie.Name = "myCompanyAuth";
                option.Cookie.HttpOnly = true; // �� �������� �� ���������� �������.
                option.LoginPath = "/account/login";
                option.AccessDeniedPath = "/account/accessdenied";
                option.SlidingExpiration = true;
            });

            // ��������� ��������� ������������ � ������������� (MVC)
            services.AddControllersWithViews()
                // ���������� ������������� � asp.net core 3.0,
                // ����� ���� ����������, ��� ��� ���������� ������ �� ���������.
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // !!! ������� ����������� middleware ����� �����.
            
            // � �������� ���������� ��� ����� ������ ��������� ���������� �� �������.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // ���������� ��������� ��������� ������ � ���������� (css, js � �.�.)
            // ������� � ����� wwwroot.
            app.UseStaticFiles();

            // ���������� ������� �������������.
            app.UseRouting();

            // ���������� �������������� � �����������.
            // !!! ����� ������� �������������, �� �� ����������� ���������.
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            // ������������ ������ ��� �������� (Endpoints)
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
