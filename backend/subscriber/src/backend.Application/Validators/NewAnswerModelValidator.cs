using System.Diagnostics.CodeAnalysis;
using backend.Application.Models;
using FluentValidation;

namespace backend.Application.Validators;

[ExcludeFromCodeCoverage]
public class NewAnswerModelValidator : AbstractValidator<NewAnswerModel>
{
    public NewAnswerModelValidator()
    {
        RuleForOccupation();
        RuleForAge();
    }

    private void RuleForOccupation()
    {
        RuleFor(x => x.OccQuestion).NotNull().WithMessage("Occupation is required");
        RuleFor(x => x.OccQuestion).NotEmpty().WithMessage("Occupation is required");
    }

    private void RuleForAge()
    {
        RuleFor(x => x.AgeQuestion).NotNull().WithMessage("Age is required");
        RuleFor(x => x.AgeQuestion).InclusiveBetween(8, 110).WithMessage("Age must be between 8 and 110");
    }
}