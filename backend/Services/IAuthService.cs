using Backend.DTOs;

namespace Backend.Services;

public interface IAuthService
{
    Task<Guid> RegisterAsync(string email, string password);
    Task<AuthResponse> LoginAsync(string email, string password);

    Task<AuthResponse> RefreshTokenAsync(string refreshToken);
    Task LogoutAsync();
}
