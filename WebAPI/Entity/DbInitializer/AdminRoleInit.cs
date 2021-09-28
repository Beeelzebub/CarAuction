using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Entity.DbInitializer
{
    public class AdminRoleInit
    {
        public static void  SeedUser(ModelBuilder builder)
        {
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            User user = new User()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                Name = "admin",
                LockoutEnabled = true,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                PasswordHash = passwordHasher.HashPassword(null, "1234")
            };

            
            

            builder.Entity<User>().HasData(user);
        }

        public static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895711", Name = "admin", NormalizedName = "ADMIN" }
            );
        }
        public static void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "fab4fac1-c546-41de-aebc-a14da6895711", UserId = "b74ddd14-6340-4840-95c2-db12554843e5" }
            );
        }
    }


}
