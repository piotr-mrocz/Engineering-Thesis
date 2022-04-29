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
        RuleForEach(x => x.ListOfPresences).SetValidator(new CreatePresenceValidation(dbContext));
    }
}
