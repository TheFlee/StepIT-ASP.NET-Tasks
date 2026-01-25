namespace _06._Web_API.DTOs.TaskItemDTOs;

public class CreateTaskItemRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ProjectId { get; set; }

}
