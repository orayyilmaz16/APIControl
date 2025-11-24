using APIControl.Application.DTOs;
using APIControl.Application.DTOs.TokenDTO;
using APIControl.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace APIControl.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _cfg;
        private readonly IUserService _users;

        public TokenService(IConfiguration cfg, IUserService users)
        {
            _cfg = cfg;
            _users = users;
        }

        public TokenResponse CreateTokens(User user)
        {
            var issuer = _cfg["Jwt:Issuer"];
            var audience = _cfg["Jwt:Audience"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_cfg["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role ?? "User"),
                new Claim("productId", user.ProductId.ToString())
            };

            var accessExp = DateTime.UtcNow.AddMinutes(int.Parse(_cfg["Jwt:AccessTokenMinutes"] ?? "30"));
            var accessToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: accessExp,
                signingCredentials: creds);

            var accessTokenStr = new JwtSecurityTokenHandler().WriteToken(accessToken);

            var refreshBytes = RandomNumberGenerator.GetBytes(64);
            var refreshToken = Convert.ToBase64String(refreshBytes);
            var refreshExp = DateTime.UtcNow.AddDays(int.Parse(_cfg["Jwt:RefreshTokenDays"] ?? "7"));

            _users.UpdateRefreshTokenAsync(user, refreshToken, refreshExp).Wait();

            return new TokenResponse
            {
                AccessToken = accessTokenStr,
                AccessTokenExpiresAt = accessExp,
                RefreshToken = refreshToken,
                RefreshTokenExpiresAt = refreshExp,
                UserId = user.Id,
                Email = user.Email,
                Role = user.Role ?? "User",
                ProductId = user.ProductId
            };
        }

        public bool ValidateToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_cfg["Jwt:Key"]!));

            try
            {
                handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _cfg["Jwt:Issuer"],
                    ValidAudience = _cfg["Jwt:Audience"],
                    IssuerSigningKey = key,
                    ClockSkew = TimeSpan.Zero
                }, out _);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
