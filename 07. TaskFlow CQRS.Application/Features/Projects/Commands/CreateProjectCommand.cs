using _07._TaskFlow_CQRS.Application.DTOs;
using MediatR;

namespace _07._TaskFlow_CQRS.Application.Features.Projects.Commands;

public record CreateProjectCommand(CreateProjectRequest Request, string OwnerId) : IRequest<ProjectResponseDto>;
