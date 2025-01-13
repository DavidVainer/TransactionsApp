namespace TransactionsApp.Infrastructure.Models.BankingProviderSettings
{
    /// <summary>
    /// Settings required for interacting with a banking provider.
    /// </summary>
    public class BankingProviderSettings : IBankingProviderSettings
    {
        /// <summary>
        /// URL for creating a token against the banking provider.
        /// </summary>
        public string CreateTokenUrl { get; set; }

        /// <summary>
        /// Secret id required for creating a token against the banking provider.
        /// </summary>
        public string CreateTokenSecretId { get; set; }

        /// <summary>
        /// URL for depositing funds in the banking provider.
        /// </summary>
        public string DepositUrl { get; set; }

        /// <summary>
        /// URL for withdrawing funds in the banking provider.
        /// </summary>
        public string WithdrawUrl { get; set; }
    }
}
