using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ABF.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        // Two custom fields, which are to be added to the Registration Form for Users
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        public string PostCode { get; set; }

        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("UserDbConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new IdentityDbInitialiser());
            Database.Initialize(true);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class IdentityDbInitialiser : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "BoxOffice"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "BoxOffice" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "User"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "User" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser {
                    Id = "admin",
                    UserName = "admin@test.net",
                    Id = "admin",
                    Email = "admin@test.net",
                    Name = "Admin User",
                    PostCode = "S40 9UF",
                    EmailConfirmed = true,
                };

                manager.Create(user, "admin123");
                manager.AddToRole(user.Id, "Admin");
            }

            if (!context.Users.Any(u => u.UserName == "boxoffice"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser {
                    Id = "boxoffice",
                    UserName = "boxoffice@test.net",
                    Id = "boxoffice",
                    Email = "boxoffice@test.net",
                    Name = "Box Office User",
                    PostCode = "S70 4HJ",
                    EmailConfirmed = true,
                };

                manager.Create(user, "boxoffice123");
                manager.AddToRole(user.Id, "BoxOffice");
            }

            if (!context.Users.Any(u => u.UserName == "testuser"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser {
                    Id = "user",
                    UserName = "testuser@test.net",
                    Id = "user",
                    Email = "testuser@test.net",
                    Name = "Standard User",
                    PostCode = "S8 7RW",
                    EmailConfirmed = true,
                };

                manager.Create(user, "user123");
                manager.AddToRole(user.Id, "User");
            }

        }
    }
}