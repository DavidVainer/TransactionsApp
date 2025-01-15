namespace TransactionsApp.Application.Models.BankingProvider.Responses
{
    /// <summary>
    /// Represents the model for a banking provider request response.
    /// </summary>
    public class BankingResponseModel
    {
        /// <summary>
        /// Request's success or failure code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Data returned by the banking provider.
        /// </summary>
        public string Data { get; set; }
    }
}
