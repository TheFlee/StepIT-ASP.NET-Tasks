using _06._Web_API.Models;

namespace _06._Web_API.DTOs.TaskItemDTOs;

public class UpdateTaskItemRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Models.TaskStatus Status { get; set; }
    public Models.TaskPriority Priority { get; set; }
}
