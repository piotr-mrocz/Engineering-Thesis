using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace IntranetWebApi.Application.Features.TaskFeatures.Validations;

public class UpdateTaskValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskValidator()
    {
        RuleFor(x => x.NewTitle)
            .NotNull()
            .NotEmpty()
            .WithMessage("Podano niepoprawny tytuł zadania!");

        RuleFor(x => x.IdTask)
            .NotNull()
            .GreaterThan(0)
            .WithMessage("Nie podano numeru zadania!");

        RuleFor(x => x.Status)
            .NotNull()
            .GreaterThan(0)
            .WithMessage("Nie podano nowego statusu wykonania!");

        RuleFor(x => x.Priority)
            .NotNull()
            .GreaterThan(0)
            .WithMessage("Nie podano ważności zadania!");
    }
}
