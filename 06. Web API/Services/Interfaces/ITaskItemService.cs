using _06._Web_API.Models;

namespace _06._Web_API.Services.Interfaces;

public interface ITaskItemService
{
    Task<IEnumerable<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(int id);
    Task<TaskItem?> GetByProjectIdAsync(int projectId);
    Task<TaskItem> CreateAsync(TaskItem taskItem);
    Task<TaskItem?> UpdateAsync(int id, TaskItem taskItem);
    Task<bool> DeleteAsync(int id);
}
