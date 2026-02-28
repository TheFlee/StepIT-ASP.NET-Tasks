using _07._TaskFlow_CQRS.Application.Common;
using _07._TaskFlow_CQRS.Application.DTOs;
using MediatR;

namespace _07._TaskFlow_CQRS.Application.Features.TaskItems.Queries;

public record GetTaskItemsPagedQuery(TaskItemQueryParams queryParams): IRequest<PagedResult<TaskItemResponseDto>>;
