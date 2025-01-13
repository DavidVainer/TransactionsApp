namespace TransactionsApp.Domain.Models.Enums
{
    /// <summary>
    /// Represents the status of a transaction.
    /// </summary>
    public enum TransactionStatus
    {
        Pending = 0,
        Completed = 1,
        Failed = 2,
        Canceled = 3,
    }
}
