using TransactionsApp.Application.Models.BankingProvider.Responses;
using TransactionsApp.Application.Models.Dto;

namespace TransactionsApp.Application.Services.Strategies
{
    /// <summary>
    /// Defines a strategy pattern for processing a transaction.
    /// </summary>
    public interface ITransactionStrategy
    {
        /// <summary>
        /// Processes a transaction based on the provided data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer object containing transaction details.</param>
        /// <returns>A <see cref="BankingResponseModel"/> indicating the result of the transaction processing.</returns>
        Task<BankingResponseModel> ProcessTransactionAsync(AddTransactionDto dto);
    }
}
