using System.Linq.Expressions;

namespace TransactionsApp.Application.Services.Repositories
{
    /// <summary>
    /// Defines CRUD operations against a data source.
    /// </summary>
    /// <typeparam name="T">The type of repository entity.</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Gets all records from a data source.
        /// </summary>
        /// <returns>Collection of all records.</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Gets a record by its unique identifier.
        /// </summary>
        /// <param name="id">Record's unique identifier</param>
        /// <returns>Record with the specified identifier.</returns>
        Task<T> GetByIdAsync(Guid id);

        /// <summary>
        /// Gets records that match a specified filter.
        /// </summary>
        /// <param name="filter">Filter to get records by.</param>
        /// <returns>Collection of records match the filter.</returns>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Adds a new record to a data source.
        /// </summary>
        /// <param name="entity">Entity model to add.</param>
        Task AddAsync(T entity);

        /// <summary>
        /// Updates an existing record in a data source.
        /// </summary>
        /// <param name="entity">Entity model to update.</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Deletes a record from a data source.
        /// </summary>
        /// <param name="id">Record's unique identifier</param>
        Task DeleteAsync(Guid id);
    }
}
