using TransactionsApp.Domain.Models.Entities;

namespace TransactionsApp.Infrastructure.Implementations.Repositories
{
    /// <summary>
    /// Defines CRUD operations of User domain entity against a data source.
    /// </summary>
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(TransactionsAppDbContext context) : base(context)
        {
        }
    }
}
