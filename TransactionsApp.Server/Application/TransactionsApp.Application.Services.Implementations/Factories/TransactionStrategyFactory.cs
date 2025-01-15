using Microsoft.Extensions.DependencyInjection;
using TransactionsApp.Application.Services.Factories;
using TransactionsApp.Application.Services.Implementations.Strategies;
using TransactionsApp.Application.Services.Strategies;
using TransactionsApp.Domain.Models.Enums;

namespace TransactionsApp.Application.Services.Implementations.Factories
{
    /// <summary>
    /// Factory for creating transaction strategies based on the transaction type.
    /// </summary>
    public class TransactionStrategyFactory : ITransactionStrategyFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public TransactionStrategyFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        /// <summary>
        /// Retrieves the matching transaction strategy for the given transaction type.
        /// </summary>
        /// <param name="transactionType">The type of the transaction</param>
        /// <returns>An instance of <see cref="ITransactionStrategy"/> of the specified transaction type.</returns>
        public ITransactionStrategy GetStrategy(TransactionType transactionType)
        {
            switch (transactionType)
            {
                case TransactionType.Deposit:
                    return _serviceProvider.GetRequiredService<DepositStrategy>();
                case TransactionType.Withdrawal:
                    return _serviceProvider.GetRequiredService<WithdrawStrategy>();
                default:
                    throw new ArgumentException("Invalid transaction type.");
            }
        }
    }

}
