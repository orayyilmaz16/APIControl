public class AuthResponse
{
    public string AccessToken { get; set; } = default!;
    public DateTime AccessTokenExpiresAt { get; set; }

    public string RefreshToken { get; set; } = default!;
    public DateTime RefreshTokenExpiresAt { get; set; }

    // Kullanıcı bilgileri
    public Guid UserId { get; set; }
    public string Email { get; set; } = default!;
    public string Role { get; set; } = "User";
    public Guid ProductId { get; set; }

    public AuthResponse() { }

    public AuthResponse(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public AuthResponse(string accessToken, DateTime accessExp, string refreshToken, DateTime refreshExp, User user)
    {
        AccessToken = accessToken;
        AccessTokenExpiresAt = accessExp;
        RefreshToken = refreshToken;
        RefreshTokenExpiresAt = refreshExp;
        UserId = user.Id;
        Email = user.Email;
        Role = user.Role ?? "User";
        ProductId = user.ProductId;
    }
}
