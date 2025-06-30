using Microsoft.AspNetCore.Identity;

namespace CourierManagementSystem.API.Seeders
{
    public class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "Staff", "Customer" };

            foreach (var roleName in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}
