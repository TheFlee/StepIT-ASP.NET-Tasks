using _06._Web_API.Data;
using _06._Web_API.DTOs.TaskItemDTOs;
using _06._Web_API.Models;
using _06._Web_API.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace _06._Web_API.Services;

public class TaskItemService : ITaskItemService
{
    private readonly TaskFlowDbContext _context;
    private readonly IMapper _mapper;

    public TaskItemService(TaskFlowDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<TaskItemResponseDTO> CreateAsync(CreateTaskItemRequest task)
    {
        var isProjectExists = await _context.Projects.AnyAsync(p => p.Id == task.ProjectId);
        if (!isProjectExists)
        {
            throw new ArgumentException($"Project with ID {task.ProjectId} does not exist.");
        }

        var taskItem = _mapper.Map<TaskItem>(task);

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

        return _mapper.Map<IEnumerable<TaskItemResponseDTO>>(taskItems);

    }

    public async Task<TaskItemResponseDTO?> GetByIdAsync(int id)
    {
        var taskItem = await _context.TaskItems.Include(t => t.Project)
                                               .FirstOrDefaultAsync(t => t.Id == id);
        if (taskItem is null) return null;
        return _mapper.Map<TaskItemResponseDTO>(taskItem);
    }

    public async Task<IEnumerable<TaskItemResponseDTO>> GetByProjectIdAsync(int projectId)
    {
        var taskItems = await _context.TaskItems.Where(t => t.ProjectId == projectId)
                                       .Include(t => t.Project)
                                       .ToListAsync();
        return _mapper.Map<IEnumerable<TaskItemResponseDTO>>(taskItems);

    }

    public async Task<TaskItemResponseDTO?> UpdateAsync(int id, UpdateTaskItemRequest taskItem)
    {
        var updatedTaskItem = await _context.TaskItems.Include(t => t.Project)
                                                      .FirstOrDefaultAsync(t => t.Id == id);
        if (updatedTaskItem is null) return null;

        _mapper.Map(taskItem, updatedTaskItem);

        await _context.SaveChangesAsync();

        return _mapper.Map<TaskItemResponseDTO>(updatedTaskItem);

    }
}
