using _07._TaskFlow_CQRS.Application.Contracts.Auth;
using _07._TaskFlow_CQRS.Application.Repositories;
using MediatR;

namespace _07._TaskFlow_CQRS.Application.Features.Auth.Commands;

public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand, Unit>
{
    private readonly IJwtService _jwtService;
    private readonly IRefreshTokenRepository _refreshTokenRepo;

    public RevokeTokenCommandHandler(IJwtService jwtService, IRefreshTokenRepository refreshTokenRepo)
    {
        _jwtService = jwtService;
        _refreshTokenRepo = refreshTokenRepo;
    }

    public async Task<Unit> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
    {
        var (_, jti) = _jwtService.ValidateRefreshToken(request.Request.RefreshToken, false);
        var storedToken = await _refreshTokenRepo.GetByJwtIdAsync(jti);
        if (storedToken != null && storedToken.IsActive)
        {
            storedToken.RevokedAt = DateTime.UtcNow;
            await _refreshTokenRepo.UpdateAsync(storedToken);
        }
        return Unit.Value;
    }
}
