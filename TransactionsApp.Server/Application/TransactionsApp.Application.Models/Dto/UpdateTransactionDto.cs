namespace TransactionsApp.Application.Models.Dto
{
    /// <summary>
    /// Data transfer object for updating an existing transaction.
    /// </summary>
    public class UpdateTransactionDto
    {
        public Guid TransactionId { get; set; }

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
