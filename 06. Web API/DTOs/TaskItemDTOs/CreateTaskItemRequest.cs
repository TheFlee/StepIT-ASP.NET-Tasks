using _06._Web_API.Models;

namespace _06._Web_API.DTOs.TaskItemDTOs;
/// <summary>
/// Represents a request to create a new task item within a project.
/// </summary>
public class CreateTaskItemRequest
{
    /// <summary>
    /// TaskItem Title
    /// </summary>
    /// <example>My Title</example>
    public string Title { get; set; } = string.Empty;
    /// <summary>
    /// TaskItem Description
    /// </summary>
    /// <example>Desc...</example>
    public string Description { get; set; } = string.Empty;
    /// <summary>
    /// Associated Project ID
    /// </summary>
    /// <example>1</example>
    public int ProjectId { get; set; }
    /// <summary>
    /// Priority level assigned to the task.
    /// </summary>
    public TaskPriority Priority { get; set; }

}
