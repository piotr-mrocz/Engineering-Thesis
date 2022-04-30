using FluentValidation;
using IntranetWebApi.Application.Features.PresenceFeatures.Commands;

namespace IntranetWebApi.Application.Features.PresenceFeatures.Validations;

public class UpdatePresenceValidator : AbstractValidator<UpdatePresenceCommand>
{
    public UpdatePresenceValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Nie podano numeru pozycji!");

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
    }
}
