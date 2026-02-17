using _06._Web_API.Models;
using TaskStatus = _06._Web_API.Models.TaskStatus;

namespace _06._Web_API.DTOs.Task_Items_DTOs;

public class TaskStatusUpdateRequest
{
    public TaskStatus Status { get; set; } = TaskStatus.ToDo;
}
