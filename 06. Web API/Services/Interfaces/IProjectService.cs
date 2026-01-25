using _06._Web_API.DTOs.ProjectDTOs;
using _06._Web_API.Models;

namespace _06._Web_API.Services.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponseDTO>> GetAllAsync();
    Task<ProjectResponseDTO?> GetByIdAsync(int id);
    Task<ProjectResponseDTO> CreateAsync(CreateProjectRequest project);
    Task<ProjectResponseDTO?> UpdateAsync(int id, UpdateProjectRequest project);
    Task<bool> DeleteAsync(int id);
}
