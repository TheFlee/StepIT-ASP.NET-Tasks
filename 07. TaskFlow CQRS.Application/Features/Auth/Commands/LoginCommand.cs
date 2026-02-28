using _07._TaskFlow_CQRS.Application.DTOs;
using MediatR;

namespace _07._TaskFlow_CQRS.Application.Features.Auth.Commands;

public record LoginCommand(LoginRequest Request) : IRequest<AuthResponseDto>;
