using FluentValidation;
using APIControl.Application.DTOs;

public class RefreshRequestValidator : AbstractValidator<RefreshRequest>
{
    public RefreshRequestValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Refresh token zorunludur.")
            .MinimumLength(20).WithMessage("Refresh token en az 20 karakter olmalı.");

        RuleFor(x => x.AccessToken)
            .NotEmpty().WithMessage("Access token zorunludur.");

        RuleFor(x => x.DeviceId)
            .MaximumLength(100).WithMessage("DeviceId en fazla 100 karakter olabilir.")
            .When(x => !string.IsNullOrEmpty(x.DeviceId));

        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("ProductId zorunludur.")
            .When(x => x.ProductId.HasValue);
    }
}
