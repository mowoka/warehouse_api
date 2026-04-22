using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace DataWarehouse.Api.Extensions;

public static class AuthExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                    };
            });
            
        services.AddAuthorization(options =>
        {
            options.AddPolicy("StaffOrAbove",   p=> p.RequireRole("Staff", "Manager", "Admin"));
            options.AddPolicy("ManagerOrAbove", p => p.RequireRole("Manager", "Admin"));
            options.AddPolicy("AdminOnly",      p => p.RequireRole("Admin")); 
        });
        
        return services;
    }
}