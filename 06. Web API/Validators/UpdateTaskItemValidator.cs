using _06._Web_API.DTOs.TaskItemDTOs;
using _06._Web_API.Models;
using FluentValidation;

namespace _06._Web_API.Validators;

public class UpdateTaskItemValidator : AbstractValidator<UpdateTaskItemRequest>
{
    public UpdateTaskItemValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Task title is required.")
            .MinimumLength(3).WithMessage("Task title must at least 3 characters.");
        RuleFor(x => x.Priority)
            .Must(p => new[] { TaskPriority.Low, TaskPriority.Medium, TaskPriority.High }.Contains(p))
            .WithMessage("Priority must be one of the defined enum values: 0(Low), 1(Medium), 2(High)");
        RuleFor(x => x.Status)
            .Must(s => new[] { Models.TaskStatus.ToDo, Models.TaskStatus.InProgress, Models.TaskStatus.Done }.Contains(s))
            .WithMessage("Status must be one of the defined enum values: 0(ToDo), 1(InProgress), 2(Done)");
    }
}
