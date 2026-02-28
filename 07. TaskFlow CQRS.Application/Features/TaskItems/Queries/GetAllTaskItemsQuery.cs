using _07._TaskFlow_CQRS.Application.DTOs;
using MediatR;

namespace _07._TaskFlow_CQRS.Application.Features.TaskItems.Queries;

public record GetAllTaskItemsQuery(): IRequest<IEnumerable<TaskItemResponseDto>>;