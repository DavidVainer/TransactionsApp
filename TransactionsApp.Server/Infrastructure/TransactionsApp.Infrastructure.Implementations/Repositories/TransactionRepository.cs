using Microsoft.EntityFrameworkCore;
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
            var records = await _context.Transactions.Include(t => t.User).ToListAsync();
            return records;
        }
    }
}
