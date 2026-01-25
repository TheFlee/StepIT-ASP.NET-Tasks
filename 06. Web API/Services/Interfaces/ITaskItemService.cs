using _06._Web_API.DTOs.ProjectDTOs;
using _06._Web_API.Models;
using _06._Web_API.DTOs.TaskItemDTOs;

namespace _06._Web_API.Services.Interfaces;

public interface ITaskItemService
{
    Task<IEnumerable<TaskItemResponseDTO>> GetAllAsync();
    Task<TaskItemResponseDTO?> GetByIdAsync(int id);
    Task<IEnumerable<TaskItemResponseDTO>> GetByProjectIdAsync(int projectId);
    Task<TaskItemResponseDTO> CreateAsync(CreateTaskItemRequest taskItem);
    Task<TaskItemResponseDTO?> UpdateAsync(int id, UpdateTaskItemRequest taskItem);
    Task<bool> DeleteAsync(int id);
}
