using _07._TaskFlow_CQRS.Application.Contracts.Auth;
using _07._TaskFlow_CQRS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07._TaskFlow_CQRS.Application.Common;

public class AuthHelper
{
    private readonly IAuthIdentityProvider _authIdentityProvider;
    private readonly IJwtService _jwtService;

    public AuthHelper(IAuthIdentityProvider authIdentityProvider, IJwtService jwtService)
    {
        _authIdentityProvider = authIdentityProvider;
        _jwtService = jwtService;
    }

    public async Task<AuthResponseDto> GenerateTokenResponseAsync(AuthUserInfo user)
    {
        var roles = await _authIdentityProvider.GetRolesAsync(user.Id);
        var (accessToken, expiresAt) = _jwtService.GenerateAccessToken(user.Id, user.Email, roles);
        var (refreshToken, refreshExpiresAt) = await _jwtService.CreateAndStoreRefreshTokenAsync(user.Id);
        return new AuthResponseDto
        {
            AccessToken = accessToken,
            ExpiresAt = expiresAt,
            RefreshToken = refreshToken,
            RefreshTokenExpiresAt = refreshExpiresAt,
            Email = user.Email,
            Roles = roles
        };
    }
}
