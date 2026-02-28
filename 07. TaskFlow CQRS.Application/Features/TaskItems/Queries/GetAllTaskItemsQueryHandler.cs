using _07._TaskFlow_CQRS.Application.DTOs;
using _07._TaskFlow_CQRS.Application.Repositories;
using AutoMapper;
using MediatR;

namespace _07._TaskFlow_CQRS.Application.Features.TaskItems.Queries;

class GetAllTaskItemsQueryHandler : IRequestHandler<GetAllTaskItemsQuery, IEnumerable<TaskItemResponseDto>>
{
    private readonly ITaskItemRepository _repo;
    private readonly IMapper _mapper;

    public GetAllTaskItemsQueryHandler(ITaskItemRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaskItemResponseDto>> Handle(GetAllTaskItemsQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _repo.GetAllWithProjectAsync();
        return _mapper.Map<IEnumerable<TaskItemResponseDto>>(tasks);
    }
}