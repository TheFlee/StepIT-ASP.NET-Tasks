using _07._TaskFlow_CQRS.Application.DTOs;
using _07._TaskFlow_CQRS.Application.Repositories;
using AutoMapper;
using MediatR;

namespace _07._TaskFlow_CQRS.Application.Features.TaskItems.Queries;

class GetTaskItemByIdQueryHandler : IRequestHandler<GetTaskItemByIdQuery, TaskItemResponseDto>
{
    private readonly ITaskItemRepository _repo;
    private readonly IMapper _mapper;

    public GetTaskItemByIdQueryHandler(ITaskItemRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<TaskItemResponseDto> Handle(GetTaskItemByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await _repo.GetByIdWithProjectAsync(request.Id);
        return task is null ? null! : _mapper.Map<TaskItemResponseDto>(task);
    }
}