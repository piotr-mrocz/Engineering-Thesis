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

namespace IntranetWebApi.Application.Features.ImportantInfoFeatures.Queries;

public class GetImportantInfoQuery : IRequest<Response<List<GetImportantInfoDto>>>
{
}

public class GetImportantInfoHandler : IRequestHandler<GetImportantInfoQuery, Response<List<GetImportantInfoDto>>>
{
    public IGenericRepository<ImportantInfo> _infoRepo;
    public IGenericRepository<User> _userRepo;

    public GetImportantInfoHandler(IGenericRepository<ImportantInfo> infoRepo, IGenericRepository<User> userRepo)
    {
        _infoRepo = infoRepo;
        _userRepo = userRepo;
    }

    public async Task<Response<List<GetImportantInfoDto>>> Handle(GetImportantInfoQuery request, CancellationToken cancellationToken)
    {
        var today = DateTime.Now;

        var infos = await _infoRepo.GetManyEntitiesByExpression(x =>
                x.StartDate.Date >= today.Date && x.EndDate.Date <= today.Date, cancellationToken);

        if (infos == null || !infos.Succeeded || infos.Data == null || !infos.Data.Any())
        {
            return new Response<List<GetImportantInfoDto>>()
            {
                Message = "Nie ma żadnych ważnych informacji",
                Data = new()
            };
        }
        
        var usersIds = infos.Data.Select(x => x.IdWhoAdded).ToList();

        var users = await _userRepo.GetManyEntitiesByExpression(x => usersIds.Contains(x.Id), cancellationToken);

        if (users == null || !users.Succeeded || users.Data == null || !users.Data.Any())
        {
            return new Response<List<GetImportantInfoDto>>()
            {
                Message = "Nie odnaleziono informacji na temat użytkowników!",
                Data = new()
            };
        }

        var infoList = new List<GetImportantInfoDto>();

        foreach (var info in infos.Data)
        {
            var user = users.Data.FirstOrDefault(x => x.Id == info.IdWhoAdded);

            var infoDetails = new GetImportantInfoDto()
            {
                Info = info.Info,
                UserName = user != null ? $"{user.FirstName} {user.LastName}" : "Brak informacji"
            };

            infoList.Add(infoDetails);
        }

        return new Response<List<GetImportantInfoDto>>()
        {
            Succeeded = true,
            Data = infoList
        };
    }
}
