using _06._Web_API.Data;
using _06._Web_API.Models;
using _06._Web_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace _06._Web_API.Services;

public class TaskItemService : ITaskItemService
{
    private readonly TaskFlowDbContext _context;

    public TaskItemService(TaskFlowDbContext context)
    {
        _context = context;
    }

    public async Task<TaskItem> CreateAsync(TaskItem task)
    {
        task.CreatedAt = DateTimeOffset.UtcNow;
        task.UpdatedAt = null;
        _context.TaskItems.Add(task);
        await _context.SaveChangesAsync();

        await _context.Entry(task).Reference(t => t.Project).LoadAsync();
        return task;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _context.TaskItems.FindAsync(id);

        if (task is null) return false;
        _context.TaskItems.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        return await _context.TaskItems.Include(t => t.Project).ToListAsync();

    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        return await _context.TaskItems.Include(t => t.Project)
                                       .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TaskItem?> GetByProjectIdAsync(int projectId)
    {
        return await _context.TaskItems.Include(t => t.Project)
                                       .FirstOrDefaultAsync(t => t.ProjectId == projectId);
    }

    public async Task<TaskItem?> UpdateAsync(int id, TaskItem taskItem)
    {
        var updatedTaskItem = await _context.TaskItems.Include(t => t.Project)
                                                      .FirstOrDefaultAsync(t => t.Id == id);
        if (taskItem is null) return null;

        updatedTaskItem!.Title = taskItem.Title;
        updatedTaskItem.Description = taskItem.Description;
        updatedTaskItem.Status = taskItem.Status;
        updatedTaskItem.UpdatedAt = DateTimeOffset.UtcNow;
        await _context.SaveChangesAsync();
        return updatedTaskItem;

    }
}
