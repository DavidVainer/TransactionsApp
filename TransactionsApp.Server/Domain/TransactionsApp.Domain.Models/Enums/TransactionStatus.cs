namespace TransactionsApp.Domain.Models.Enums
{
    /// <summary>
    /// Represents the status of a transaction.
    /// </summary>
    public enum TransactionStatus
    {
        Pending = 1,
        Completed = 2,
        Failed = 3,
    }
}
