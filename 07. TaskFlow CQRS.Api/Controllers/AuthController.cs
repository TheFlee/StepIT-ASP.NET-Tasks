using _07._TaskFlow_CQRS.Application.Common;
using _07._TaskFlow_CQRS.Application.DTOs;
using _07._TaskFlow_CQRS.Application.Features.Auth.Commands;
using _07._TaskFlow_CQRS.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace _07._TaskFlow_CQRS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMediator _mediator;

    public AuthController(IAuthService authService, IMediator mediator)
    {
        _authService = authService;
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Register([FromBody] RegisterRequest registerRequest)
    {
        var result = await _mediator.Send(new RegisterCommand(registerRequest));
        return Ok(ApiResponse<AuthResponseDto>.SuccessResponse(result));
    }

    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Login([FromBody] LoginRequest loginRequest)
    {
        var result = await _mediator.Send(new LoginCommand(loginRequest));
        return Ok(ApiResponse<AuthResponseDto>.SuccessResponse(result));
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Refresh([FromBody] RefreshTokenRequest refreshTokenRequest)
    {
        var result = await _mediator.Send(new RefreshTokenCommand(refreshTokenRequest));
        return Ok(ApiResponse<AuthResponseDto>.SuccessResponse(result));
    }

    [HttpPost("revoke")]
    public async Task<ActionResult<ApiResponse<object>>> Revoke([FromBody] RefreshTokenRequest refreshTokenRequest)
    {
        await _mediator.Send(new RevokeTokenCommand(refreshTokenRequest));
        return Ok(ApiResponse<object>.SuccessResponse("Refresh token revoked"));
    }
}
