using FluentValidation;
using IntranetWebApi.Application.Features.PresenceFeatures.Commands;
using IntranetWebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Application.Features.PresenceFeatures.Validations;

public class CreateRangePresenceValidation : AbstractValidator<CreateRangePresencesCommand>
{
    public CreateRangePresenceValidation(IntranetDbContext dbContext)
    {
        RuleFor(x => x.ListOfPresences)
            .NotEmpty()
            .WithMessage("Nie podano żadnych danych!");

        RuleForEach(x => x.ListOfPresences.Select(x => x.Date))
            .NotNull()
            .NotEmpty()
            .WithMessage("");

        RuleForEach(x => x.ListOfPresences).SetValidator(new CreatePresenceValidation());
    }
}
