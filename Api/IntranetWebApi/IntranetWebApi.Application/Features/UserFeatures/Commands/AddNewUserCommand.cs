using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Application.Helpers;
using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Interfaces;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.UserFeatures.Commands;

public class AddNewUserCommand : IRequest<BaseResponse>
{
    public UserToAddDto UserInfo { get; set; } = new();
}

public class AddNewUserHandler : IRequestHandler<AddNewUserCommand, BaseResponse>
{
    private readonly IGenericRepository<User> _userRepo;
    private readonly IGenericRepository<Photo> _photoRepo;

    public AddNewUserHandler(IGenericRepository<User> userRepo, IGenericRepository<Photo> photoRepo)
    {
        _userRepo = userRepo;
        _photoRepo = photoRepo;
    }

    public async Task<BaseResponse> Handle(AddNewUserCommand request, CancellationToken cancellationToken)
    {
        var newLogin = await GenerateNewLogin(request.UserInfo.FirstName, request.UserInfo.LastName, cancellationToken);
        var userPasswords = GeneratePassword();

        var addUserResult = await AddNewUser(request, newLogin, userPasswords.hashPassword, cancellationToken);

        if (!addUserResult.Succeeded)
        {
            return new BaseResponse()
            {
                Message = addUserResult.Message
            };
        }

        var addPhotoResult = await AddUserPhoto(request.UserInfo.PhotoName, addUserResult.Data, cancellationToken);

        return new BaseResponse()
        {
            Succeeded = addPhotoResult.Succeeded,
            Message = addPhotoResult.Succeeded 
            ? $"{addPhotoResult.Message} Login: {newLogin}{Environment.NewLine}Hasło: {userPasswords.password}"
            : addPhotoResult.Message
        };
    }

    private async Task<string> GenerateNewLogin(string name, string lastName, CancellationToken cancellationToken)
    {
        var loginForNewUser = $"{name.ToLower()[0]}.{lastName.Trim().ToLower()}";

        loginForNewUser = loginForNewUser
            .Replace("ą", "a")
            .Replace("ę", "e")
            .Replace("ć", "c")
            .Replace("ł", "l")
            .Replace("ó", "o")
            .Replace("ś", "s")
            .Replace("ż", "z")
            .Replace("ź", "z");

        var usersWithTheSameLogin = await _userRepo.GetManyEntitiesByExpression(x => x.Login.Trim().ToLower() == loginForNewUser, cancellationToken);

        if (usersWithTheSameLogin is not null && usersWithTheSameLogin.Succeeded &&
            usersWithTheSameLogin.Data is not null && usersWithTheSameLogin.Data.Any())
        {
            var lastUserWithTheSameLogin = usersWithTheSameLogin.Data
                .OrderByDescending(x => x.Id)
                .FirstOrDefault();

            var lastChar = lastUserWithTheSameLogin.Login.Trim()[lastUserWithTheSameLogin.Login.Trim().Length - 1];
            loginForNewUser = Char.IsDigit(lastChar)
                ? $"{loginForNewUser}{lastChar++}"
                : $"{loginForNewUser}{1}";
        }

        return loginForNewUser;
    }

    private (string password, string hashPassword) GeneratePassword()
    {
        var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[8];
        var random = new Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        var finalString = new String(stringChars);
        var hashedPassword = PasswordHelper.SecurePassword(finalString);

        return (password: finalString, hashPassword: hashedPassword);
    }

    private async Task<ResponseStruct<int>> AddNewUser(AddNewUserCommand request, string login, string hashPassword, CancellationToken cancellationToken)
    {
        var newUser = new User()
        {
            FirstName = request.UserInfo.FirstName,
            LastName = request.UserInfo.LastName,
            DateOfBirth = request.UserInfo.DateOfBirth,
            DateOfEmployment = DateTime.Now,
            DateOfRelease = null,
            IdDepartment = request.UserInfo.IdDepartment,
            Login = login,
            Password = hashPassword,
            IdRole = request.UserInfo.IdRole,
            IsActive = true,
            Phone = request.UserInfo.Phone,
            Email = request.UserInfo.Email,
            IdPosition = request.UserInfo.IdPosition
        };

        var addUserResult = await _userRepo.CreateEntity(newUser, cancellationToken);

        return addUserResult;
    }

    private async Task<BaseResponse> AddUserPhoto(string photoName, int idUser, CancellationToken cancellationToken)
    {
        var newPhoto = new Photo()
        {
            Name = photoName,
            IdUser = idUser
        };

        var addPhotoResult = await _photoRepo.CreateEntity(newPhoto, cancellationToken);

        return new BaseResponse()
        {
            Succeeded = addPhotoResult.Succeeded,
            Message = addPhotoResult.Succeeded ? "Poprawnie dodano użytkownika." : addPhotoResult.Message
        };
    }
}
