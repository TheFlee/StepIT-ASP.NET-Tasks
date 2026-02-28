using _07._TaskFlow_CQRS.Application.DTOs;
using _07._TaskFlow_CQRS.Application.Repositories;
using _07._TaskFlow_CQRS.Domain;
using AutoMapper;
using MediatR;

namespace _07._TaskFlow_CQRS.Application.Features.TaskItems.Commands;

class CreateTaskItemCommandHandler : IRequestHandler<CreateTaskItemCommand, TaskItemResponseDto>
{
    private readonly ITaskItemRepository _taskRepo;
    private readonly IMapper _mapper;

    public CreateTaskItemCommandHandler(
        ITaskItemRepository taskRepo,
        IMapper mapper)
    {
        _taskRepo = taskRepo;
        _mapper = mapper;
    }

    public async Task<TaskItemResponseDto> Handle(CreateTaskItemCommand request, CancellationToken cancellationToken)
    {
        var task = _mapper.Map<TaskItem>(request.Request);
        task = await _taskRepo.AddAsync(task);
        return _mapper.Map<TaskItemResponseDto>(task);
    }
}