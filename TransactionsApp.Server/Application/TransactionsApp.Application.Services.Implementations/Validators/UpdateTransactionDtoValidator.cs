using FluentValidation;
using TransactionsApp.Application.Models.Dto;

namespace TransactionsApp.Application.Services.Implementations.Validators
{
    /// <summary>
    /// Validator for the <see cref="UpdateTransactionDto"/> class.
    /// </summary>
    public class UpdateTransactionDtoValidator : AbstractValidator<UpdateTransactionDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTransactionDtoValidator"/> class.
        /// Defines validation rules for the <see cref="UpdateTransactionDto"/> class.
        /// </summary>
        public UpdateTransactionDtoValidator()
        {
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
