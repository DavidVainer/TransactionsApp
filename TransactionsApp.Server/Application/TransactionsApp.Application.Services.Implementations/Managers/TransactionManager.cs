using TransactionsApp.Application.Models.Dto;
using TransactionsApp.Application.Services.Factories;
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
        private readonly ITransactionStrategyFactory _transactionStrategyFactory;

        public TransactionManager(
            IRepository<Transaction> transactionRepository,
            IUserManager userManager,
            IBankingProviderService bankingProviderService,
            ITransactionStrategyFactory transactionStrategyFactory)
        {
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _bankingProviderService = bankingProviderService ?? throw new ArgumentNullException(nameof(bankingProviderService));
            _transactionStrategyFactory = transactionStrategyFactory ?? throw new ArgumentNullException(nameof(transactionStrategyFactory));
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
        /// Processes a new transaction.
        /// </summary>
        /// <param name="dto">Data transfer object with data of a transaction to process.</param>
        public async Task ProcessTransactionAsync(AddTransactionDto dto)
        {
            var userId = await GetUserId(dto);
            var transaction = await InitializeTransactionAsync(dto, userId);

            try
            {
                await ValidateTransactionAsync(dto);
                await ExecuteTransactionAsync(dto);

                transaction.Status = TransactionStatus.Completed;
            }
            catch (Exception ex)
            {
                transaction.Status = TransactionStatus.Failed;
            }
            finally
            {
                await _transactionRepository.UpdateAsync(transaction);
            }
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
        /// Initializes a new transaction and saves it to the database.
        /// </summary>
        /// <param name="dto">Data transfer object.</param>
        /// <param name="userId">User unique identifier.</param>
        /// <returns>Created transaction.</returns>
        private async Task<Transaction> InitializeTransactionAsync(AddTransactionDto dto, Guid userId)
        {
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                TransactionType = dto.TransactionType,
                Amount = dto.Amount,
                Date = DateTime.Now,
                AccountNumber = dto.AccountNumber,
                Status = TransactionStatus.Pending,
                Deleted = false,
            };

            return await _transactionRepository.AddAsync(transaction);
        }

        /// <summary>
        /// Validates the transaction by generating a token.
        /// <param name="dto">Data transfer object.</param>
        /// </summary>
        private async Task ValidateTransactionAsync(AddTransactionDto dto)
        {
            var tokenResponse = await _bankingProviderService.GenerateTokenAsync(dto);

            if (tokenResponse.Code != "Success")
            {
                throw new Exception("Failed to generate token.");
            }
        }

        /// <summary>
        /// Executes the transaction.
        /// </summary>
        /// <param name="dto">Data transfer object.</param>
        private async Task ExecuteTransactionAsync(AddTransactionDto dto)
        {
            var transactionStrategy = _transactionStrategyFactory.GetStrategy(dto.TransactionType);
            var responseModel = await transactionStrategy.ProcessTransactionAsync(dto);

            if (responseModel.Code != "Success")
            {
                throw new Exception("Failed to process transaction.");
            }
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
