using _07._TaskFlow_CQRS.Application.Contracts.Auth;
using _07._TaskFlow_CQRS.Application.DTOs;
using _07._TaskFlow_CQRS.Application.Services;
using MediatR;

namespace _07._TaskFlow_CQRS.Application.Features.Auth.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto>
{
    private readonly IAuthIdentityProvider _identityProvider;
    private readonly IAuthService _authService;

    public LoginCommandHandler(IAuthIdentityProvider identityProvider, IAuthService authService)
    {
        _identityProvider = identityProvider;
        _authService = authService;
    }

    public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _identityProvider.FindByEmailAsync(request.Request.Email);
        if (user == null) 
            throw new UnauthorizedAccessException("Invalid email or password.");
        if (!await _identityProvider.CheckPasswordAsync(user.Id, request.Request.Password)) 
            throw new UnauthorizedAccessException("Invalid email or password.");
        return await _authService.GenerateTokenResponseAsync(user);
    }
}
