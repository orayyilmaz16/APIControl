using APIControl.Application.DTOs;
using APIControl.Domain.Entities;

namespace APIControl.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _users;
        private readonly ITokenService _tokens;

        public AuthService(IUserService users, ITokenService tokens)
        {
            _users = users;
            _tokens = tokens;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            // Yeni kullanıcı oluştur
            var user = await _users.RegisterAsync(request.Email, request.Password, request.ProductId, "User");

            // Token üret
            var tokenResponse = _tokens.CreateTokens(user);

            return new AuthResponse
            {
                UserId = user.Id,
                Email = user.Email,
                Role = user.Role ?? "User",
                ProductId = user.ProductId,
                AccessToken = tokenResponse.AccessToken,
                AccessTokenExpiresAt = tokenResponse.AccessTokenExpiresAt,
                RefreshToken = tokenResponse.RefreshToken,
                RefreshTokenExpiresAt = tokenResponse.RefreshTokenExpiresAt
            };
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var isValid = await _users.ValidatePasswordAsync(request.Email, request.Password);
            if (!isValid)
                throw new UnauthorizedAccessException("Email veya parola hatalı!");

            var user = await _users.GetUserByEmailAsync(request.Email);
            if (user == null)
                throw new UnauthorizedAccessException("Kullanıcı bulunamadı!");

            var tokenResponse = _tokens.CreateTokens(user);

            return new AuthResponse
            {
                UserId = user.Id,
                Email = user.Email,
                Role = user.Role ?? "User",
                ProductId = user.ProductId,
                AccessToken = tokenResponse.AccessToken,
                AccessTokenExpiresAt = tokenResponse.AccessTokenExpiresAt,
                RefreshToken = tokenResponse.RefreshToken,
                RefreshTokenExpiresAt = tokenResponse.RefreshTokenExpiresAt
            };
        }

        public async Task<RefreshResponse> RefreshAsync(RefreshRequest request)
        {
            // Refresh token kontrolü
            var user = await _users.GetUserByEmailAsync(request.DeviceId ?? string.Empty);
            if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiresAt < DateTime.UtcNow)
                throw new UnauthorizedAccessException("Refresh token geçersiz veya süresi dolmuş!");

            var tokenResponse = _tokens.CreateTokens(user);

            return new RefreshResponse
            {
                AccessToken = tokenResponse.AccessToken,
                AccessTokenExpiresAt = tokenResponse.AccessTokenExpiresAt,
                RefreshToken = tokenResponse.RefreshToken,
                RefreshTokenExpiresAt = tokenResponse.RefreshTokenExpiresAt
            };
        }
    }
}
