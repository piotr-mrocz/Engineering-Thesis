using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using IntranetWebApi.Data;

namespace IntranetWebApi.Application.Features.TaskFeatures.Validations;

public class AddNewTaskValidator : AbstractValidator<AddNewTaskCommand>
{
    public AddNewTaskValidator()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty()
            .WithMessage("Podano niepoprawny tytuł zadania");

        RuleFor(x => x.IdUser)
            .NotNull()
            .GreaterThan(0)
            .WithMessage("Nie podano użytkownika, który ma wykonać to zadanie");

        RuleFor(x => x.Priority)
            .NotNull()
            .GreaterThan(0)
            .WithMessage("Nie podano ważności zadania");
    }
}
