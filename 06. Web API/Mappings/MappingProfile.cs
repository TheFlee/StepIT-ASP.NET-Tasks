using _06._Web_API.DTOs;
using _06._Web_API.Models;
using AutoMapper;

namespace _06._Web_API.Mappings;

public class MappingProfile:Profile
{
	public MappingProfile()
	{
		// Project

		CreateMap<Project, ProjectResponseDto>()
			.ForMember(dest => dest.TaskCount, opt => opt.MapFrom(src => src.Tasks.Count()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<CreateProjectRequest, Project>()
			.ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
			.ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
			.ForMember(dest => dest.Tasks, opt => opt.Ignore());

        CreateMap<UpdateProjectRequest, Project>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Tasks, opt => opt.Ignore());

		// TaskItem

		CreateMap<TaskItem, TaskItemResponseDto>()
			.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
			.ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.ToString()))
			.ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.Name));

        CreateMap<CreateTaskItemRequest, TaskItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Project, opt => opt.Ignore());

        CreateMap<UpdateTaskItemRequest, TaskItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Project, opt => opt.Ignore())
            .ForMember(dest => dest.ProjectId, opt => opt.Ignore());
    }
}
