namespace TransactionsApp.Infrastructure.Models.BankingProviderSettings
{
    /// <summary>
    /// Settings required for interacting with a banking provider.
    /// </summary>
    public interface IBankingProviderSettings
    {
        /// <summary>
        /// URL for creating a token against the banking provider.
        /// </summary>
        string CreateTokenUrl { get; set; }

        /// <summary>
        /// Secret id required for creating a token against the banking provider.
        /// </summary>
        string CreateTokenSecretId { get; set; }

        /// <summary>
        /// URL for depositing funds in the banking provider.
        /// </summary>
        string DepositUrl { get; set; }

        /// <summary>
        /// URL for withdrawing funds in the banking provider.
        /// </summary>
        string WithdrawUrl { get; set; }
    }
}
