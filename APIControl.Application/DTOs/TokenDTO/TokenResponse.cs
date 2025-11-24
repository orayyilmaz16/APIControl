namespace APIControl.Application.DTOs.TokenDTO
{ 

    public class TokenResponse
    {
        public string AccessToken { get; set; } = default!;
        public DateTime AccessTokenExpiresAt { get; set; }
        public string RefreshToken { get; set; } = default!;
        public DateTime RefreshTokenExpiresAt { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; } = default!;
        public string Role { get; set; } = "User";
        public Guid ProductId { get; set; }
}

}