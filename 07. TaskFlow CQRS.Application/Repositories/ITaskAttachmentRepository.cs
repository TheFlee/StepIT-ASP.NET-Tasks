using _07._TaskFlow_CQRS.Domain;

namespace _07._TaskFlow_CQRS.Application.Repositories;

public interface ITaskAttachmentRepository
{
    Task<TaskAttachment?> GetByIdAsync(int id);
    Task<TaskAttachment?> GetByIdWithTaskItemAsync(int id);
    Task<TaskAttachment> AddAsync(TaskAttachment attachment);
    Task UpdateAsync(TaskAttachment attachment);
    Task RemoveAsync(TaskAttachment attachment);
}
