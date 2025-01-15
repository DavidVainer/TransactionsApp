namespace TransactionsApp.Application.Models.Settings
{
    public class UpdateTransactionDtoValidatorSettings : IUpdateTransactionDtoValidatorSettings
    {
        /// <summary>
        /// Minimum value for the amount.
        /// </summary>
        public decimal AmountMinValue { get; set; }

        /// <summary>
        /// Maximum value for the amount.
        /// </summary>
        public decimal AmountMaxValue { get; set; }

        /// <summary>
        /// Regular expression pattern for the account number.
        /// </summary>
        public string AccountNumberPattern { get; set; }
    }
}
