using APIControl.Application.DTOs.TokenDTO;
using APIControl.Domain.Entities;

namespace APIControl.Application.Services
{
    public interface ITokenService
    {
        TokenResponse CreateTokens(User user);
        bool ValidateToken(string token);
    }
}
