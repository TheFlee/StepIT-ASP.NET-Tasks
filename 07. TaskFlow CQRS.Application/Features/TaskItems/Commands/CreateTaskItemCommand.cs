using _07._TaskFlow_CQRS.Application.DTOs;
using MediatR;

namespace _07._TaskFlow_CQRS.Application.Features.TaskItems.Commands;

public record CreateTaskItemCommand(CreateTaskItemRequest Request): IRequest<TaskItemResponseDto>;
