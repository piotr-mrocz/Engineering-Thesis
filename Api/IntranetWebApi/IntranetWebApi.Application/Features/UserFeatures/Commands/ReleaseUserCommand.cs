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

    public class ReleaseUserCommand : IRequest<BaseResponse>
    {
    public int IdUser { get; set; }
    }

public class ReleaseUserHandler : IRequestHandler<ReleaseUserCommand, BaseResponse>
{
    private readonly IGenericRepository<User> _userRepo;

    public ReleaseUserHandler(IGenericRepository<User> userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<BaseResponse> Handle(ReleaseUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepo.GetEntityByExpression(x => x.Id == request.IdUser, cancellationToken);

        if (user == null || !user.Succeeded || user.Data == null)
        {
            return new BaseResponse()
            {
                Message = "Nie udało się odnaleźć użytkownika. Procedura wstrzymana!"
            };
        }

        if (!user.Data.IsActive)
        {
            return new BaseResponse()
            {
                Message = "Użytkownik już u nas nie pracuje. Procedura wstrzymana!"
            };
        }

        user.Data.IsActive = false;
        user.Data.DateOfRelease = DateTime.Now;

        var response = await _userRepo.UpdateEntity(user.Data, cancellationToken);

        return new BaseResponse()
        {
            Succeeded = response.Succeeded,
            Message = response.Succeeded ? "Operacja zakończona pomyślnie" : "Wystąpił błąd podczas próby zwolnienia użytkownika!"
        };
    }
}
