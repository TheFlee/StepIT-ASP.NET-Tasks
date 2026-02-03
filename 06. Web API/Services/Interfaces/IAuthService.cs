using _06._Web_API.DTOs.AuthDTOs;

namespace _06._Web_API.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDTO> RegisterAsync(RegisterRequest registerRequest);
    Task<AuthResponseDTO> LoginAsync(LoginRequest loginRequest);
}
