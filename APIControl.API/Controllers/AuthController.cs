using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using APIControl.Application.DTOs;
using APIControl.Application.Services;

namespace APIControl.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest req)
        {
            var result = await _auth.RegisterAsync(req);
            return Ok(result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest req)
        {
            var result = await _auth.LoginAsync(req);
            return Ok(result);
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<ActionResult<RefreshResponse>> Refresh([FromBody] RefreshRequest req)
        {
            var result = await _auth.RefreshAsync(req);
            return Ok(result);
        }
    }
}
