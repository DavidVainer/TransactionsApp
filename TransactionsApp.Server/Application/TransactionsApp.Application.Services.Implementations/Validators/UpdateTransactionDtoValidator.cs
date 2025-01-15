using FluentValidation;
using TransactionsApp.Application.Models.Dto;
using TransactionsApp.Application.Models.Settings;

namespace TransactionsApp.Application.Services.Implementations.Validators
{
    /// <summary>
    /// Validator for the <see cref="UpdateTransactionDto"/> class.
    /// </summary>
    public class UpdateTransactionDtoValidator : AbstractValidator<UpdateTransactionDto>
    {
        private const string TRANSACTION_ID_REQUIRED_MESSAGE = "Transaction ID is required.";
        private const string AMOUNT_REQUIRED_MESSAGE = "Amount is required.";
        private const string AMOUNT_GREATER_THAN_ZERO_MESSAGE = "Amount must be greater than zero.";
        private const string AMOUNT_UP_TO_10_DIGITS_MESSAGE = "Amount must be up to 10 digits.";
        private const string ACCOUNT_NUMBER_REQUIRED_MESSAGE = "Account Number is required.";
        private const string ACCOUNT_NUMBER_UP_TO_10_DIGITS_MESSAGE = "Account Number must be up to 10 digits.";

        private readonly IUpdateTransactionDtoValidatorSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTransactionDtoValidator"/> class.
        /// Defines validation rules for the <see cref="UpdateTransactionDto"/> class.
        /// </summary>
        public UpdateTransactionDtoValidator(IUpdateTransactionDtoValidatorSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));

            // Validates that the transaction ID is not empty.
            RuleFor(x => x.TransactionId)
                .NotEmpty().WithMessage(TRANSACTION_ID_REQUIRED_MESSAGE);

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
