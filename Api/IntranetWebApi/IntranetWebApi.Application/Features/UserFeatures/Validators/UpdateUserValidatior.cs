using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using IntranetWebApi.Application.Features.UserFeatures.Commands;

namespace IntranetWebApi.Application.Features.UserFeatures.Validators;

public class UpdateUserValidatior : AbstractValidator<UpdateUserDataCommand>
{
    public UpdateUserValidatior()
    {
        RuleFor(x => x.UserInfo.IdUser)
            .GreaterThan(0)
            .WithMessage("Nie podano usera");

        RuleFor(x => x.UserInfo.FirstName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(2)
            .WithMessage("Podano nieprawidłowe imię użytkownika");

        RuleFor(x => x.UserInfo.LastName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(2)
            .WithMessage("Podano nieprawidłowe nazwisko użytkownika");

        RuleFor(x => x.UserInfo.IdDepartment)
            .GreaterThan(0)
            .WithMessage("Nie podano działu.");

        RuleFor(x => x.UserInfo.IdRole)
            .GreaterThan(0)
            .WithMessage("Nie podano roli.");

        RuleFor(x => x.UserInfo.IdPosition)
           .GreaterThan(0)
           .WithMessage("Nie podano stanowiska użytkownika.");

        RuleFor(x => x.UserInfo.PhotoName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(2)
            .WithMessage("Podano nieprawidłową nazwę zdjęcia");
    }
}
