namespace TransactionsApp.Domain.Models.Entities
{
    /// <summary>
    /// Represents a user domain entity.
    /// </summary>
    public class User
    {
        /// <summary>
        /// User's unique data source identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// User's unique identifier (e.g., national ID).
        /// </summary>
        public string UserId { get; set; }

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
        /// Collection of transactions associated with the user.
        /// </summary>
        public ICollection<Transaction> Transactions { get; set; }
    }
}