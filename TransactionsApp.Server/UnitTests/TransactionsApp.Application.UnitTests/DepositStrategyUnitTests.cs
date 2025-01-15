using FakeItEasy;
using TransactionsApp.Application.Models.BankingProvider.Responses;
using TransactionsApp.Application.Models.Dto;
using TransactionsApp.Application.Services.Implementations.Strategies;
using TransactionsApp.Application.Services.Services;

namespace TransactionsApp.Application.UnitTests
{
    [TestClass]
    public class DepositStrategyUnitTests
    {
        private IBankingProviderService _fakeBankingProviderService;
        private DepositStrategy _depositStrategy;

        [TestInitialize]
        public void Setup()
        {
            _fakeBankingProviderService = A.Fake<IBankingProviderService>();
            _depositStrategy = new DepositStrategy(_fakeBankingProviderService);
        }

        [TestMethod]
        public async Task ProcessTransactionAsync_ValidDto_CallsDepositAsync()
        {
            var dto = new AddTransactionDto
            {
                Amount = 100,
                AccountNumber = "123456789",
            };

            var expectedResponse = new BankingResponseModel
            {
                Code = "Success",
                Data = "Deposit successful"
            };

            A.CallTo(() => _fakeBankingProviderService.DepositAsync(dto))
                .Returns(Task.FromResult(expectedResponse));

            var result = await _depositStrategy.ProcessTransactionAsync(dto);

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResponse, result);
        }
    }
}
