using Microsoft.EntityFrameworkCore;
using TransactionsApp.Application.Services.Implementations.Managers;
using TransactionsApp.Application.Services.Managers;
using TransactionsApp.Application.Services.Repositories;
using TransactionsApp.Domain.Models.Entities;
using TransactionsApp.Infrastructure.Implementations;
using TransactionsApp.Infrastructure.Implementations.Repositories;

namespace TransactionsApp.API.IoC
{
    /// <summary>
    /// Extension methods for configuring services and dependencies in the IoC container.
    /// </summary>
    public static class DependencyInjection
    {
        private const string DEFAULT_CONNECTION_STRING_KEY = "DefaultConnection";

        /// <summary>
        /// Configures services and dependencies for the application in the IoC container.
        /// </summary>
        /// <param name="services">The collection of services.</param>
        /// <param name="configuration">The configuration instance.</param>
        public static void ConfigureDI(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(DEFAULT_CONNECTION_STRING_KEY);

            services.AddDbContext<TransactionsAppDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IRepository<Transaction>, BaseRepository<Transaction>>();

            services.AddScoped<IRepository<User>, BaseRepository<User>>();
        }
    }
}
