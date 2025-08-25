using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MoneyBee.TransferModule.Application
{
    /// <summary>Registers application services.</summary>
    public static class ServiceExtensions
    {
        /// <summary>Adds MediatR and application handlers.</summary>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            
            return services;
        }
    }
}