using FakeItEasy;
using TransactionsApp.Application.Services.Implementations.Factories;
using TransactionsApp.Application.Services.Implementations.Strategies;
using TransactionsApp.Domain.Models.Enums;

namespace TransactionsApp.Application.UnitTests
{
    [TestClass]
    public class TransactionStrategyFactoryTests
    {
        private IServiceProvider _fakeServiceProvider;
        private TransactionStrategyFactory _factory;

        [TestInitialize]
        public void Setup()
        {
            _fakeServiceProvider = A.Fake<IServiceProvider>();
            _factory = new TransactionStrategyFactory(_fakeServiceProvider);
        }

        [TestMethod]
        public void GetStrategy_Deposit_ReturnsDepositStrategy()
        {
            var depositStrategy = A.Fake<DepositStrategy>();
            A.CallTo(() => _fakeServiceProvider.GetService(typeof(DepositStrategy)))
                .Returns(depositStrategy);

            var result = _factory.GetStrategy(TransactionType.Deposit);

            Assert.IsNotNull(result);
            Assert.AreEqual(depositStrategy, result);
        }

        [TestMethod]
        public void GetStrategy_Withdrawal_ReturnsWithdrawStrategy()
        {
            var withdrawStrategy = A.Fake<WithdrawStrategy>();
            A.CallTo(() => _fakeServiceProvider.GetService(typeof(WithdrawStrategy)))
                .Returns(withdrawStrategy);

            var result = _factory.GetStrategy(TransactionType.Withdrawal);

            Assert.IsNotNull(result);
            Assert.AreEqual(withdrawStrategy, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullServiceProvider_ThrowsArgumentNullException()
        {
            new TransactionStrategyFactory(null);
        }
    }
}
