using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using School.Data.Entities.Identity;
using School.Infrastructure.Data;

namespace School.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServiceRegistration(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<int>>(Option =>
            {
                // Password settings.
                Option.Password.RequireDigit = true;
                Option.Password.RequireLowercase = true;
                Option.Password.RequireNonAlphanumeric = true;
                Option.Password.RequireUppercase = true;
                Option.Password.RequiredLength = 6;
                Option.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                Option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                Option.Lockout.MaxFailedAccessAttempts = 5;
                Option.Lockout.AllowedForNewUsers = true;

                // User settings.
                Option.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                Option.User.RequireUniqueEmail = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            return services;
        }
    }
}
