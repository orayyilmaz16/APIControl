using System.ComponentModel.DataAnnotations;

public class RegisterRequest
{
    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; } = default!;

    [Required]
    [MinLength(6)]
    [MaxLength(100)]
    public string Password { get; set; } = default!;

    [Required]
    public Guid ProductId { get; set; }

    // Opsiyonel alanlar
    [MaxLength(50)]
    public string? FirstName { get; set; }

    [MaxLength(50)]
    public string? LastName { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }
}
