using _06._Web_API.DTOs.Project_DTOs;

namespace _06._Web_API.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectResponseDto>> GetAllAsync();
        Task<ProjectResponseDto?> GetByIdAsync(int id);
        Task<ProjectResponseDto> CreateAsync(CreateProjectRequest createProjectRequest);
        Task<ProjectResponseDto?> UpdateAsync(int id, UpdateProjectRequest updateProjectRequest);
        Task<bool> DeleteAsync(int id);
    }
}
