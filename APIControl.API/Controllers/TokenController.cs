using APIControl.Application.DTOs;
using APIControl.Application.DTOs.TokenDTO;
using APIControl.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIControl.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokens;
        private readonly IUserService _users;

        public TokenController(ITokenService tokens, IUserService users)
        {
            _tokens = tokens;
            _users = users;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<TokenResponse>> CreateToken([FromBody] TokenCreateRequest req)
        {
            var user = await _users.GetUserByEmailAsync(req.Email);
            if (user == null) return NotFound("Kullanıcı bulunamadı.");

            var response = _tokens.CreateTokens(user);
            return Ok(response);
        }

        [HttpPost("validate")]
        [AllowAnonymous]
        public ActionResult<bool> ValidateToken([FromBody] TokenValidationRequest req)
        {
            var isValid = _tokens.ValidateToken(req.Token);
            return Ok(isValid);
        }
    }
}
