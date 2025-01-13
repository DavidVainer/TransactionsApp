namespace TransactionsApp.Application.Models.BankingProvider.Requests
{
    /// <summary>
    /// Represents the model for the transaction request.
    /// </summary>
    public class TransactionRequestModel
    {
        /// <summary>
        /// Transaction amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Bank account number of the user.
        /// </summary>
        public string Bank { get; set; }
    }
}
