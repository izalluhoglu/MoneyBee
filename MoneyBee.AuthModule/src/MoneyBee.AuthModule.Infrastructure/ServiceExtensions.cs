using Microsoft.EntityFrameworkCore; 
using Microsoft.Extensions.Configuration; 
using Microsoft.Extensions.DependencyInjection;
using MoneyBee.AuthModule.Application.Abstractions; 
using MoneyBee.AuthModule.Infrastructure.Auth;
using MoneyBee.AuthModule.Infrastructure.Persistence;
using MoneyBee.AuthModule.Infrastructure.Repositories; 
using MoneyBee.AuthModule.Infrastructure.Security;

namespace MoneyBee.AuthModule.Infrastructure
{
    /// <summary>Registers services.</summary>
    public static class ServiceExtensions
    {
        /// <summary>Adds DbContexts, repositories, and auth services.</summary>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var readConn = configuration.GetConnectionString("ReadDb"); 
            var writeConn = configuration.GetConnectionString("WriteDb");
            
            services.AddDbContext<AuthReadDbContext>(opt => opt.UseSqlServer(readConn));
            services.AddDbContext<AuthWriteDbContext>(opt => opt.UseSqlServer(writeConn));
            
            services.AddScoped<IEmployeeReadRepository, EmployeeReadRepository>(); 
            services.AddScoped<IEmployeeWriteRepository, EmployeeWriteRepository>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            
            services.Configure<JwtOptions>(configuration.GetSection("Jwt")); 
            services.AddSingleton<IJwtTokenService, JwtTokenService>();
            
            return services;
        }
    }
}