namespace TransactionsApp.Domain.Models.Enums
{
    /// <summary>
    /// Represents the status of a transaction.
    /// </summary>
    public enum TransactionStatus
    {
        Pending,
        Completed,
        Failed,
        Canceled
    }
}
