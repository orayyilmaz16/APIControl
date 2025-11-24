using FluentValidation;
using APIControl.Application.DTOs;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email zorunludur.")
            .EmailAddress().WithMessage("Geçerli bir email giriniz.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Parola zorunludur.")
            .MinimumLength(6).WithMessage("Parola en az 6 karakter olmalı.")
            .MaximumLength(100).WithMessage("Parola en fazla 100 karakter olabilir.");

        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("ProductId zorunludur.");
    }
}
