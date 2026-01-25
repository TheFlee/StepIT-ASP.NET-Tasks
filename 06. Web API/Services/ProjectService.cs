using _06._Web_API.Data;
using _06._Web_API.DTOs.ProjectDTOs;
using _06._Web_API.Models;
using _06._Web_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace _06._Web_API.Services;

public class ProjectService : IProjectService
{
    private readonly TaskFlowDbContext _context;

    public ProjectService(TaskFlowDbContext context)
    {
        _context = context;
    }

    public async Task<ProjectResponseDTO> CreateAsync(CreateProjectRequest project)
    {
        var newProject = new Project
        {
            Name = project.Name,
            Description = project.Description,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = null!
        };
        
        _context.Projects.Add(newProject);
        await _context.SaveChangesAsync();
        
        await _context.Entry(newProject).Collection(p => p.Tasks).LoadAsync();
        return MapToResponseDTO(newProject);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var project = await _context.Projects.FindAsync(id);

        if (project is null) return false;
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<ProjectResponseDTO>> GetAllAsync()
    {
        var projects = await _context.Projects.Include(p => p.Tasks).ToListAsync();
        return projects.Select(MapToResponseDTO);
    }

    public async Task<ProjectResponseDTO?> GetByIdAsync(int id)
    {
        var project = await _context.Projects.Include(p => p.Tasks).FirstOrDefaultAsync(p => p.Id == id);
        return MapToResponseDTO(project!);
    }

    public async Task<ProjectResponseDTO?> UpdateAsync(int id, UpdateProjectRequest project)
    {
        var updatedProject = await _context.Projects.Include(p => p.Tasks)
                                                    .FirstOrDefaultAsync(p => p.Id == id);

        if (updatedProject is null) return null;

        updatedProject!.Name = project.Name;
        updatedProject.Description = project.Description;
        updatedProject.UpdatedAt = DateTimeOffset.UtcNow;
        await _context.SaveChangesAsync();
        return MapToResponseDTO(updatedProject);
    }

    private ProjectResponseDTO MapToResponseDTO(Project project)
    {
        return new ProjectResponseDTO
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            TaskCount = project.Tasks.Count()
        };
    }
}   
