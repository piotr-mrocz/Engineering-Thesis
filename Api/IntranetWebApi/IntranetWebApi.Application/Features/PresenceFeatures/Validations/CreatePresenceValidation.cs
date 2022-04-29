using FluentValidation;
using IntranetWebApi.Data;
using IntranetWebApi.Domain.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Application.Features.PresenceFeatures.Validations;

public class CreatePresenceValidation : AbstractValidator<PresenceDto>
{
    public CreatePresenceValidation(IntranetDbContext dbContext)
    {
        RuleFor(x => x.Date)
            .NotEmpty()
            .NotNull()
            .NotEqual(DateTime.Now.Date)
            .WithMessage("Nieprawidłowy format daty. Data musi być dzisiejsza!");

        RuleFor(x => x.StartTime)
            .NotEmpty()
            .NotNull()
            .WithMessage("Nie podano czasu rozpoczęcia pracy");

        RuleFor(x => x.EndTime)
            .NotEmpty()
            .NotNull()
            .WithMessage("Nie podano czasu zakończenia pracy");

        RuleFor(x => x.IdUser)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Nie podano użytkownika");

        RuleFor(x => x.IsPresent)
            .NotNull()
            .NotEmpty()
            .WithMessage("Należy zaznaczyć obecność użytkownika");

        RuleFor(x => x.WorkHours)
            .NotNull()
            .NotEmpty()
            .LessThanOrEqualTo(8)
            .WithMessage("Maksymalna ilość godzin pracy wynosi 8");

        RuleFor(x => x.ExtraWorkHours)
            .NotNull()
            .NotEmpty()
            .LessThanOrEqualTo(4)
            .WithMessage("Maksymalna ilość nadgodzin wynosi 4");

        RuleFor(x => x.WorkHours + x.ExtraWorkHours)
            .LessThanOrEqualTo(12)
            .WithMessage("Pracownik maksymalnie może pracować 12 godzin");

        RuleFor(x => x.IdUser)
            .Custom((value, context) =>
            {
                var today = DateTime.Today;
                var userPresence = dbContext.Presences.Any(u => u.IdUser == value && u.Date == today);

                if (userPresence)
                {
                    context.AddFailure(nameof(value), "Taki wpis już istnieje w bazie danych");
                }
            });
    }
}
