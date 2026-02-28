using _07._TaskFlow_CQRS.Domain;

namespace _07._TaskFlow_CQRS.Application.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByJwtIdAsync(string jwtId);
    Task<RefreshToken> AddAsync(RefreshToken refreshToken);
    Task UpdateAsync(RefreshToken refreshToken);
}
