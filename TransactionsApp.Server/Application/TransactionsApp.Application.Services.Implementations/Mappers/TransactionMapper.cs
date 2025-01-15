using TransactionsApp.Application.Models.Dto;
using TransactionsApp.Common.Services;
using TransactionsApp.Domain.Models.Entities;
using TransactionsApp.Domain.Models.Enums;

namespace TransactionsApp.Application.Services.Implementations.Mappers
{
    /// <summary>
    /// Maps <see cref="AddTransactionDto"/> into <see cref="Transaction"/>.
    /// </summary>
    public class TransactionMapper : IMapper<AddTransactionDto, Transaction>
    {
        /// <summary>
        /// Maps <see cref="AddTransactionDto"/> into <see cref="Transaction"/>.
        /// </summary>
        /// <param name="source">Object to map from.</param>
        /// <returns>Mapped user.</returns>
        public Transaction Map(AddTransactionDto source)
        {
            ArgumentNullException.ThrowIfNull(source);

            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                UserId = source.UserId,
                TransactionType = source.TransactionType,
                Amount = source.Amount,
                Date = DateTime.Now,
                AccountNumber = source.AccountNumber,
                Status = TransactionStatus.Pending,
                Deleted = false,
            };

            return transaction;
        }
    }
}
