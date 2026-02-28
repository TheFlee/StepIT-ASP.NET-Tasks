using _07._TaskFlow_CQRS.Application.Repositories;
using MediatR;

namespace _07._TaskFlow_CQRS.Application.Features.TaskItems.Commands;

class DeleteTaskItemCommandHandler : IRequestHandler<DeleteTaskItemCommand, bool>
{
    private readonly ITaskItemRepository _taskRepo;

    public DeleteTaskItemCommandHandler(ITaskItemRepository taskRepo)
    {
        _taskRepo = taskRepo;
    }

    public async Task<bool> Handle(DeleteTaskItemCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepo.FindAsync(request.Id);
        if (task == null) return false;
        await _taskRepo.RemoveAsync(task);
        return true;
    }
}
