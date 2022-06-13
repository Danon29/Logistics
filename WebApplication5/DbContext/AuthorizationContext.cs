using Microsoft.EntityFrameworkCore;
using Logistics.Models.Authorization;

namespace Logistics.DBContext
{
    public class AuthorizationContext: DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        

        public AuthorizationContext(DbContextOptions<AuthorizationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminEmail = "admin@mail.ru";
            string adminPassword = "123456";

            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            User adminUser = new User { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id  };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            
            base.OnModelCreating(modelBuilder);
        }


    }
}
