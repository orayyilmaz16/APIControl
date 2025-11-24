using APIControl.Application.DTOs;
using System.Security.Cryptography;
using System.Text;

public class AuthService : IAuthService
{
    private readonly IUserRepository _users;
    private readonly ITokenService _tokens;

    public AuthService(IUserRepository users, ITokenService tokens)
    {
        _users = users;
        _tokens = tokens;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest req)
    {
        // Parola hash (örnek PBKDF2; prod’da BCrypt/Argon2 önerilir)
        var hash = HashPassword(req.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = req.Email,
            PasswordHash = hash,
            Role = "User",
            ProductId = req.ProductId
        };

        await _users.CreateAsync(user);

        var access = _tokens.CreateAccessToken(user);
        var (refresh, exp) = _tokens.CreateRefreshToken();

        user.RefreshToken = refresh;
        user.RefreshTokenExpiresAt = exp;
        await _users.UpdateAsync(user);

        return new AuthResponse(access, refresh);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest req)
    {
        var user = await _users.GetByEmailAsync(req.Email)
                   ?? throw new UnauthorizedAccessException("Invalid credentials");

        if (!VerifyPassword(req.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials");

        var access = _tokens.CreateAccessToken(user);
        var (refresh, exp) = _tokens.CreateRefreshToken();

        user.RefreshToken = refresh;
        user.RefreshTokenExpiresAt = exp;
        await _users.UpdateAsync(user);

        return new AuthResponse(access, refresh);
    }

    public async Task<AuthResponse> RefreshAsync(RefreshRequest req)
    {
        var user = await _users.GetByRefreshTokenAsync(req.RefreshToken)
                   ?? throw new UnauthorizedAccessException("Invalid refresh token");

        if (user.RefreshTokenExpiresAt is null || user.RefreshTokenExpiresAt <= DateTime.UtcNow)
            throw new UnauthorizedAccessException("Refresh token expired");

        var access = _tokens.CreateAccessToken(user);
        var (refresh, exp) = _tokens.CreateRefreshToken();

        // Rotation: eski refresh tokenı geçersiz kıl, yenisi ver
        user.RefreshToken = refresh;
        user.RefreshTokenExpiresAt = exp;
        await _users.UpdateAsync(user);

        return new AuthResponse(access, refresh);
    }

    // Basit örnek hash/verify
    private static string HashPassword(string password)
    {
        using var sha = SHA256.Create();
        return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
    }
    private static bool VerifyPassword(string password, string hash) =>
        HashPassword(password) == hash;
}
