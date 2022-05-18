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
            // Подключаем класс Config с инфо из appsettings.json
            Configuration.Bind("Project", new Config());

            // Подключаем нужный функционал приложения в качестве сервисов.
            // Связываем интерфейс с реализацией этого интерфейса.
            // В любой момент можем заменить на другую реализацию (будет тогда
            // другой DbContext, другой Provider).
            // Создаётся по запросу.
            services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();
            services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
            services.AddTransient<DataManager>();

            // Подключаем контекс БД.
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Config.ConnectionString));

            // Настраиваем Identity систему.
            services.AddIdentity<IdentityUser, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true; // Подтверждение e-mail письмом.
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            // Настраиваем authentication cookie.
            services.ConfigureApplicationCookie(option =>
            {
                option.Cookie.Name = "myCompanyAuth";
                option.Cookie.HttpOnly = true; // не доступны на клиентской стороне.
                option.LoginPath = "/account/login";
                option.AccessDeniedPath = "/account/accessdenied";
                option.SlidingExpiration = true;
            });

            // Добавляем поддержку контроллеров и представлений (MVC)
            services.AddControllersWithViews()
                // выставляем совместимость с asp.net core 3.0,
                // чтобы быть уверенными, что при обновлении ничего не сломалось.
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // !!! Порядок регистрации middleware очень важен.
            
            // В процессе разработки нам важно видеть подробную информацию об ошибках.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // Подключаем поддержку статичных файлов в приложении (css, js и т.д.)
            // которые в папке wwwroot.
            app.UseStaticFiles();

            // Подключаем систему маршрутизации.
            app.UseRouting();

            // Подключаем аутентификацию и авторизацию.
            // !!! ПОСЛЕ системы маршрутизации, но ДО определения маршрутов.
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            // Регистрируем нужные нам маршруты (Endpoints)
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
