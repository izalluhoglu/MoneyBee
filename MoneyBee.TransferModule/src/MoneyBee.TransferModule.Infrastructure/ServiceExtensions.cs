using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyBee.TransferModule.Application.Abstractions;
using MoneyBee.TransferModule.Infrastructure.Persistence;
using MoneyBee.TransferModule.Infrastructure.Repositories;

namespace MoneyBee.TransferModule.Infrastructure
{
    /// <summary>Registers infra services.</summary>
    public static class ServiceExtensions
    {
        /// <summary>Adds DbContexts and repositories using read/write connections.</summary>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var readConn = configuration.GetConnectionString("ReadDb");
            var writeConn = configuration.GetConnectionString("WriteDb");

            services.AddDbContext<TransferReadDbContext>(opt => opt.UseSqlServer(readConn));
            services.AddDbContext<TransferWriteDbContext>(opt => opt.UseSqlServer(writeConn));

            services.AddScoped<ITransferReadRepository, TransferReadRepository>();
            services.AddScoped<ITransferWriteRepository, TransferWriteRepository>();

            return services;
        }
    }
}
