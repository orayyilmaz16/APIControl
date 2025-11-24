using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APIControl.Application.DTOs.TokenDTO
{
    public class TokenValidationRequest
    {
        [Required]
        public string Token { get; set; } = default!;
    }
}
