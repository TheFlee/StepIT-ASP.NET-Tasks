using _07._TaskFlow_CQRS.Application.DTOs;
using _07._TaskFlow_CQRS.Application.Repositories;
using AutoMapper;
using MediatR;

namespace _07._TaskFlow_CQRS.Application.Features.TaskItems.Queries;

class GetTaskItemsByProjectQueryHandler : IRequestHandler<GetTaskItemsByProjectQuery, IEnumerable<TaskItemResponseDto>>
{
    private readonly ITaskItemRepository _repo;
    private readonly IMapper _mapper;

    public GetTaskItemsByProjectQueryHandler(ITaskItemRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaskItemResponseDto>> Handle(GetTaskItemsByProjectQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _repo.GetByProjectIdAsync(request.ProjectId);
        return _mapper.Map<IEnumerable<TaskItemResponseDto>>(tasks);
    }
}
