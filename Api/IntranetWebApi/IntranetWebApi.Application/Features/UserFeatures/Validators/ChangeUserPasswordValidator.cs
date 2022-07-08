using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using IntranetWebApi.Application.Features.UserFeatures.Commands;
using IntranetWebApi.Domain.Consts;

namespace IntranetWebApi.Application.Features.UserFeatures.Validators;

public class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
{
    public ChangeUserPasswordValidator()
    {
        RuleFor(x => x.IdUser)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0)
            .WithMessage("Nie podano użytkownika!");

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .NotNull()
            .MinimumLength(6)
            .WithMessage("Nowe hasło musi zawierać przynajmniej sześć znaków!");

        RuleFor(x => x.ConfirmNewPassword)
            .NotEmpty()
            .NotNull()
            .WithMessage("Nowe hasło musi zawierać przynajmniej sześć znaków!");

        RuleFor(x => x.OldPassword)
            .NotEmpty()
            .NotNull()
            .WithMessage("Nowe hasło musi zawierać przynajmniej sześć znaków!");

        RuleFor(x => x.NewPassword)
            .Custom((value, context) =>
            {
                if (!value.Any(char.IsUpper))
                {
                    context.AddFailure(nameof(value), "Nowe hasło musi zawierać przynajmniej jedną wielką literę!");
                }

                if (!value.Any(char.IsDigit))
                {
                    context.AddFailure(nameof(value), "Nowe hasło musi zawierać przynajmniej jedną cyfrę!");
                }

                if (!value.Any(x => PasswordSpecialCharConst.PossibleChars.Contains(x)))
                {
                    context.AddFailure(nameof(value), "Nowe hasło musi zawierać przynajmniej jeden znak specjalny!");
                }
            });
    }
}
