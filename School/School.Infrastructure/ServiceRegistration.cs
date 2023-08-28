using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using School.Data.Entities.Identity;
using School.Data.Helpers;
using School.Infrastructure.Data;
using System.Text;

namespace School.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<User, Role>(Option =>
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
                Option.User.RequireUniqueEmail = true;
                Option.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            //JWT Authentication
            var jwtSettings = new JwtSettings();
            configuration.GetSection(nameof(jwtSettings)).Bind(jwtSettings);
            services.AddSingleton(jwtSettings);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;

                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = jwtSettings.ValidateIssuer,
                    ValidIssuers = new[] { jwtSettings.Issure },
                    ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSignigKey,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                    ValidAudience = jwtSettings.Audience,
                    ValidateAudience = jwtSettings.ValidateAudience,
                    ValidateLifetime = jwtSettings.ValidateLifetime,

                };
            });

            services.AddSwaggerGen(swagger =>
             {
                 //This is to generate the Default UI of Swagger Documentation  

                 swagger.SwaggerDoc("v1", new OpenApiInfo
                 {
                     Version = "v1",
                     Title = "JWT Token Authentication API",
                     Description = "ASP.NET Core 3.1 Web API"
                 });
                 // To Enable authorization using Swagger (JWT)  
                 swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                 {
                     Name = "Authorization",
                     Type = SecuritySchemeType.ApiKey,
                     Scheme = "Bearer",
                     BearerFormat = "JWT",
                     In = ParameterLocation.Header,
                     Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                 });
                 swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                   {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                   });
             });



            return services;
        }
    }
}
