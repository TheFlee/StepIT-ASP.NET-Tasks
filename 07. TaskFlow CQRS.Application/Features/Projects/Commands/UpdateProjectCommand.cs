using _07._TaskFlow_CQRS.Application.DTOs;
using MediatR;

namespace _07._TaskFlow_CQRS.Application.Features.Projects.Commands;

public record UpdateProjectCommand(int id, UpdateProjectRequest request) : IRequest<ProjectResponseDto?>;
