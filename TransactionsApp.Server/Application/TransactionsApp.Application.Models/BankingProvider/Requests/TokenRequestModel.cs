namespace TransactionsApp.Application.Models.BankingProvider.Requests
{
    /// <summary>
    /// Represents the model for the token retrieval request.
    /// </summary>
    public class TokenRequestModel
    {
        /// <summary>
        /// User's unique identifer (e.g., national ID);
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Secret key for the request.
        /// </summary>
        public string SecretId { get; set; }
    }
}
