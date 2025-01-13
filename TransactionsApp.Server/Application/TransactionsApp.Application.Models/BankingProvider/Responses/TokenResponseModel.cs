namespace TransactionsApp.Application.Models.BankingProvider.Responses
{
    /// <summary>
    /// Represents the model for the token retrieval response.
    /// </summary>
    public class TokenResponseModel
    {
        /// <summary>
        /// Request's success or failure code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Generated token.
        /// </summary>
        public string Data { get; set; }
    }
}
