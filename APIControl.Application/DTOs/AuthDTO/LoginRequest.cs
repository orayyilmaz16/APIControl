using System.ComponentModel.DataAnnotations;

public class LoginRequest
{
    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; } = default!;

    [Required]
    [MinLength(6)]
    [MaxLength(100)]
    public string Password { get; set; } = default!;

    // İsteğe bağlı: hangi product için login yapılıyor
    public Guid? ProductId { get; set; }
}
