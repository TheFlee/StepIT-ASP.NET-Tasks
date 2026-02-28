using _07._TaskFlow_CQRS.Application.DTOs;
using _07._TaskFlow_CQRS.Application.Repositories;
using AutoMapper;
using MediatR;

namespace _07._TaskFlow_CQRS.Application.Features.TaskItems.Commands;

class UpdateTaskItemCommandHandler : IRequestHandler<UpdateTaskItemCommand, TaskItemResponseDto?>
{
    private readonly ITaskItemRepository _taskRepo;
    private readonly IMapper _mapper;

    public UpdateTaskItemCommandHandler(
        ITaskItemRepository taskRepo,
        IMapper mapper)
    {
        _taskRepo = taskRepo;
        _mapper = mapper;
    }

    public async Task<TaskItemResponseDto?> Handle(UpdateTaskItemCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepo.FindAsync(request.Id);
        if (task == null) return null;
        _mapper.Map(request.Request, task);
        await _taskRepo.UpdateAsync(task);
        return _mapper.Map<TaskItemResponseDto>(task);
    }
}