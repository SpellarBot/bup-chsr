using CHSR.Data;
using CHSR.Domain.UAM;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CHSR.DataSeeder
{
    public class DummyData
    {
        public static async Task Initialize(ApplicationDbContext context,
                              UserManager<ApplicationUser> userManager,
                              RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();



            string role1 = "Admin";

            string password = "1qaz4esz";

            if (await roleManager.FindByNameAsync(role1) == null)
            {
                await roleManager.CreateAsync(new IdentityRole() { Name = role1 });
            }

            if (await userManager.FindByNameAsync("admin@bookish.com") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "admin@bookish.com",
                    Email = "admin@bookish.com",
                    PhoneNumber = "6902341234"
                };

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    //await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role1);
                }
            }
        }
    }
}
