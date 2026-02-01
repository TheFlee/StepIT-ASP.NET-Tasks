using _06._Web_API.Common;
using _06._Web_API.DTOs.TaskItemDTOs;
using _06._Web_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace _06._Web_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskItemsController : ControllerBase
{
    private readonly ITaskItemService _taskItemService;
    public TaskItemsController(ITaskItemService taskItemService)
    {
        _taskItemService = taskItemService;
    }
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<TaskItemResponseDTO>>> GetAll()
    {
        var tasks = await _taskItemService.GetAllAsync();
        return Ok(tasks);
    }
    [HttpGet]
    public async Task<ActionResult<PagedResult<TaskItemResponseDTO>>> GetPaged([FromQuery]TaskItemQueryParams queryParams)
    {
        var tasks = await _taskItemService.GetPagedAsync(queryParams);
        return Ok(tasks);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItemResponseDTO>> GetById(int id)
    {
        var task = await _taskItemService.GetByIdAsync(id);
        if (task == null)
        {
            return NotFound($"TaskItem with ID {id} not found");
        }
        return Ok(task);
    }

    [HttpGet("project/{projectId}")]
    public async Task<ActionResult<TaskItemResponseDTO>> GetByProjectId(int projectId)
    {
        var task = await _taskItemService.GetByProjectIdAsync(projectId);
        if (task == null)
        {
            return NotFound($"TaskItem with Project ID {projectId} not found");
        }
        return Ok(task);
    }
    [HttpPost]
    public async Task<ActionResult<TaskItemResponseDTO>> Create([FromBody] CreateTaskItemRequest taskItem)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var task = await _taskItemService.CreateAsync(taskItem);
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TaskItemResponseDTO>> Update(int id, [FromBody] UpdateTaskItemRequest taskItem)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var updatedTask = await _taskItemService.UpdateAsync(id, taskItem);
        if (updatedTask == null)
        {
            return NotFound($"TaskItem with ID {id} not found");
        }
        return Ok(updatedTask);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deletedTask = await _taskItemService.DeleteAsync(id);
        if (!deletedTask)
        {
            return NotFound($"TaskItem with ID {id} not found");
        }
        return NoContent();
    }
}
