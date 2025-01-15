namespace TransactionsApp.Application.Models.Settings
{
    /// <summary>
    /// Represents a settings model required for the update transaction dto validator.
    /// </summary>
    public interface IUpdateTransactionDtoValidatorSettings
    {
        /// <summary>
        /// Minimum value for the amount.
        /// </summary>
        decimal AmountMinValue { get; set; }

        /// <summary>
        /// Maximum value for the amount.
        /// </summary>
        decimal AmountMaxValue { get; set; }

        /// <summary>
        /// Regular expression pattern for the account number.
        /// </summary>
        string AccountNumberPattern { get; set; }
    }
}
