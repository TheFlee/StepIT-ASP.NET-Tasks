using _07._TaskFlow_CQRS.Application.Contracts.Auth;
using _07._TaskFlow_CQRS.Application.DTOs;
using _07._TaskFlow_CQRS.Application.Services;
using MediatR;

namespace _07._TaskFlow_CQRS.Application.Features.Auth.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
{
    private readonly IAuthIdentityProvider _identityProvider;
    private readonly IAuthService _authService;

    public RegisterCommandHandler(IAuthIdentityProvider identityProvider, IAuthService authService)
    {
        _identityProvider = identityProvider;
        _authService = authService;
    }

    public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await _identityProvider.FindByEmailAsync(request.Request.Email) != null) 
            throw new InvalidOperationException("User already exists.");
        var user = await _identityProvider.CreateAsync(request.Request);
        await _identityProvider.AddToRoleAsync(user.Id, "User");
        return await _authService.GenerateTokenResponseAsync(user);
    }
}
