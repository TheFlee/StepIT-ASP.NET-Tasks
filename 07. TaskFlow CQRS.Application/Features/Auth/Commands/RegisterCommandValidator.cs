using _07._TaskFlow_CQRS.Application.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07._TaskFlow_CQRS.Application.Features.Auth.Commands;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Request).SetValidator(new RegisterRequestValidator());
    }
}
