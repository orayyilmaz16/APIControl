public class UserService : IUserService
{
    private readonly IUserRepository _repo;
    public UserService(IUserRepository repo) => _repo = repo;

    public async Task<User?> GetUserByEmailAsync(string email) => await _repo.GetByEmailAsync(email);

    public async Task<User> RegisterAsync(string email, string password, Guid productId, string role = "User")
    {
        var hash = HashPassword(password);
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            PasswordHash = hash,
            Role = role,
            ProductId = productId
        };
        await _repo.CreateAsync(user);
        return user;
    }

    public async Task<bool> ValidatePasswordAsync(string email, string password)
    {
        var user = await _repo.GetByEmailAsync(email);
        if (user == null) return false;
        return VerifyPassword(password, user.PasswordHash);
    }

    public async Task UpdateRefreshTokenAsync(User user, string refreshToken, DateTime expiresAt)
    {
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiresAt = expiresAt;
        await _repo.UpdateAsync(user);
    }

    private static string HashPassword(string password)
    {
        using var sha = System.Security.Cryptography.SHA256.Create();
        return Convert.ToBase64String(sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
    }
    private static bool VerifyPassword(string password, string hash) => HashPassword(password) == hash;
}