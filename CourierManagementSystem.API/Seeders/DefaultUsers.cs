using CourierManagementSystem.API.Models;
using Microsoft.AspNetCore.Identity;

namespace CourierManagementSystem.API.Seeders
{
    public class DefaultUsers
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            // Admin user
            if (await userManager.FindByEmailAsync("admin@cms.com") == null)
            {
                var user = new ApplicationUser
                {
                    FullName = "System Admin",
                    UserName = "admin@cms.com",
                    Email = "admin@cms.com",
                    Role = "Admin"
                };

                var result = await userManager.CreateAsync(user, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }

            // Staff user
            if (await userManager.FindByEmailAsync("staff@cms.com") == null)
            {
                var user = new ApplicationUser
                {
                    FullName = "Courier Staff",
                    UserName = "staff@cms.com",
                    Email = "staff@cms.com",
                    Role = "Staff"
                };

                var result = await userManager.CreateAsync(user, "Staff@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Staff");
                }
            }

            // Customer user
            if (await userManager.FindByEmailAsync("customer@cms.com") == null)
            {
                var user = new ApplicationUser
                {
                    FullName = "Test Customer",
                    UserName = "customer@cms.com",
                    Email = "customer@cms.com",
                    Role = "Customer"
                };

                var result = await userManager.CreateAsync(user, "Customer@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }
        }
    }
}
