using _07._TaskFlow_CQRS.Application.DTOs;
using MediatR;

namespace _07._TaskFlow_CQRS.Application.Features.Projects.Queries;
public record GetProjectByIdQuery(int id) : IRequest<ProjectResponseDto>;