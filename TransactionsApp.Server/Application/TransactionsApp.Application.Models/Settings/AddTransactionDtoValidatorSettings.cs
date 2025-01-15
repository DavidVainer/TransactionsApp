namespace TransactionsApp.Application.Models.Settings
{
    /// <summary>
    /// Represents a settings model required for the add transaction dto validator.
    /// </summary>
    public class AddTransactionDtoValidatorSettings : IAddTransactionDtoValidatorSettings
    {
        /// <summary>
        /// Regular expression pattern for the full name in Hebrew.
        /// </summary>
        public string FullNameHebrewPattern { get; set; }

        /// <summary>
        /// Regular expression pattern for the full name in English.
        /// </summary>
        public string FullNameEnglishPattern { get; set; }

        /// <summary>
        /// Maximum date of birth allowed.
        /// </summary>
        public DateTime DateOfBirthMaximum { get; set; }

        /// <summary>
        /// Regular expression pattern for the user identity.
        /// </summary>
        public string UserIdentityPattern { get; set; }

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
