using APIControl.Application.DTOs.TokenDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIControl.Application.Validation
{
    public class TokenValidationRequestValidator : AbstractValidator<TokenValidationRequest>
    {
        public TokenValidationRequestValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Token zorunludur.")
                .MinimumLength(20).WithMessage("Token en az 20 karakter olmalı.");
        }
    }
}
