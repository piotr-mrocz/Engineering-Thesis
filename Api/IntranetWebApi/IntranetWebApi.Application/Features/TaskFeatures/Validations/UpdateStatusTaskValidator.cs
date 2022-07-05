using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace IntranetWebApi.Application.Features.TaskFeatures.Validations;

public class UpdateStatusTaskValidator : AbstractValidator<UpdateStatusTaskCommand>
{
    public UpdateStatusTaskValidator()
    {
        RuleFor(x => x.Status)
            .NotNull()
            .GreaterThan(0)
            .WithMessage("Nie podano nowego statusu wykonania!");

        RuleFor(x => x.IdTask)
            .NotNull()
            .GreaterThan(0)
            .WithMessage("Nie podano numeru zadania!");
    }
}
