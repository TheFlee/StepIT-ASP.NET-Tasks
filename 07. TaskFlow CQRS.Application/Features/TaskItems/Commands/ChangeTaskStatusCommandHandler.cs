using _07._TaskFlow_CQRS.Application.DTOs;
using _07._TaskFlow_CQRS.Application.Repositories;
using AutoMapper;
using MediatR;

namespace _07._TaskFlow_CQRS.Application.Features.TaskItems.Commands;

class ChangeTaskStatusCommandHandler : IRequestHandler<ChangeTaskStatusCommand, TaskItemResponseDto?>
{
    private readonly ITaskItemRepository _taskRepo;
    private readonly IMapper _mapper;

    public ChangeTaskStatusCommandHandler(
        ITaskItemRepository taskRepo,
        IMapper mapper)
    {
        _taskRepo = taskRepo;
        _mapper = mapper;
    }

    public async Task<TaskItemResponseDto?> Handle(ChangeTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepo.FindAsync(request.TaskId);
        if (task == null) return null;
        task.Status = request.status;
        await _taskRepo.UpdateAsync(task);
        return _mapper.Map<TaskItemResponseDto>(task);
    }
}