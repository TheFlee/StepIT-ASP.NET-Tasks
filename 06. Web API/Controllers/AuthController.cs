using _06._Web_API.Common;
using _06._Web_API.DTOs.AuthDTOs;
using _06._Web_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace _06._Web_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<AuthResponseDTO>>> Register([FromBody]RegisterRequest registerRequest)
    {
        var result = await _authService.RegisterAsync(registerRequest);
        return Ok(ApiResponse<AuthResponseDTO>.SuccessResponse(result));
    }

    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<AuthResponseDTO>>> Login([FromBody]LoginRequest loginRequest)
    {
        var result = await _authService.LoginAsync(loginRequest);
        return Ok(ApiResponse<AuthResponseDTO>.SuccessResponse(result));
    }
}
