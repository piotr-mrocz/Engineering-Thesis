using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using IntranetWebApi.Application.Features.PresenceFeatures.Queries;

namespace IntranetWebApi.Application.Features.PresenceFeatures.Validations;

public class GetPresenceByIdUserValidator : AbstractValidator<GetPresencesUsersPerMonthQuery>
{
    public GetPresenceByIdUserValidator()
    {
        RuleFor(x => x.IdUser)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Nie wybrano użytkownika");

        RuleFor(x => x.MonthNumber)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Nie wybrano miesiąca");

        RuleFor(x => x.Year)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Nie wybrano miesiąca");
    }
}
