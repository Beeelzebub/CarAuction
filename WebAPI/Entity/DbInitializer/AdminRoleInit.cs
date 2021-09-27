using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Models;
using Microsoft.AspNetCore.Identity;

namespace Entity.DbInitializer
{
    public class AdminRoleInit
    {
        public static async Task  InitializeAsync(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            const string userName = "admin";
            const string password = "1234";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await userManager.FindByNameAsync(userName) == null)
            {
                var admin = new User { Name = userName, UserName = userName };
                var result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
