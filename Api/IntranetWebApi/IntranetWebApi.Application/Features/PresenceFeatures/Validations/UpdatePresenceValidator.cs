using FluentValidation;
using IntranetWebApi.Application.Features.PresenceFeatures.Commands;

namespace IntranetWebApi.Application.Features.PresenceFeatures.Validations;

public class UpdatePresenceValidator : AbstractValidator<UpdatePresenceCommand>
{
    public UpdatePresenceValidator()
    {
        RuleFor(x => x.PresenceToUpdate.Id)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Nie podano numeru pozycji!");

        RuleFor(x => x.PresenceToUpdate.IdUser)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Nie podano użytkownika");

        RuleFor(x => x.PresenceToUpdate.IsPresent)
            .NotNull()
            .NotEmpty()
            .WithMessage("Należy zaznaczyć obecność użytkownika");
    }
}
