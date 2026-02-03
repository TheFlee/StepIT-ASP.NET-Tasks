using _06._Web_API.DTOs.AuthDTOs;
using _06._Web_API.Models;
using _06._Web_API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace _06._Web_API.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public Task<AuthResponseDTO> LoginAsync(LoginRequest loginRequest)
    {
        var user = _userManager.FindByEmailAsync(loginRequest.Email);

        if (user == null)
        {
            throw new InvalidOperationException("Invalid email or password.");
        }
        var passwordValid = _userManager.CheckPasswordAsync(user.Result, loginRequest.Password);
        if (!passwordValid.Result)
        {
            throw new InvalidOperationException("Invalid email or password.");
        }
        return Task.FromResult(new AuthResponseDTO
        {
            Email = loginRequest.Email
        });
    }

    public async Task<AuthResponseDTO> RegisterAsync(RegisterRequest registerRequest)
    {
        var existingUser = await _userManager.FindByEmailAsync(registerRequest.Email);

        if (existingUser != null)
        {
            throw new InvalidOperationException("User with this email already exists.");
        }
        var newUser = new ApplicationUser
        {
            UserName = registerRequest.Email,
            Email = registerRequest.Email,
            FirstName = registerRequest.FirstName,
            LastName = registerRequest.LastName,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = null
        };
        var result = await _userManager.CreateAsync(newUser, registerRequest.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"User registration failed: {errors}");
        }
        return new AuthResponseDTO
        {
            Email = newUser.Email
        };
    }
}
