using System;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.Application.Enums;
using AuthServer.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthServer.Infrastructure.Data
{
    public class AuthServerDbContextSeed
    {
        private static async Task SeedRolesAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
        }
        public static async Task SeedIdentityAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedRolesAsync(userManager, roleManager);
            await SeedAdminAsync(userManager, roleManager);
            await SeedBasicUserAsync(userManager, roleManager);
        }

        private static async Task SeedAdminAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new User
            {
                UserName = "superadmin",
                Email = "superadmin@localhost.com",
                FirstName = "pramod",
                LastName = "mishra",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Maeri(!!22");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Moderator.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                }

            }
        }
        private static async Task SeedBasicUserAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new User
            {
                UserName = "pramod",
                Email = "pramod@localhost.com",
                FirstName = "pramod",
                LastName = "pramod",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()

            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Maeri(!!22");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                }

            }
        }
    }
}
