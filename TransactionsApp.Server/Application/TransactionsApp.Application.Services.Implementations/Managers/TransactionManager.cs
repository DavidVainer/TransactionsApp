using TransactionsApp.Application.Models.BankingProvider.Responses;
using TransactionsApp.Application.Models.Dto;
using TransactionsApp.Application.Services.Managers;
using TransactionsApp.Application.Services.Repositories;
using TransactionsApp.Application.Services.Services;
using TransactionsApp.Domain.Models.Entities;
using TransactionsApp.Domain.Models.Enums;

namespace TransactionsApp.Application.Services.Implementations.Managers
{
    /// <summary>
    /// Defines the operations for managing transactions.
    /// </summary>
    public class TransactionManager : ITransactionManager
    {
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly IUserManager _userManager;
        private readonly IBankingProviderService _bankingProviderService;

        public TransactionManager(
            IRepository<Transaction> transactionRepository,
            IUserManager userManager,
            IBankingProviderService bankingProviderService)
        {
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _bankingProviderService = bankingProviderService ?? throw new ArgumentNullException(nameof(bankingProviderService));
        }

        /// <summary>
        /// Gets all transactions.
        /// </summary>
        /// <returns>A collection of all transactions.</returns>
        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            var transactions = await _transactionRepository.GetAllAsync();
            return transactions;
        }

        /// <summary>
        /// Gets a specific transaction by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Transaction with the specified identifier.</returns>
        public async Task<Transaction> GetTransactionByIdAsync(Guid id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            return transaction;
        }

        /// <summary>
        /// Creates a new transaction.
        /// </summary>
        /// <param name="dto">Data transfer object with data of a transaction to create.</param>
        public async Task CreateTransactionAsync(AddTransactionDto dto)
        {
            var tokenResponse = await _bankingProviderService.GenerateTokenAsync(dto);

            if (tokenResponse.Code != "Success")
            {
                throw new Exception("Failed to generate token.");
            }

            var responseModel = dto.TransactionType == TransactionType.Deposit ?
                await _bankingProviderService.DepositAsync(dto) :
                await _bankingProviderService.WithdrawAsync(dto);

            if (responseModel.Code != "Success")
            {
                throw new Exception("Failed to process transaction.");
            }

            var userId = await GetUserId(dto);

            var newTransaction = new Transaction
            {
                UserId = userId,
                TransactionType = dto.TransactionType,
                Amount = dto.Amount,
                Date = DateTime.Now,
                AccountNumber = dto.AccountNumber,
                Status = TransactionStatus.Pending,
            };

            await _transactionRepository.AddAsync(newTransaction);
        }

        /// <summary>
        /// Updates an existing transaction.
        /// </summary>
        /// <param name="dto">Data transfer object with data of a transaction to create.</param>
        public async Task UpdateTransactionAsync(UpdateTransactionDto dto)
        {
            var existingTransaction = await _transactionRepository.GetByIdAsync(dto.TransactionId);

            if (existingTransaction == null)
            {
                throw new Exception("Transaction not found.");
            }

            existingTransaction.Amount = dto.Amount;
            existingTransaction.AccountNumber = dto.AccountNumber;

            await _transactionRepository.UpdateAsync(existingTransaction);
        }

        /// <summary>
        /// Deletes a transaction by its id.
        /// </summary>
        /// <param name="id">Unique identifier of the transaction to delete.</param>
        public async Task DeleteTransactionAsync(Guid id)
        {
            await _transactionRepository.DeleteAsync(id);
        }

        /// <summary>
        /// Gets the unique identifier of a user who made the transaction.
        /// </summary>
        /// <param name="dto">Data transfer object.</param>
        /// <returns>Unique identifier of a user who made the transaction.</returns>
        private async Task<Guid> GetUserId(AddTransactionDto dto)
        {
            var existingUser = await _userManager.GetUserByIdentity(dto.UserIdentity);

            if (existingUser == null)
            {
                var newUser = await _userManager.CreateUserAsync(new AddUserDto
                {
                    FullNameHebrew = dto.FullNameHebrew,
                    FullNameEnglish = dto.FullNameEnglish,
                    DateOfBirth = dto.DateOfBirth,
                    UserIdentity = dto.UserIdentity
                });

                return newUser.Id;
            }
            else
            {
                return existingUser.Id;
            }

        }
    }
}
