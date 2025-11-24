using System;
using System.ComponentModel.DataAnnotations;

namespace APIControl.Application.DTOs
{
    public class UserDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = default!;

        [Required]
        [MaxLength(50)]
        public string Role { get; set; } = "User";

        [Required]
        public Guid ProductId { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiresAt { get; set; }
    }
}
