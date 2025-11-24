// APIControl.Application/Validation/CreateProductValidator.cs
using APIControl.Application.DTOs;
using APIControl.Application.DTOs.TokenDTO;
using FluentValidation;

public class TokenCreateRequestValidator : AbstractValidator<TokenCreateRequest>
{
    public TokenCreateRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email zorunludur.")
            .EmailAddress().WithMessage("Geçerli bir email giriniz.");
    }
}