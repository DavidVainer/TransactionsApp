using TransactionsApp.Application.Models.BankingProvider.Responses;
using TransactionsApp.Application.Models.Dto;

namespace TransactionsApp.Application.Services.Services
{
    /// <summary>
    /// Represents the service interface for interacting with a banking provider.
    /// </summary>
    public interface IBankingProviderService
    {
        /// <summary>
        /// Generates a token for the banking provider.
        /// </summary>
        /// <param name="request">Request model.</param>
        /// <returns>Response model with generated token and request status.</returns>
        Task<TokenResponseModel> GenerateTokenAsync(AddTransactionDto dto);

        /// <summary>
        /// Executes a deposit transaction in the banking provider.
        /// </summary>
        /// <param name="request">Request model.</param>
        /// <returns>Response model with request and action statuses.</returns>
        Task<TransactionResponseModel> DepositAsync(AddTransactionDto dto);

        /// <summary>
        /// Executes a withdrawl transaction in the banking provider.
        /// </summary>
        /// <param name="request">Request model.</param>
        /// <returns>Response model with request and action statuses.</returns>
        Task<TransactionResponseModel> WithdrawAsync(AddTransactionDto dto);
    }
}
