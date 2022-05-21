using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyCompany.Domain.Entities;
using System;

namespace MyCompany.Domain
{
    /// <summary>
    /// A class for linking the objects of our site with the database.
    /// </summary>
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// For linking of the TextField class with the database (table TextFields).
        /// </summary>
        public DbSet<TextField> TextFields { get; set; }

        /// <summary>
        /// For linking of the ServiceItem class with the database (ServiceItems table).
        /// </summary>
        public DbSet<ServiceItem> ServiceItems { get; set; }

        /// <summary>
        /// Filling the database with default values.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // We create a role for the user as an administrator.
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "8af10569-b018-4fe6-a380-7d6a14c70b74",
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            // Define an administrator-user.
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

            // In the auxiliary table, we link that our user has the Administrator role.
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "8af10569-b018-4fe6-a380-7d6a14c70b74",
                UserId = "3b62472e-4f66-49fa-a20f-e7685b9565d8"
            });

            // The text fields in order to change the content of the pages on the site and register SEO meta tags.

            // Create a TextField object in the database as a PageIndex.
            modelBuilder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("63dc8fa6-07ae-4391-8916-e057f71239ce"),
                CodeWord = "PageIndex",
                Title = "Main"
            });

            // Create a TextField object in the database as PageServices.
            modelBuilder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("70bf165a-700a-4156-91c0-e83fce0a277f"),
                CodeWord = "PageServices",
                Title = "Our services"
            });

            // Create a TextField object in the database as PageContacts.
            modelBuilder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("4aa76a4c-c59d-409a-84c1-06e6487a137a"),
                CodeWord = "PageContacts",
                Title = "Contacts"
            });
        }
    }
}
