using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyBee.CustomerModule.Application.Abstractions;
using MoneyBee.CustomerModule.Infrastructure.Persistence;
using MoneyBee.CustomerModule.Infrastructure.Repositories;

namespace MoneyBee.CustomerModule.Infrastructure
{ 
    public static class ServiceExtensions
    {
        /// <summary>Adds DbContexts and repositories using read/write connections.</summary>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var readConn = configuration.GetConnectionString("ReadDb");
            var writeConn = configuration.GetConnectionString("WriteDb");

            services.AddDbContext<CustomerReadDbContext>(opt => opt.UseSqlServer(readConn));
            services.AddDbContext<CustomerWriteDbContext>(opt => opt.UseSqlServer(writeConn));

            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            
            return services;
        }
    }
}
