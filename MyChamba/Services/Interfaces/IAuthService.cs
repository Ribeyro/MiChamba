using MyChamba.DTOs.Auth;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(LoginRequest request);
}