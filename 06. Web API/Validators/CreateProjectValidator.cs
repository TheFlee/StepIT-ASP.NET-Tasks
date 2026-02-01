using _06._Web_API.DTOs.ProjectDTOs;
using FluentValidation;

namespace _06._Web_API.Validators;

public class CreateProjectValidator : AbstractValidator<CreateProjectRequest>
{
    public CreateProjectValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Project name is required.")
            .MinimumLength(3).WithMessage("Project name must at least 3 characters.");
    }
}
