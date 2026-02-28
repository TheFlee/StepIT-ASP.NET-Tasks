using _07._TaskFlow_CQRS.Application.Common;
using _07._TaskFlow_CQRS.Application.DTOs;
using _07._TaskFlow_CQRS.Domain;

namespace _07._TaskFlow_CQRS.Application.Services;

public interface ITaskItemService
{
    Task<TaskItem?> GetTaskEntityAsync(int id);
}
