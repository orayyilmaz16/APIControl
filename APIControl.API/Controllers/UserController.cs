using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using APIControl.Application.Services;
using APIControl.Application.DTOs;
using AutoMapper;

namespace APIControl.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // sadece Admin erişebilir
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // GET: api/users/email/{email}
        [HttpGet("email/{email}")]
        public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null) return NotFound();

            return Ok(_mapper.Map<UserDto>(user));
        }

        // POST: api/users/register
        [HttpPost("register")]
        [AllowAnonymous] // register için anonim erişim
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterRequest req)
        {
            var user = await _userService.RegisterAsync(req.Email, req.Password, req.ProductId, "User");
            return Ok(_mapper.Map<UserDto>(user));
        }

        // POST: api/users/validate
        [HttpPost("validate")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> Validate([FromBody] LoginRequest req)
        {
            var isValid = await _userService.ValidatePasswordAsync(req.Email, req.Password);
            return Ok(isValid);
        }

        // PUT: api/users/{email}/refresh
        [HttpPut("{email}/refresh")]
        public async Task<IActionResult> UpdateRefreshToken(string email, [FromBody] RefreshRequest req)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null) return NotFound();

            await _userService.UpdateRefreshTokenAsync(user, req.RefreshToken, DateTime.UtcNow.AddDays(7));
            return NoContent();
        }
    }
}
