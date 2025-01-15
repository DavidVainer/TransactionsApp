using System;
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
    /// Represents the service implementation for interacting with the banking provider.
    /// </summary>
    public class BankingProviderService : IBankingProviderService
    {
        private const string BANKING_RESPONSE_SUCCESS_CODE = "Success";

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IBankingProviderSettings _settings;

        public BankingProviderService(IHttpClientFactory httpClientFactory, IBankingProviderSettings settings)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        /// <summary>
        /// Generates a token for the banking provider.
        /// </summary>
        /// <param name="dto">Data transfer object with data to execute a transaction.</param>
        /// <returns>Response model with generated token and request status.</returns>
        public async Task<BankingResponseModel> GenerateTokenAsync(AddTransactionDto dto)
        {
            var requestModel = new TokenRequestModel
            {
                UserId = dto.UserIdentity,
                SecretId = _settings.CreateTokenSecretId,
            };

            var response = await SimulateRequestAsync(_settings.CreateTokenUrl, requestModel);

            return response;
        }

        /// <summary>
        /// Executes a deposit transaction in the banking provider.
        /// </summary>
        /// <param name="dto">Data transfer object with data to execute a transaction.</param>
        /// <returns>Response model with request and action statuses.</returns>
        public async Task<BankingResponseModel> DepositAsync(AddTransactionDto dto)
        {
            var response = await ExecuteTransaction(_settings.DepositUrl, dto);

            return response;
        }

        /// <summary>
        /// Executes a withdrawal transaction in the banking provider.
        /// </summary>
        /// <param name="dto">Data transfer object with data to execute a transaction.</param>
        /// <returns>Response model with request and action statuses.</returns>
        public async Task<BankingResponseModel> WithdrawAsync(AddTransactionDto dto)
        {
            var response = await ExecuteTransaction(_settings.WithdrawUrl, dto);

            return response;
        }

        /// <summary>
        /// Executes a transaction in the banking provider.
        /// </summary>
        /// <param name="url">Url to send request to.</param>
        /// <param name="dto">Data transfer object.</param>
        /// <returns>Request's response model.</returns>
        private async Task<BankingResponseModel> ExecuteTransaction(string url, AddTransactionDto dto)
        {
            var requestModel = new TransactionRequestModel
            {
                Amount = dto.Amount,
                Bank = dto.AccountNumber,
            };

            var response = await SimulateRequestAsync(url, requestModel);

            return response;
        }

        /// <summary>
        /// Simulates a request to the banking provider.
        /// </summary>
        /// <typeparam name="TRequest">Type of the request model.</typeparam>
        /// <param name="url">Url to send request to.</param>
        /// <param name="requestModel">Request model.</param>
        /// <returns>Request's response model.</returns>
        private async Task<BankingResponseModel> SimulateRequestAsync<TRequest>(string url, TRequest requestModel)
        {
            // Stimulating a request delay
            await Task.Delay(1000);

            // Uncomment the following code to make a real request
            // var httpClient = _httpClientFactory.CreateClient();
            // var content = new StringContent(JsonSerializer.Serialize(requestModel), Encoding.UTF8, "application/json");
            // var response = await httpClient.PostAsync(url, content);
            // var responseContent = await response.Content.ReadAsStringAsync();
            // var responseModel = JsonSerializer.Deserialize<BankingResponseModel>(responseContent);

            // return responseModel;

            var responseModel = new BankingResponseModel
            {
                Code = BANKING_RESPONSE_SUCCESS_CODE,
                Data = "Dummy data"
            };

            return responseModel;
        }
    }
}
