using _06._Web_API.Data;
using _06._Web_API.DTOs.TaskItemDTOs;
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
    public async Task<TaskItemResponseDTO> CreateAsync(CreateTaskItemRequest task)
    {
        var isProjectExists = await _context.Projects.AnyAsync(p => p.Id == task.ProjectId);
        if (!isProjectExists)
        {
            throw new ArgumentException($"Project with ID {task.ProjectId} does not exist.");
        }

        var taskItem = new TaskItem
        {
            Title = task.Title,
            Description = task.Description,
            Status = Models.TaskStatus.ToDo,
            ProjectId = task.ProjectId,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = null!
        };
        _context.TaskItems.Add(taskItem);
        await _context.SaveChangesAsync();

        await _context.Entry(taskItem).Reference(t => t.Project).LoadAsync();

        return MapToResponseDTO(taskItem);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _context.TaskItems.FindAsync(id);

        if (task is null) return false;
        _context.TaskItems.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<TaskItemResponseDTO>> GetAllAsync()
    {
        var taskItems = await _context.TaskItems.Include(t => t.Project).ToListAsync();
        return taskItems.Select(MapToResponseDTO);

    }

    public async Task<TaskItemResponseDTO?> GetByIdAsync(int id)
    {
        var taskItem = await _context.TaskItems.Include(t => t.Project)
                                               .FirstOrDefaultAsync(t => t.Id == id);
        return MapToResponseDTO(taskItem!);
    }

    public async Task<IEnumerable<TaskItemResponseDTO>> GetByProjectIdAsync(int projectId)
    {
        var taskItems = await _context.TaskItems.Where(t => t.ProjectId == projectId)
                                       .Include(t => t.Project)
                                       .ToListAsync();
        return taskItems.Select(MapToResponseDTO);
    }

    public async Task<TaskItemResponseDTO?> UpdateAsync(int id, UpdateTaskItemRequest taskItem)
    {
        var updatedTaskItem = await _context.TaskItems.Include(t => t.Project)
                                                      .FirstOrDefaultAsync(t => t.Id == id);
        if (taskItem is null) return null;

        updatedTaskItem!.Title = taskItem.Title;
        updatedTaskItem.Description = taskItem.Description;
        updatedTaskItem.Status = taskItem.Status;
        updatedTaskItem.UpdatedAt = DateTimeOffset.UtcNow;
        await _context.SaveChangesAsync();
        return MapToResponseDTO(updatedTaskItem);

    }

    private TaskItemResponseDTO MapToResponseDTO(TaskItem taskItem)
    {
        return new TaskItemResponseDTO
        {
            Id = taskItem.Id,
            Title = taskItem.Title,
            Description = taskItem.Description,
            Status = taskItem.Status.ToString(),
            ProjectId = taskItem.ProjectId,
            ProjectName = taskItem.Project!.Name
        };
    }
}
