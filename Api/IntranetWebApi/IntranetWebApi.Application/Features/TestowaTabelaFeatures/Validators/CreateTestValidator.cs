using FluentValidation;
using IntranetWebApi.Data;
using IntranetWebApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Application.Features.TestowaTabelaFeatures.Validators
{
    public class CreateTestValidator : AbstractValidator<Test>
    {
        public CreateTestValidator(IntranetDbContext dbContext)
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Number)
                .NotEmpty();

            RuleFor(x => x.Name)
                .Custom((value, context) =>
                {
                    var test = dbContext.TestowaTabela.Any(u => u.Name == value);

                    if (test)
                    {
                        context.AddFailure("Name", "That name is taken");
                    }
                });
        }
    }
}
