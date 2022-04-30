using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using IntranetWebApi.Application.Features.PresenceFeatures.Queries;

namespace IntranetWebApi.Application.Features.PresenceFeatures.Validations;

public class GetUsersPresencePerDayValidation : AbstractValidator<GetUsersPresencePerDayQuery>
{
    public GetUsersPresencePerDayValidation()
    {
        RuleFor(x => x.Date)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(DateTime.Now.Date)
            .WithMessage("Nieprawidłowa data!");
    }
}
