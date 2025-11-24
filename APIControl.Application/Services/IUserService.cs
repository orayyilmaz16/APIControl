public interface IUserService
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User> RegisterAsync(string email, string password, Guid productId, string role = "User");
    Task<bool> ValidatePasswordAsync(string email, string password);
    Task UpdateRefreshTokenAsync(User user, string refreshToken, DateTime expiresAt);
}