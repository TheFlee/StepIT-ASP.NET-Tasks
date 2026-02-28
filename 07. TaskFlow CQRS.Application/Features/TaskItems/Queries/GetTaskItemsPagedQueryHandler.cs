using _07._TaskFlow_CQRS.Application.Common;
using _07._TaskFlow_CQRS.Application.DTOs;
using _07._TaskFlow_CQRS.Application.Repositories;
using AutoMapper;
using MediatR;

namespace _07._TaskFlow_CQRS.Application.Features.TaskItems.Queries;

class GetTaskItemsPagedQueryHandler : IRequestHandler<GetTaskItemsPagedQuery, PagedResult<TaskItemResponseDto>>
{
    private readonly ITaskItemRepository _repo;
    private readonly IMapper _mapper;

    public GetTaskItemsPagedQueryHandler(ITaskItemRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<PagedResult<TaskItemResponseDto>> Handle(GetTaskItemsPagedQuery request, CancellationToken cancellationToken)
    {
        var qp = request.queryParams;

        var (items, total) = await _repo.GetPagedAsync(
            qp.ProjectId,
            qp.Status,
            qp.Priority,
            qp.Search,
            qp.Sort,
            qp.SortDirection,
            qp.Page,
            qp.PageSize
        );

        return new PagedResult<TaskItemResponseDto>
        {
            Items = _mapper.Map<IEnumerable<TaskItemResponseDto>>(items),
            TotalCount = total
        };
    }
}
