using FluentValidation;
using TransactionsApp.Application.Models.Dto;

namespace TransactionsApp.Application.Services.Implementations.Validators
{
    /// <summary>
    /// Validator for the <see cref="AddTransactionDto"/> class.
    /// </summary>
    public class AddTransactionDtoValidator : AbstractValidator<AddTransactionDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddTransactionDtoValidator"/> class.
        /// Defines validation rules for the <see cref="AddTransactionDto"/> class.
        /// </summary>
        public AddTransactionDtoValidator()
        {
            // Validates that the Hebrew full name is not empty and contains only Hebrew characters, up to 20 characters.
            RuleFor(x => x.FullNameHebrew)
                .NotEmpty().WithMessage("Full Name (Hebrew) is required.")
                .Matches(@"^[א-ת'\- ]{1,20}$").WithMessage("Full Name (Hebrew) must contain only Hebrew characters, up to 20.");

            // Validates that the English full name is not empty and contains only English characters, up to 15 characters.
            RuleFor(x => x.FullNameEnglish)
                .NotEmpty().WithMessage("Full Name (English) is required.")
                .Matches(@"^[A-Za-z'\- ]{1,15}$").WithMessage("Full Name (English) must contain only English characters, up to 15.");

            // Validates that the date of birth is not empty and is not in the future.
            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Birth Date is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Birth Date cannot be in the future.");

            // Validates that the user identity is not empty and is exactly 9 digits.
            RuleFor(x => x.UserIdentity)
                .NotEmpty().WithMessage("Identity is required.")
                .Matches(@"^\d{9}$").WithMessage("Identity must be exactly 9 digits.");

            // Validates that the amount is not empty, greater than zero, and is up to 10 digits.
            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Amount is required.")
                .GreaterThan(0).WithMessage("Amount must be greater than zero.")
                .LessThan(10000000000).WithMessage("Amount must be up to 10 digits.");

            // Validates that the account number is not empty and contains up to 10 digits.
            RuleFor(x => x.AccountNumber)
                .NotEmpty().WithMessage("Account Number is required.")
                .Matches(@"^\d{1,10}$").WithMessage("Account Number must be up to 10 digits.");
        }
    }
}
