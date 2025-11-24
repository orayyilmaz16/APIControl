public interface ITokenService
{
    string CreateAccessToken(User user);
    (string token, DateTime expiresAt) CreateRefreshToken();
}
