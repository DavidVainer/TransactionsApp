namespace TransactionsApp.Application.Models.Settings
{
    /// <summary>
    /// Represents a settings model required for the add transaction dto validator.
    /// </summary>
    public interface IAddTransactionDtoValidatorSettings : IUpdateTransactionDtoValidatorSettings
    {
        /// <summary>
        /// Regular expression pattern for the full name in Hebrew.
        /// </summary>
        string FullNameHebrewPattern { get; set; }

        /// <summary>
        /// Regular expression pattern for the full name in English.
        /// </summary>
        string FullNameEnglishPattern { get; set; }

        /// <summary>
        /// Maximum date of birth allowed.
        /// </summary>
        DateTime DateOfBirthMaximum { get; set; }

        /// <summary>
        /// Regular expression pattern for the user identity.
        /// </summary>
        string UserIdentityPattern { get; set; }
    }
}
