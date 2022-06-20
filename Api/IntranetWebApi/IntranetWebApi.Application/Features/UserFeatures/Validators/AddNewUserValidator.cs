using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using IntranetWebApi.Application.Features.UserFeatures.Commands;
using IntranetWebApi.Data;

namespace IntranetWebApi.Application.Features.UserFeatures.Validators;

public class AddNewUserValidator : AbstractValidator<AddNewUserCommand>
{
    public AddNewUserValidator(IntranetDbContext dbContext)
    {
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

        RuleFor(x => x.UserInfo.DateOfBirth)
            .NotNull()
            .NotEmpty()
            .GreaterThan(DateTime.Now.AddYears(-65))
            .LessThan(DateTime.Now.AddYears(-18))
            .WithMessage("Podano nieprawidłową datę urodzenia użytkownika.");

        RuleFor(x => x.UserInfo.IdDepartment)
            .GreaterThan(0)
            .WithMessage("Nie podano działu.");

        RuleFor(x => x.UserInfo.IdRole)
            .GreaterThan(0)
            .WithMessage("Nie podano roli.");

        RuleFor(x => x.UserInfo.Phone)
            .NotNull()
            .NotEmpty()
            .Length(9)
            .WithMessage("Podano nieprawidłowy numer telefonu");

        RuleFor(x => x.UserInfo.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Podano nieprawidłowy email");

        RuleFor(x => x.UserInfo.IdPosition)
           .GreaterThan(0)
           .WithMessage("Nie podano stanowiska użytkownika.");

        RuleFor(x => x.UserInfo.PhotoDetails.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(2)
            .WithMessage("Podano nieprawidłową nazwę zdjęcia");

        RuleFor(x => x.UserInfo.PhotoDetails.IdUser)
            .GreaterThan(0)
            .WithMessage("Nie przypisano użytkownika.");

        RuleFor(x => x.UserInfo.Email)
            .Custom((value, context) =>
            {
                var user = dbContext.Users.FirstOrDefault(u => u.Email.Trim().ToLower() == value.Trim().ToLower());

                if (user is not null)
                    context.AddFailure(nameof(user.Email), "Taki adres email znajduje się już w bazie danych.");
            });

        RuleFor(x => x.UserInfo.Phone)
            .Custom((value, context) =>
            {
                var user = dbContext.Users.FirstOrDefault(u => u.Phone.Trim().ToLower() == value.Trim().ToLower());

                if (user is not null)
                    context.AddFailure(nameof(user.Phone), "Taki numer telefonu znajduje się już w bazie danych.");
            });
    }
}
