using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TransactionsApp.Application.Services.Repositories;

namespace TransactionsApp.Infrastructure.Implementations.Repositories
{
    /// <summary>
    /// Base CRUD operations against a data source.
    /// </summary>
    /// <typeparam name="T">The type of repository entity.</typeparam>
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly TransactionsAppDbContext _context;

        public BaseRepository(TransactionsAppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Gets all records from a data source.
        /// </summary>
        /// <returns>Collection of all records.</returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var records = await _context.Set<T>().ToListAsync();
            return records;
        }

        /// <summary>
        /// Gets a record by its unique identifier.
        /// </summary>
        /// <param name="id">Record's unique identifier</param>
        /// <returns>Record with the specified identifier.</returns>
        public async Task<T> GetByIdAsync(Guid id)
        {
            var record = await _context.Set<T>().FindAsync(id);
            return record;
        }

        /// <summary>
        /// Gets records that match a specified filter.
        /// </summary>
        /// <param name="filter">Filter to get records by.</param>
        /// <returns>Collection of records match the filter.</returns>
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filter)
        {
            var record = await _context.Set<T>().Where(filter).ToListAsync();
            return record;
        }

        /// <summary>
        /// Adds a new record to a data source.
        /// </summary>
        /// <param name="entity">Entity model to add.</param>
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing record in a data source.
        /// </summary>
        /// <param name="entity">Entity model to update.</param>
        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a record from a data source.
        /// </summary>
        /// <param name="id">Record's unique identifier</param>
        public async Task DeleteAsync(Guid id)
        {
            var record = await GetByIdAsync(id);

            _context.Set<T>().Remove(record);

            await _context.SaveChangesAsync();
        }
    }
}
