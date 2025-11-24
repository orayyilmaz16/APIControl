using System;

namespace APIControl.Application.DTOs
{
    public class RefreshResponse
    {
        public string AccessToken { get; set; } = default!;
        public DateTime AccessTokenExpiresAt { get; set; }

        public string RefreshToken { get; set; } = default!;
        public DateTime RefreshTokenExpiresAt { get; set; }
    }
}
