using System;
using System.ComponentModel.DataAnnotations;

namespace APIControl.Application.DTOs
{
    public class RefreshRequest
    {
        [Required(ErrorMessage = "Refresh token zorunludur.")]
        [StringLength(500, MinimumLength = 20, ErrorMessage = "Refresh token en az 20 karakter olmalıdır.")]
        public string RefreshToken { get; set; } = default!;

        [Required(ErrorMessage = "Access token zorunludur.")]
        [StringLength(2000, ErrorMessage = "Access token çok uzun.")]
        public string AccessToken { get; set; } = default!;

        // Opsiyonel: client id / device id
        [MaxLength(100)]
        public string? DeviceId { get; set; }

        // Opsiyonel: product context
        public Guid? ProductId { get; set; }
    }
}
