using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.PhotoFeatures.Queries;
public class GetUserPhotoNameQuery : IRequest<ResponseStruct<string>>
{
    public int IdUser { get; set; }
}

public class GetUserPhotoNameHandler : IRequestHandler<GetUserPhotoNameQuery, ResponseStruct<string>>
{
    private readonly IGenericRepository<Photo> _photoRepo;

    public GetUserPhotoNameHandler(IGenericRepository<Photo> photoRepo)
    {
        _photoRepo = photoRepo;
    }

    public async Task<ResponseStruct<string>> Handle(GetUserPhotoNameQuery request, CancellationToken cancellationToken)
    {
        var photo = await _photoRepo.GetEntityByExpression(x => x.IdUser == request.IdUser, cancellationToken);

        var namePhoto = photo.Succeeded && photo.Data != null
            ? photo.Data.Name
            : string.Empty;

        return new ResponseStruct<string>()
        {
            Succeeded = !string.IsNullOrEmpty(namePhoto),
            Message = !string.IsNullOrEmpty(namePhoto)
                      ? string.Empty
                      : "Nie odnaleziono zdjęcia",
            Data = namePhoto
        };
    }
}
