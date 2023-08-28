using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using School.Data.Entities.Identity;

namespace School.Infrastructure.Seeder
{
    public static class UserSeeder
    {

        public static async Task SeedAsync(UserManager<User> _userManager)
        {
            var usersCount = await _userManager.Users.CountAsync();
            if (usersCount <= 0)
            {
                var defaultUser = new User()
                {
                    UserName = "admin",
                    Email = "admin@project.com",
                    FullName = "SchoolProject",
                    Country = "Egypt",
                    PhoneNumber = "123456",
                    Address = "Zagazg",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                };
                await _userManager.CreateAsync(defaultUser, "Aa123456@");
                await _userManager.AddToRoleAsync(defaultUser, "Admin");
            }
        }
    }
}
