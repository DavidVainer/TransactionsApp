using Microsoft.EntityFrameworkCore;
using TransactionsApp.Application.Services.Implementations.Managers;
using TransactionsApp.Application.Services.Managers;
using TransactionsApp.Application.Services.Repositories;
using TransactionsApp.Application.Services.Services;
using TransactionsApp.Domain.Models.Entities;
using TransactionsApp.Infrastructure.Implementations;
using TransactionsApp.Infrastructure.Implementations.Repositories;
using TransactionsApp.Infrastructure.Implementations.Services;
using TransactionsApp.Infrastructure.Models.BankingProviderSettings;

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

            services.AddHttpClient("OpenBankingClient");

            services.AddDbContext<TransactionsAppDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<ITransactionManager, TransactionManager>();

            services.AddScoped<IBankingProviderService, OpenBankingProviderService>();

            services.AddSingleton<IBankingProviderSettings>(new BankingProviderSettings
            {
                CreateTokenUrl = "https://openBanking/createtoken",
                CreateTokenSecretId = "Je45GDf34",
                DepositUrl = "https://openBanking/createdeposit",
                WithdrawUrl = "https://openBanking/createWithdrawal",
            });

            services.AddScoped<IRepository<Transaction>, TransactionRepository>();

            services.AddScoped<IRepository<User>, BaseRepository<User>>();
        }
    }
}
