using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.UserFeatures.Commands;

public class UpdateUserDataCommand : IRequest<BaseResponse>
{
    public UserToUpdateDto UserInfo { get; set; } = new();
}

public class UpdateUserDataHandler : IRequestHandler<UpdateUserDataCommand, BaseResponse>
{
    private readonly IGenericRepository<User> _userRepo;
    private readonly IGenericRepository<Photo> _photoRepo;

    public UpdateUserDataHandler(IGenericRepository<User> userRepo, IGenericRepository<Photo> photoRepo)
    {
        _userRepo = userRepo;
        _photoRepo = photoRepo;
    }

    public async Task<BaseResponse> Handle(UpdateUserDataCommand request, CancellationToken cancellationToken)
    {
        var updateUserResponse = await UpdateUser(request, cancellationToken);

        if (!updateUserResponse.Succeeded)
            return updateUserResponse;

        var updatePhotoResponse = await UpdatePhoto(request, cancellationToken);

        return updatePhotoResponse;
    }

    private async Task<BaseResponse> UpdateUser(UpdateUserDataCommand request, CancellationToken cancellationToken)
    {
        var userToChange = await _userRepo.GetEntityByExpression(x => x.Id == request.UserInfo.IdUser, cancellationToken);

        if (userToChange == null || !userToChange.Succeeded || userToChange.Data == null)
        {
            return new BaseResponse()
            {
                Message = "Nie odnaleziono użytkownika w bazie danych!"
            };
        }

        userToChange.Data.FirstName = request.UserInfo.FirstName;
        userToChange.Data.LastName = request.UserInfo.LastName;
        userToChange.Data.IdDepartment = request.UserInfo.IdDepartment;
        userToChange.Data.IdRole = request.UserInfo.IdRole;
        userToChange.Data.Phone = request.UserInfo.Phone;
        userToChange.Data.Email = request.UserInfo.Email;
        userToChange.Data.IdPosition = request.UserInfo.IdPosition;

        var response = await _userRepo.UpdateEntity(userToChange.Data, cancellationToken);

        if (response == null || !response.Succeeded)
        {
            return new BaseResponse()
            {
                Message = "Edycja użytkownika nie powiodła się!"
            };
        }

        return new BaseResponse()
        {
            Succeeded = response.Succeeded,
            Message = response.Succeeded ? "Operacja zakończona powodzeniem" : "Edycja użytkownika nie powiodła się!"
        };
    }

    public async Task<BaseResponse> UpdatePhoto(UpdateUserDataCommand request, CancellationToken cancellationToken)
    {
        var photo = await _photoRepo.GetEntityByExpression(x => x.IdUser == request.UserInfo.IdUser, cancellationToken);

        if (photo == null || !photo.Succeeded || photo.Data == null)
        {
            var newPhoto = new Photo()
            {
                IdUser = request.UserInfo.IdUser,
                Name = request.UserInfo.PhotoName
            };

            var addResponse = await _photoRepo.CreateEntity(newPhoto, cancellationToken);

            if (addResponse == null || !addResponse.Succeeded)
            {
                return new BaseResponse()
                {
                    Message = "Nie udało się zapisać zdjęcia!"
                };
            }
        }

        return new BaseResponse()
        {
            Succeeded = true,
            Message = "Operacja zakończona pomyślnie"
        };
    }
}
