using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APIControl.Application.DTOs.TokenDTO
{
    public class TokenCreateRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;
    }   
}
