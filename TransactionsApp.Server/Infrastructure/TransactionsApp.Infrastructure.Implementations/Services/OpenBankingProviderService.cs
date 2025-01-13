using System.Text;
using System.Text.Json;
using TransactionsApp.Application.Models.BankingProvider.Requests;
using TransactionsApp.Application.Models.BankingProvider.Responses;
using TransactionsApp.Application.Models.Dto;
using TransactionsApp.Application.Services.Services;
using TransactionsApp.Infrastructure.Models.BankingProviderSettings;

namespace TransactionsApp.Infrastructure.Implementations.Services
{
    /// <summary>
    /// Represents the service interface for interacting with OpenBanking provider.
    /// </summary>
    public class OpenBankingProviderService : IBankingProviderService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IBankingProviderSettings _settings;

        public OpenBankingProviderService(IHttpClientFactory httpClientFactory, IBankingProviderSettings settings)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        /// <summary>
        /// Generates a token for the banking provider.
        /// </summary>
        /// <param name="dto">Data transfer object with data to execute a transaction.</param>
        /// <returns>Response model with generated token and request status.</returns>
        public async Task<TokenResponseModel> GenerateTokenAsync(AddTransactionDto dto)
        {
            var httpClient = _httpClientFactory.CreateClient("OpenBankingClient");

            var requestModel = new TokenRequestModel
            {
                UserId = dto.UserId,
                SecretId = _settings.CreateTokenSecretId,
            };

            var content = new StringContent(JsonSerializer.Serialize(requestModel), Encoding.UTF8, "application/json");

            await Task.Delay(500);

            return new TokenResponseModel
            {
                Code = "Success",
                Data = "FakeGeneratedToken"
            };
        }

        /// <summary>
        /// Executes a deposit transaction in the banking provider.
        /// </summary>
        /// <param name="dto">Data transfer object with data to execute a transaction.</param>
        /// <returns>Response model with request and action statuses.</returns>
        public async Task<TransactionResponseModel> DepositAsync(AddTransactionDto dto)
        {
            var response = await ExecuteTransaction(_settings.DepositUrl, dto);
            return response;
        }

        /// <summary>
        /// Executes a withdrawl transaction in the banking provider.
        /// </summary>
        /// <param name="dto">Data transfer object with data to execute a transaction.</param>
        /// <returns>Response model with request and action statuses.</returns>
        public async Task<TransactionResponseModel> WithdrawAsync(AddTransactionDto dto)
        {
            var response = await ExecuteTransaction(_settings.WithdrawUrl, dto);
            return response;
        }

        private async Task<TransactionResponseModel> ExecuteTransaction(string url, AddTransactionDto dto)
        {
            var httpClient = _httpClientFactory.CreateClient("OpenBankingClient");

            var requestModel = new TransactionRequestModel
            {
                Amount = dto.Amount,
                Bank = dto.AccountNumber,
            };

            var content = new StringContent(JsonSerializer.Serialize(requestModel), Encoding.UTF8, "application/json");

            await Task.Delay(500);

            return new TransactionResponseModel
            {
                Code = "Success",
                Data = "Transaction processed successfully."
            };
        }
    }
}
