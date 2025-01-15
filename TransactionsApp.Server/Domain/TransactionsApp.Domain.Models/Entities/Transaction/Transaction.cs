using TransactionsApp.Domain.Models.Enums;

namespace TransactionsApp.Domain.Models.Entities
{
    /// <summary>
    /// Represents a transaction domain entity.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Transaction's unique identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Unique identifier of the user associated with the transaction.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Type of the transaction.
        /// </summary>
        public TransactionType TransactionType { get; set; }

        /// <summary>
        /// Amount of money used in the transaction.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// The date the transaction was made.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Bank account number associated with the transaction.
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Completion status of the transaction.
        /// </summary>
        public TransactionStatus Status { get; set; }

        /// <summary>
        /// Indicates whether the user is deleted.
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// User entity associated with the transaction.
        /// </summary>
        public User User { get; set; }
    }
}
