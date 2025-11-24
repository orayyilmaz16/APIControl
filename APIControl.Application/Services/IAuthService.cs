using APIControl.Application.DTOs;

namespace APIControl.Application.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<RefreshResponse> RefreshAsync(RefreshRequest request);
    }
}
