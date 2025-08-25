using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MoneyBee.CustomerModule.Application
{
    /// <summary>Registers application services.</summary>
    public static class ServiceExtensions
    {
        /// <summary>Adds MediatR and application handlers.</summary>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}