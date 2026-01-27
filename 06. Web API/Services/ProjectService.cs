using _06._Web_API.Data;
using _06._Web_API.DTOs.ProjectDTOs;
using _06._Web_API.Models;
using _06._Web_API.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace _06._Web_API.Services;

public class ProjectService : IProjectService
{
    private readonly TaskFlowDbContext _context;
    private readonly IMapper _mapper;

    public ProjectService(TaskFlowDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProjectResponseDTO> CreateAsync(CreateProjectRequest project)
    {
        var newProject = _mapper.Map<Project>(project);

        _context.Projects.Add(newProject);
        await _context.SaveChangesAsync();
        
        await _context.Entry(newProject).Collection(p => p.Tasks).LoadAsync();
        return _mapper.Map<ProjectResponseDTO>(newProject);
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

        //return projects.Select(_mapper.Map<ProjectResponseDTO>);
        return _mapper.Map<IEnumerable<ProjectResponseDTO>>(projects);
    }

    public async Task<ProjectResponseDTO?> GetByIdAsync(int id)
    {
        var project = await _context.Projects.Include(p => p.Tasks).FirstOrDefaultAsync(p => p.Id == id);

        if (project is null) return null;

        return _mapper.Map<ProjectResponseDTO>(project);
    }

    public async Task<ProjectResponseDTO?> UpdateAsync(int id, UpdateProjectRequest project)
    {
        var updatedProject = await _context.Projects.Include(p => p.Tasks)
                                                    .FirstOrDefaultAsync(p => p.Id == id);

        if (updatedProject is null) return null;

        _mapper.Map(project, updatedProject);

        await _context.SaveChangesAsync();

        return _mapper.Map<ProjectResponseDTO>(updatedProject);
    }

}   
