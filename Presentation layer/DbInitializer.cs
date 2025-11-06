using DAL.Entity;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;

namespace Presentation_layer
{
    public static class DbInitializer
    {
        public static async Task SeedAdminAsync(IServiceProvider serviceProvider, IConfiguration config)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var roleName = "admin";

            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            var email = config["Admin:Email"];
            var password = config["Admin:Password"];
            var username = config["Admin:Username"];
            var fname = config["Admin:FName"];
            var lname = config["Admin:LName"];
            var address = config["Admin:Address"];

            var user = await userManager.FindByNameAsync(username);

            if (user == null)
            {
                user = new Admin
                {
                    UserName = username ,
                    Email = email,
                    EmailConfirmed = true,
                    fname = fname,
                    lname = lname,
                    address = address
                };

                var result = await userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    throw new Exception(
                        string.Join(" | ", result.Errors.Select(e => e.Description))
                    );
                }
            }

            if (!await userManager.IsInRoleAsync(user, roleName))
            {
                await userManager.AddToRoleAsync(user, roleName);
            }
        }
    }
}

