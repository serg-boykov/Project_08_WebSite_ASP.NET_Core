using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyCompany.Domain.Entities;
using System;

namespace MyCompany.Domain
{
    /// <summary>
    /// Класс для связи объектов нашего сайта с базой данных.
    /// </summary>
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// Связь класса TextField с базой данных (таблица TextFields).
        /// </summary>
        public DbSet<TextField> TextFields { get; set; }

        /// <summary>
        /// Связь класса ServiceItem с базой данных (таблица ServiceItems).
        /// </summary>
        public DbSet<ServiceItem> ServiceItems { get; set; }

        /// <summary>
        /// Заполняем базу данных значениями по умолчанию.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Создаем роль для пользователя как Администратор.
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "8af10569-b018-4fe6-a380-7d6a14c70b74",
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            // Определяем пользователя-администратора.
            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = "3b62472e-4f66-49fa-a20f-e7685b9565d8",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "my@email.com",
                NormalizedEmail = "MY@EMAIL.COM",
                EmailConfirmed = true, // обязательно true, чтобы можно было авторизоваться.
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "superpassword"),
                SecurityStamp = string.Empty
            });

            // Во вспомогательной таблице связываем, что нашему пользователю принадлежит роль Администратора.
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "8af10569-b018-4fe6-a380-7d6a14c70b74",
                UserId = "3b62472e-4f66-49fa-a20f-e7685b9565d8"
            });

            // Далее идут текстовые поля для того, чтобы изменять содержание страниц на сайте
            // и прописать SEO метатеги.

            // Создаём объект TextField в базе данных как PageIndex.
            modelBuilder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("63dc8fa6-07ae-4391-8916-e057f71239ce"),
                CodeWord = "PageIndex",
                Title = "Main"
            });

            // Создаём объект TextField в базе данных как PageServices.
            modelBuilder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("70bf165a-700a-4156-91c0-e83fce0a277f"),
                CodeWord = "PageServices",
                Title = "Our services"
            });

            // Создаём объект TextField в базе данных как PageContacts.
            modelBuilder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("4aa76a4c-c59d-409a-84c1-06e6487a137a"),
                CodeWord = "PageContacts",
                Title = "Contacts"
            });
        }
    }
}
