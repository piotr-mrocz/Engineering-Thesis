using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.UserFeatures.Commands;

public class AddVacationDaysToNewUserCommand : IRequest<BaseResponse>
{
    public int IdUser { get; set; }
    public int VacationDays { get; set; }
}

public class AddVacationDaysToNewUserHandler : IRequestHandler<AddVacationDaysToNewUserCommand, BaseResponse>
{
    private readonly IGenericRepository<User> _userRepo;

    public AddVacationDaysToNewUserHandler(IGenericRepository<User> userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<BaseResponse> Handle(AddVacationDaysToNewUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepo.GetEntityByExpression(x => x.Id == request.IdUser, cancellationToken);

        if (user == null || !user.Succeeded || user.Data == null)
        {
            return new BaseResponse()
            {
                Message = "Nie odnaleziono użytkownika w bazie danych!"
            };
        }

        user.Data.VacationDaysThisYear = request.VacationDays;

        var response = await _userRepo.UpdateEntity(user.Data, cancellationToken);

        return new BaseResponse()
        {
            Succeeded = response.Succeeded,
            Message = response.Succeeded ? "Operacja zakończona powodzeniem" : "Edycja użytkownika nie powiodła się!"
        };
    }
}
