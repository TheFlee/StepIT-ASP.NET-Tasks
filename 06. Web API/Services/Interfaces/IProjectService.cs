using _06._Web_API.Models;

namespace _06._Web_API.Services.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<Project>> GetAllAsync();
    Task<Project?> GetByIdAsync(int id);
    Task<Project> CreateAsync(Project project);
    Task<Project?> UpdateAsync(int id, Project project);
    Task<bool> DeleteAsync(int id);
}
