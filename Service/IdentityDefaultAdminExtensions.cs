using Microsoft.AspNetCore.Identity;
using OptiApp.Models;

namespace OptiApp.Service;

public static class IdentityDefaultAdminExtensions
{
    public static async Task CreateRolesAndDefaultAdmin(this IServiceCollection services,string email, string password)
    {
        var serviceProvider = services.BuildServiceProvider();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        // Check if the roles exist, and create them if not
        string[] roleNames = { Roles.User.ToString(), Roles.Optometrist.ToString(), Roles.Admin.ToString()};
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Create a default user with the Admin role if it doesn't exist
        var adminUser = await userManager.FindByEmailAsync(email);
        if (adminUser == null)
        {
            var user = new IdentityUser
            {
                UserName = email,
                Email = email,
            };
            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }
        }
    }
}