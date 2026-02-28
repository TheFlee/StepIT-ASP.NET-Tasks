using _07._TaskFlow_CQRS.Domain;

namespace _07._TaskFlow_CQRS.Application.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<ApplicationUser>> GetOrderedByEmailExceptIdsAsync(IEnumerable<string> excludeIds);
}
