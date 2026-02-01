using _06._Web_API.Common;
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

        return _mapper.Map<TaskItemResponseDTO>(taskItem);
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

    public async Task<PagedResult<TaskItemResponseDTO>> GetPagedAsync(TaskItemQueryParams queryParams)
    {
        queryParams.Validate();
        var query = _context.TaskItems.Include(t => t.Project).AsQueryable();

        // filter by ProjectId
        if(queryParams.ProjectId.HasValue)
        {
            query = query.Where(t => t.ProjectId == queryParams.ProjectId.Value);
        }

        // filter by Status
        if (!string.IsNullOrEmpty(queryParams.Status))
        {
            if(Enum.TryParse<Models.TaskStatus>(queryParams.Status, true, out var status))
            {
                query = query.Where(t => t.Status == status);
            }
        }

        // filter by Priority
        if (!string.IsNullOrEmpty(queryParams.Priority))
        {
            if(Enum.TryParse<Models.TaskPriority>(queryParams.Priority, true, out var priority))
            {
                query = query.Where(t => t.Priority == priority);
            }
        }

        // filter by Title and Description
        if (!string.IsNullOrEmpty(queryParams.Search))
        {
            var searchTerm = queryParams.Search.ToLower();
            query = query.Where(t => t.Title.ToLower().Contains(searchTerm) || 
                                     t.Description.ToLower().Contains(searchTerm));
        }

        // Pagination
        var totalCount = await query.CountAsync();
        var skip = (queryParams.Page - 1) * queryParams.PageSize;
        var tasks = await query.Skip(skip)
                               .Take(queryParams.PageSize)
                               .ToListAsync();
        var taskDTOs = _mapper.Map<IEnumerable<TaskItemResponseDTO>>(tasks);
        return PagedResult<TaskItemResponseDTO>.Create(taskDTOs, queryParams.Page, queryParams.PageSize, totalCount);
    }
}
