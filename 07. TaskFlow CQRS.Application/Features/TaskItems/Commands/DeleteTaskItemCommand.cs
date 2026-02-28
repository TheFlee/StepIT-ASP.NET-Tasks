using MediatR;

namespace _07._TaskFlow_CQRS.Application.Features.TaskItems.Commands;

public record DeleteTaskItemCommand(int Id) : IRequest<bool>;
