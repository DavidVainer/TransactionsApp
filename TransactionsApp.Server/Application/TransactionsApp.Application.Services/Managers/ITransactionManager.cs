using TransactionsApp.Application.Models.Dto;
using TransactionsApp.Domain.Models.Entities;

namespace TransactionsApp.Application.Services.Managers
{
    /// <summary>
    /// Defines the operations for managing transactions.
    /// </summary>
    public interface ITransactionManager
    {
        /// <summary>
        /// Gets all transactions.
        /// </summary>
        /// <returns>A collection of all transactions.</returns>
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();

        /// <summary>
        /// Gets a specific transaction by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Transaction with the specified identifier.</returns>
        Task<Transaction> GetTransactionByIdAsync(Guid id);

        /// <summary>
        /// Creates a new transaction.
        /// </summary>
        /// <param name="transaction">Transaction to create.</param>
        Task CreateTransactionAsync(AddTransactionDto transaction);

        /// <summary>
        /// Updates an existing transaction.
        /// </summary>
        /// <param name="transaction">Transaction to update.</param>
        Task UpdateTransactionAsync(UpdateTransactionDto transaction);

        /// <summary>
        /// Deletes a transaction by its id.
        /// </summary>
        /// <param name="id">Unique identifier of the transaction to delete.</param>
        Task DeleteTransactionAsync(Guid id);
    }
}
