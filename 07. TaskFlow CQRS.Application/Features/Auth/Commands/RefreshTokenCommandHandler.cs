using _07._TaskFlow_CQRS.Application.Contracts.Auth;
using _07._TaskFlow_CQRS.Application.DTOs;
using _07._TaskFlow_CQRS.Application.Repositories;
using _07._TaskFlow_CQRS.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07._TaskFlow_CQRS.Application.Features.Auth.Commands;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponseDto>
{
    private readonly IJwtService _jwtService;
    private readonly IRefreshTokenRepository _refreshTokenRepo;
    private readonly IAuthIdentityProvider _identityProvider;
    private readonly IAuthService _authService;

    public RefreshTokenCommandHandler(
        IJwtService jwtService, 
        IRefreshTokenRepository refreshTokenRepo, 
        IAuthIdentityProvider identityProvider,
        IAuthService authService)
    {
        _jwtService = jwtService;
        _refreshTokenRepo = refreshTokenRepo;
        _identityProvider = identityProvider;
        _authService = authService;
    }

    public async Task<AuthResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var (userId, jti) = _jwtService.ValidateRefreshToken(request.Request.RefreshToken);
        var storedToken = await _refreshTokenRepo.GetByJwtIdAsync(jti);
        if (storedToken == null || !storedToken.IsActive) 
            throw new UnauthorizedAccessException("Invalid refresh token");
        var user = await _identityProvider.FindByIdAsync(userId);
        if (user == null) 
            throw new UnauthorizedAccessException("User not found");
        storedToken.RevokedAt = DateTime.UtcNow;
        await _refreshTokenRepo.UpdateAsync(storedToken);
        return await _authService.GenerateTokenResponseAsync(user);
    }
}
