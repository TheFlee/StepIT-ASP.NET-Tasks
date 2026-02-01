namespace _06._Web_API.DTOs.ProjectDTOs;
/// <summary>
/// DTO for creating a new project. 
/// </summary>
public class CreateProjectRequest
{
    /// <summary>
    /// Project Name
    /// </summary>
    /// <example>New Project</example>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Project Description
    /// </summary>
    /// <example>This is a sample project description.</example>
    public string Description { get; set; } = string.Empty;

}
