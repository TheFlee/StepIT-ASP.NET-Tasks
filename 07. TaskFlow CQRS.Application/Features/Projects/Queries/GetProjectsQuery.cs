using _07._TaskFlow_CQRS.Application.DTOs;
using MediatR;

namespace _07._TaskFlow_CQRS.Application.Features.Projects.Queries;

public record GetProjectsQuery(string UserId, IList<string> UserRoles):IRequest<IEnumerable<ProjectResponseDto>>;
