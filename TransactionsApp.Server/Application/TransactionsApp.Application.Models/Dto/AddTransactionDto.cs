using TransactionsApp.Domain.Models.Enums;

namespace TransactionsApp.Application.Models.Dto
{
    /// <summary>
    /// Data transfer object for adding a new transaction.
    /// </summary>
    public class AddTransactionDto
    {
        /// <summary>
        /// Full name of the user in Hebrew.
        /// </summary>
        public string FullNameHebrew { get; set; }

        /// <summary>
        /// Full name of the user in English.
        /// </summary>
        public string FullNameEnglish { get; set; }

        /// <summary>
        /// User's date of birth.
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// User's unique identity (e.g., Teudat Zehut, National ID).
        /// </summary>
        public string UserIdentity { get; set; }

        /// <summary>
        /// Unique data source identifier of the user.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Transaction type.
        /// </summary>
        public TransactionType TransactionType { get; set; }

        /// <summary>
        /// Amount of money used in the transaction.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Bank account number associated with the transaction.
        /// </summary>
        public string AccountNumber { get; set; }
    }
}
