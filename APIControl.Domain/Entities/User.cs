public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string? Role { get; set; } // "User", "Admin"...
    public Guid ProductId { get; set; } // product tabanlı yetki için
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiresAt { get; set; }
}
