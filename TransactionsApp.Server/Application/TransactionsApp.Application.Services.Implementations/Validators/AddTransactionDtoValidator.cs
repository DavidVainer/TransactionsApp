using FluentValidation;
using TransactionsApp.Application.Models.Dto;
using TransactionsApp.Application.Models.Settings;

namespace TransactionsApp.Application.Services.Implementations.Validators
{
    /// <summary>
    /// Validator for the <see cref="AddTransactionDto"/> class.
    /// </summary>
    public class AddTransactionDtoValidator : AbstractValidator<AddTransactionDto>
    {
        private const string FULL_NAME_HEBREW_REQUIRED_MESSAGE = "Full Name (Hebrew) is required.";
        private const string FULL_NAME_HEBREW_PATTERN_MESSAGE = "Full Name (Hebrew) must contain only Hebrew characters, up to 20.";
        private const string FULL_NAME_ENGLISH_REQUIRED_MESSAGE = "Full Name (English) is required.";
        private const string FULL_NAME_ENGLISH_PATTERN_MESSAGE = "Full Name (English) must contain only English characters, up to 15.";
        private const string BIRTH_DATE_REQUIRED_MESSAGE = "Birth Date is required.";
        private const string BIRTH_DATE_NOT_IN_FUTURE_MESSAGE = "Birth Date cannot be in the future.";
        private const string USER_IDENTITY_REQUIRED_MESSAGE = "Identity is required.";
        private const string USER_IDENTITY_PATTERN_MESSAGE = "Identity must be exactly 9 digits.";
        private const string AMOUNT_REQUIRED_MESSAGE = "Amount is required.";
        private const string AMOUNT_GREATER_THAN_ZERO_MESSAGE = "Amount must be greater than zero.";
        private const string AMOUNT_UP_TO_10_DIGITS_MESSAGE = "Amount must be up to 10 digits.";
        private const string ACCOUNT_NUMBER_REQUIRED_MESSAGE = "Account Number is required.";
        private const string ACCOUNT_NUMBER_UP_TO_10_DIGITS_MESSAGE = "Account Number must be up to 10 digits.";

        private readonly IAddTransactionDtoValidatorSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddTransactionDtoValidator"/> class.
        /// Defines validation rules for the <see cref="AddTransactionDto"/> class.
        /// </summary>
        public AddTransactionDtoValidator(IAddTransactionDtoValidatorSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));

            // Validates that the Hebrew full name is not empty and contains only Hebrew characters, up to 20 characters.
            RuleFor(x => x.FullNameHebrew)
                .NotEmpty().WithMessage(FULL_NAME_HEBREW_REQUIRED_MESSAGE)
                .Matches(_settings.FullNameHebrewPattern).WithMessage(FULL_NAME_HEBREW_PATTERN_MESSAGE);

            // Validates that the English full name is not empty and contains only English characters, up to 15 characters.
            RuleFor(x => x.FullNameEnglish)
                .NotEmpty().WithMessage(FULL_NAME_ENGLISH_REQUIRED_MESSAGE)
                .Matches(_settings.FullNameEnglishPattern).WithMessage(FULL_NAME_ENGLISH_PATTERN_MESSAGE);

            // Validates that the date of birth is not empty and is not in the future.
            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage(BIRTH_DATE_REQUIRED_MESSAGE)
                .LessThanOrEqualTo(_settings.DateOfBirthMaximum).WithMessage(BIRTH_DATE_NOT_IN_FUTURE_MESSAGE);

            // Validates that the user identity is not empty and is exactly 9 digits.
            RuleFor(x => x.UserIdentity)
                .NotEmpty().WithMessage(USER_IDENTITY_REQUIRED_MESSAGE)
                .Matches(_settings.UserIdentityPattern).WithMessage(USER_IDENTITY_PATTERN_MESSAGE);

            // Validates that the amount is not empty, greater than zero, and is up to 10 digits.
            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage(AMOUNT_REQUIRED_MESSAGE)
                .GreaterThan(_settings.AmountMinValue).WithMessage(AMOUNT_GREATER_THAN_ZERO_MESSAGE)
                .LessThan(_settings.AmountMaxValue).WithMessage(AMOUNT_UP_TO_10_DIGITS_MESSAGE);

            // Validates that the account number is not empty and contains up to 10 digits.
            RuleFor(x => x.AccountNumber)
                .NotEmpty().WithMessage(ACCOUNT_NUMBER_REQUIRED_MESSAGE)
                .Matches(_settings.AccountNumberPattern).WithMessage(ACCOUNT_NUMBER_UP_TO_10_DIGITS_MESSAGE);
        }
    }
}
