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
    }
}
