namespace TransactionsApp.Application.Models.BankingProvider.Responses
{
    public class TransactionResponseModel
    {
        /// <summary>
        /// Request's success or failure code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Transaction status.
        /// </summary>
        public string Data { get; set; }
    }
}
