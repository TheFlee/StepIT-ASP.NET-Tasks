using _07._TaskFlow_CQRS.Application.Validators;
using FluentValidation;

namespace _07._TaskFlow_CQRS.Application.Features.Auth.Commands;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.Request).SetValidator(new RefreshTokenRequestValidator());
    }
}
