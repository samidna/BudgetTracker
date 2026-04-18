using BudgetTracker.Application.DTOs.Transaction;
using FluentValidation;

namespace BudgetTracker.Application.Validations.Transaction;
public class CreateTransactionValidator : AbstractValidator<CreateTransactionDto>
{
    public CreateTransactionValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title must not be null")
            .MaximumLength(100).WithMessage("Title must be less than 100 characters");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be more than 0");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category must be choosen");
    }
}
