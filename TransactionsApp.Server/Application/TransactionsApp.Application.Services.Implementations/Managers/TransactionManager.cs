using TransactionsApp.Application.Models.Dto;
using TransactionsApp.Application.Services.Managers;
using TransactionsApp.Application.Services.Repositories;
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
        private readonly IRepository<User> _userRepository;

        public TransactionManager(
            IRepository<Transaction> transactionRepository,
            IRepository<User> userRepository)
        {
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
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
        /// <param name="transaction">Transaction to create.</param>
        public async Task CreateTransactionAsync(AddTransactionDto transaction)
        {
            var userId = await GetUserId(transaction);

            var newTransaction = new Transaction
            {
                UserId = userId,
                TransactionType = transaction.TransactionType,
                Amount = transaction.Amount,
                Date = DateTime.UtcNow,
                AccountNumber = transaction.AccountNumber,
                Status = TransactionStatus.Pending,
            };

            await _transactionRepository.AddAsync(newTransaction);
        }

        /// <summary>
        /// Updates an existing transaction.
        /// </summary>
        /// <param name="transaction">Transaction to update.</param>
        public async Task UpdateTransactionAsync(UpdateTransactionDto transaction)
        {
            var existingTransaction = await _transactionRepository.GetByIdAsync(transaction.TransactionId);

            if (existingTransaction == null)
            {
                throw new Exception("Transaction not found.");
            }

            existingTransaction.Amount = transaction.Amount;
            existingTransaction.AccountNumber = transaction.AccountNumber;

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

        private async Task<Guid> GetUserId(AddTransactionDto dto)
        {
            User existingUser = _userRepository.FindAsync(x => x.UserId == dto.UserId).Result.FirstOrDefault();

            if (existingUser == null)
            {
                var newUser = await CreateUser(dto);

                return newUser.Id;
            }
            else
            {
                return existingUser.Id;
            }
        }

        private async Task<User> CreateUser(AddTransactionDto dto)
        {
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                UserId = dto.UserId,
                FullNameHebrew = dto.FullNameHebrew,
                FullNameEnglish = dto.FullNameEnglish,
                DateOfBirth = dto.DateOfBirth
            };

            await _userRepository.AddAsync(newUser);

            return newUser;
        }
    }
}
