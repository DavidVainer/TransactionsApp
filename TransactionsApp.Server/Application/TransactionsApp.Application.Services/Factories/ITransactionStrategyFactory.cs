using TransactionsApp.Application.Services.Strategies;
using TransactionsApp.Domain.Models.Enums;

namespace TransactionsApp.Application.Services.Factories
{
    /// <summary>
    /// Factory interface for creating transaction strategies based on the transaction type.
    /// </summary>
    public interface ITransactionStrategyFactory
    {
        /// <summary>
        /// Retrieves the matching transaction strategy for the given transaction type.
        /// </summary>
        /// <param name="transactionType">The type of the transaction</param>
        /// <returns>An instance of <see cref="ITransactionStrategy"/> of the specified transaction type.</returns>
        ITransactionStrategy GetStrategy(TransactionType transactionType);
    }
}
