using TransactionsApp.Application.Models.BankingProvider.Responses;
using TransactionsApp.Application.Models.Dto;
using TransactionsApp.Application.Services.Services;
using TransactionsApp.Application.Services.Strategies;

namespace TransactionsApp.Application.Services.Implementations.Strategies
{
    /// <summary>
    /// Strategy for processing a withdrawal transaction.
    /// </summary>
    public class WithdrawStrategy : ITransactionStrategy
    {
        private readonly IBankingProviderService _bankingProviderService;

        public WithdrawStrategy(IBankingProviderService bankingProviderService)
        {
            _bankingProviderService = bankingProviderService;
        }

        /// <summary>
        /// Processes a withdrawal transaction based on the provided data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer object containing transaction details.</param>
        /// <returns>A <see cref="BankingResponseModel"/> indicating the result of the transaction processing.</returns>
        public async Task<BankingResponseModel> ProcessTransactionAsync(AddTransactionDto dto)
        {
            return await _bankingProviderService.WithdrawAsync(dto);
        }
    }
}
