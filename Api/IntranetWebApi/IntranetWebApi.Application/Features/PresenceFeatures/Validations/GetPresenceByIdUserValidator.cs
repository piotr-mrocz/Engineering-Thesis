using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using IntranetWebApi.Application.Features.PresenceFeatures.Queries;

namespace IntranetWebApi.Application.Features.PresenceFeatures.Validations;

public class GetPresenceByIdUserValidator : AbstractValidator<GetPresenceByIdUserQuery>
{
    public GetPresenceByIdUserValidator()
    {
        RuleFor(x => x.IdUser)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(0)
            .WithMessage("Nie wybrano użytkownika");

        RuleFor(x => x.Date)
            .Custom((value, context) =>
            {
                if (value.HasValue)
                {
                    if (value.Value >= DateTime.Now)
                    {
                        context.AddFailure("Date", "Niepoprawna data ");
                    }
                }
            });

        RuleFor(x => x.StartDate)
            .Custom((value, context) =>
            {
                if (value.HasValue)
                {
                    if (value.Value >= DateTime.Now)
                    {
                        context.AddFailure("Date", "Niepoprawna data ");
                    }
                }
            });

        RuleFor(x => x.EndDate)
            .Custom((value, context) =>
            {
                if (value.HasValue)
                {
                    if (value.Value >= DateTime.Now)
                    {
                        context.AddFailure("Date", "Niepoprawna data ");
                    }
                }
            });
    }
}
