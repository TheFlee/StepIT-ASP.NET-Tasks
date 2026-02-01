using _06._Web_API.DTOs.TaskItemDTOs;
using _06._Web_API.Models;
using FluentValidation;

namespace _06._Web_API.Validators;

public class CreateTaskItemValidator : AbstractValidator<CreateTaskItemRequest>
{
    public CreateTaskItemValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Task title is required.")
            .MinimumLength(3).WithMessage("Task title must at least 3 characters.");

        RuleFor(x => x.ProjectId)
            .NotEmpty().WithMessage("ProjectId is required.")
            .GreaterThan(0).WithMessage("ProjectId must be a positive integer.");

        RuleFor(x => x.Priority)
            .Must(p => new[] {TaskPriority.Low, TaskPriority.Medium, TaskPriority.High}.Contains(p))
            .WithMessage("Priority must be one of the defined enum values: 0(Low), 1(Medium), 2(High)");
    }
}
