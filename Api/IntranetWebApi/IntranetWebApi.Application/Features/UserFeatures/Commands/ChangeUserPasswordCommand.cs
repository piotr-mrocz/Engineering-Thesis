using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Application.Helpers;
using IntranetWebApi.Domain.Consts;
using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Interfaces;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.UserFeatures.Commands;

public class ChangeUserPasswordCommand : IRequest<BaseResponse>
{
    public ChangeUserPasswordDto UserPasswordInfo { get; set; } = null!;
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
        var user = await _userRepo.GetEntityByExpression(x => x.Id == request.UserPasswordInfo.IdUser, cancellationToken);

        if (user == null || !user.Succeeded || user.Data == null)
        {
            return new BaseResponse()
            {
                Message = "Nie udało się odnaleźć użytkownika w bazie danych!"
            };
        }

        request.UserPasswordInfo.NewPassword = RemoveWhitespace(request.UserPasswordInfo.NewPassword);
        request.UserPasswordInfo.ConfirmNewPassword = RemoveWhitespace(request.UserPasswordInfo.ConfirmNewPassword);

        var checkNewPasswordResponse = ComparePassword(request, user.Data.Password);

        if (!checkNewPasswordResponse.Succeeded)
            return checkNewPasswordResponse;

        var newPasswordHash = PasswordHelper.SecurePassword(request.UserPasswordInfo.NewPassword);

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
        if (!PasswordHelper.ValidatePassword(request.UserPasswordInfo.OldPassword, userPassword))
        {
            return new BaseResponse()
            {
                Message = "Wprowadzono niepoprawne stare hasło!"
            };
        }

        if (PasswordHelper.ValidatePassword(request.UserPasswordInfo.NewPassword, userPassword))
        {
            return new BaseResponse()
            {
                Message = "Nowe hasło musi być inne od starego!"
            };
        }

        if (request.UserPasswordInfo.NewPassword != request.UserPasswordInfo.ConfirmNewPassword)
        {
            return new BaseResponse()
            {
                Message = "Nowe hasło i potwierdzenie hasła muszą być takie same!"
            };
        }

        if (!request.UserPasswordInfo.NewPassword.Any(char.IsUpper))
        {
            return new BaseResponse()
            {
                Message = "Nowe hasło musi zawierać przynajmniej jedną wielką literę!"
            };
        }

        if (!request.UserPasswordInfo.NewPassword.Any(char.IsDigit))
        {
            return new BaseResponse()
            {
                Message = "Nowe hasło musi zawierać przynajmniej jedną cyfrę!"
            };
        }

        if (request.UserPasswordInfo.NewPassword.Length < 6)
        {
            return new BaseResponse()
            {
                Message = "Nowe hasło musi zawierać przynajmniej sześć znaków!"
            };
        }

        if (!request.UserPasswordInfo.NewPassword.Any(x => PasswordSpecialCharConst.PossibleChars.Contains(x)))
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
