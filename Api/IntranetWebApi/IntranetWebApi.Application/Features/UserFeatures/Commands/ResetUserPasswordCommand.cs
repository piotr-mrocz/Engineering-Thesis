using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Application.Helpers;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.UserFeatures.Commands;

public class ResetUserPasswordCommand : IRequest<BaseResponse>
{
    public int IdUser { get; set; }
}

public class ResetUserPasswordHandler : IRequestHandler<ResetUserPasswordCommand, BaseResponse>
{
    private readonly IGenericRepository<User> _userRepo;

    public ResetUserPasswordHandler(IGenericRepository<User> userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<BaseResponse> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepo.GetEntityByExpression(x => x.Id == request.IdUser, cancellationToken);

        if (user == null || !user.Succeeded || user.Data == null)
        {
            return new BaseResponse()
            {
                Message = "Nie udało się odnaleźć użytkownika w bazie danych!"
            };
        }

        var userPasswords = PasswordHelper.GeneratePassword();

        user.Data.Password = userPasswords.hashPassword;

        var response = await _userRepo.UpdateEntity(user.Data, cancellationToken);

        return new BaseResponse()
        {
            Succeeded = response.Succeeded,
            Message = response.Succeeded 
                    ? $"Operacja zakończona pomyślnie. {Environment.NewLine}Hasło: {userPasswords.password}" 
                    : "Nie udało się zmienić hasła!"
        };
    }
}
