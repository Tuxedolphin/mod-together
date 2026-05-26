using Backend.DTOs;

namespace Backend.Services;

public class AuthService : IAuthService
{
    public Task<AuthResponse> LoginAsync(string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task LogoutAsync()
    {
        throw new NotImplementedException();
    }

    public Task<AuthResponse> RefreshTokenAsync(string refreshToken)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> RegisterAsync(string email, string password)
    {
        throw new NotImplementedException();
    }
}
