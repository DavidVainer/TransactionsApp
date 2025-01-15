using Microsoft.EntityFrameworkCore;
using TransactionsApp.Application.Models.Dto;
using TransactionsApp.Application.Models.Settings;
using TransactionsApp.Application.Services.Factories;
using TransactionsApp.Application.Services.Implementations.Factories;
using TransactionsApp.Application.Services.Implementations.Managers;
using TransactionsApp.Application.Services.Implementations.Mappers;
using TransactionsApp.Application.Services.Implementations.Strategies;
using TransactionsApp.Application.Services.Managers;
using TransactionsApp.Application.Services.Repositories;
using TransactionsApp.Application.Services.Services;
using TransactionsApp.Common.Services;
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
        private const string BANKING_PROVIDER_SETTINGS_KEY = "BankingProviderSettings";

        /// <summary>
        /// Configures services and dependencies for the application in the IoC container.
        /// </summary>
        /// <param name="services">The collection of services.</param>
        /// <param name="configuration">The configuration instance.</param>
        public static void ConfigureDI(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(DEFAULT_CONNECTION_STRING_KEY);
            var bankingProviderSettings = configuration.GetSection(BANKING_PROVIDER_SETTINGS_KEY).Get<BankingProviderSettings>();

            services.AddHttpClient();

            services.AddDbContext<TransactionsAppDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<ITransactionManager, TransactionManager>();

            services.AddScoped<IUserManager, UserManager>();

            services.AddScoped<IBankingProviderService, BankingProviderService>();

            services.AddSingleton<IBankingProviderSettings>(bankingProviderSettings);

            services.AddScoped<IRepository<Transaction>, TransactionRepository>();

            services.AddScoped<IRepository<User>, BaseRepository<User>>();

            services.AddSingleton<IUpdateTransactionDtoValidatorSettings>(new UpdateTransactionDtoValidatorSettings
            {
                AmountMinValue = 0,
                AmountMaxValue = 10000000000,
                AccountNumberPattern = @"^\d{1,10}$"
            });

            services.AddSingleton<IAddTransactionDtoValidatorSettings>(new AddTransactionDtoValidatorSettings
            {
                FullNameHebrewPattern = @"^[א-ת'\- ]{1,20}$",
                FullNameEnglishPattern = @"^[A-Za-z'\- ]{1,15}$",
                DateOfBirthMaximum = DateTime.Now,
                UserIdentityPattern = @"^\d{9}$",
                AmountMinValue = 0,
                AmountMaxValue = 10000000000,
                AccountNumberPattern = @"^\d{1,10}$"
            });

            services.AddScoped<DepositStrategy>();

            services.AddScoped<WithdrawStrategy>();

            services.AddScoped<ITransactionStrategyFactory, TransactionStrategyFactory>();

            services.AddScoped<IMapper<AddUserDto, User>, UserMapper>();

            services.AddScoped<IMapper<AddTransactionDto, Transaction>, TransactionMapper>();

            services.AddScoped<IMapper<AddTransactionDto, AddUserDto>, AddUserDtoMapper>();
        }
    }
}
