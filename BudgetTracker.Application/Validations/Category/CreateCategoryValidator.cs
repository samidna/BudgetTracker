using BudgetTracker.Application.DTOs.Category;
using FluentValidation;

namespace BudgetTracker.Application.Validations.Category;
public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(50).WithMessage("Category name must not exceed 50 characters.");
    }
}
