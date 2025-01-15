using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using TransactionsApp.Domain.Models.Entities;

namespace TransactionsApp.Infrastructure.Implementations.Repositories
{
    /// <summary>
    /// Defines CRUD operations of Transaction domain entity against a data source.
    /// </summary>
    public class TransactionRepository : BaseRepository<Transaction>
    {
        public TransactionRepository(TransactionsAppDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets all records from a data source.
        /// </summary>
        /// <returns>Collection of all records.</returns>
        public override async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            var records = await _context.Transactions
                .Include(t => t.User)
                .Where(t => !t.Deleted)
                .ToListAsync();

            return records;
        }

        /// <summary>
        /// Deletes a record from a data source.
        /// </summary>
        /// <param name="id">Record's unique identifier</param>
        public override async Task DeleteAsync(Guid id)
        {
            var record = await GetByIdAsync(id);

            record.Deleted = true;

            _context.Update(record);

            await _context.SaveChangesAsync();
        }
    }
}
