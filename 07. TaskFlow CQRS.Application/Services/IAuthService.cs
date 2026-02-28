using _07._TaskFlow_CQRS.Application.Contracts.Auth;
using _07._TaskFlow_CQRS.Application.DTOs;

namespace _07._TaskFlow_CQRS.Application.Services;

public interface IAuthService
{
    //Task<AuthResponseDto> LoginAsync(LoginRequest request);
    //Task<AuthResponseDto> RegisterAsync(RegisterRequest request);
    //Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequest request);
    //Task RevokeRefreshTokenAsync(RefreshTokenRequest request);
    Task<AuthResponseDto> GenerateTokenResponseAsync(AuthUserInfo user);
}
