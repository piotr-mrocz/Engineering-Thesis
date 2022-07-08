using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Application.Helpers;
using IntranetWebApi.Domain.Consts;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Interfaces;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.UserFeatures.Commands;

public class ChangeUserPasswordCommand : IRequest<BaseResponse>
{
    public int IdUser { get; set; }
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string ConfirmNewPassword { get; set; } = null!;
}

public class ChangeUserPasswordHandler : IRequestHandler<ChangeUserPasswordCommand, BaseResponse>
{
    private readonly IGenericRepository<User> _userRepo;

    public ChangeUserPasswordHandler(IGenericRepository<User> userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<BaseResponse> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepo.GetEntityByExpression(x => x.Id == request.IdUser, cancellationToken);

        if (user == null || !user.Succeeded || user.Data == null)
        {
            return new BaseResponse()
            {
                Message = "Nie udało się odnaleźć użytkownika w bazie danych!"
            };
        }

        request.NewPassword = RemoveWhitespace(request.NewPassword);
        request.ConfirmNewPassword = RemoveWhitespace(request.ConfirmNewPassword);

        var checkNewPasswordResponse = ComparePassword(request, user.Data.Password);

        if (!checkNewPasswordResponse.Succeeded)
            return checkNewPasswordResponse;

        var newPasswordHash = PasswordHelper.SecurePassword(request.NewPassword);

        user.Data.Password = newPasswordHash;

        var response = await _userRepo.UpdateEntity(user.Data, cancellationToken);

        return new BaseResponse()
        {
            Succeeded = response.Succeeded,
            Message = response.Succeeded ? "Operacja zakończona pomyślnie" : "Nie udało się zmienić hasła!"
        };
    }

    public string RemoveWhitespace(string input)
    {
        return new string(input.ToCharArray()
            .Where(c => !char.IsWhiteSpace(c))
            .ToArray());
    }

    private BaseResponse ComparePassword(ChangeUserPasswordCommand request, string userPassword)
    {
        if (!PasswordHelper.ValidatePassword(request.OldPassword, userPassword))
        {
            return new BaseResponse()
            {
                Message = "Wprowadzono niepoprawne stare hasło!"
            };
        }

        if (PasswordHelper.ValidatePassword(request.NewPassword, userPassword))
        {
            return new BaseResponse()
            {
                Message = "Nowe hasło musi być inne od starego!"
            };
        }

        if (request.NewPassword != request.ConfirmNewPassword)
        {
            return new BaseResponse()
            {
                Message = "Nowe hasło i potwierdzenie hasła muszą być takie same!"
            };
        }

        if (!request.NewPassword.Any(char.IsUpper))
        {
            return new BaseResponse()
            {
                Message = "Nowe hasło musi zawierać przynajmniej jedną wielką literę!"
            };
        }

        if (!request.NewPassword.Any(char.IsDigit))
        {
            return new BaseResponse()
            {
                Message = "Nowe hasło musi zawierać przynajmniej jedną cyfrę!"
            };
        }

        if (request.NewPassword.Length < 6)
        {
            return new BaseResponse()
            {
                Message = "Nowe hasło musi zawierać przynajmniej sześć znaków!"
            };
        }

        if (!request.NewPassword.Any(x => PasswordSpecialCharConst.PossibleChars.Contains(x)))
        {
            return new BaseResponse()
            {
                Message = "Nowe hasło musi zawierać przynajmniej jeden znak specjalny!"
            };
        }

        return new BaseResponse()
        {
            Succeeded = true
        };
    }
}
