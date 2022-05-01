using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Application.Helpers;
using IntranetWebApi.Domain.Enums;
using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Domain.Models.Entities.Views;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Entities;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.PresenceFeatures.Queries;

public class GetUsersPresencePerDayQuery : IRequest<Response<UsersPresencesPerDayDto>>
{
    public DateTime Date { get; set; }
}

public class GetUsersPresencePerDayHandler : IRequestHandler<GetUsersPresencePerDayQuery, Response<UsersPresencesPerDayDto>>
{
    private readonly IGenericRepository<VUsersPresence> _vUsersPresenceRepo;

    public GetUsersPresencePerDayHandler(IGenericRepository<VUsersPresence> vUsersPresenceRepo)
    {
        _vUsersPresenceRepo = vUsersPresenceRepo;
    }

    public async Task<Response<UsersPresencesPerDayDto>> Handle(GetUsersPresencePerDayQuery request, CancellationToken cancellationToken)
    {
        var date = request.Date.Date;
        var vUsersPresences = await _vUsersPresenceRepo.GetManyEntitiesByExpression(x => x.Date == date, cancellationToken);

        if (!vUsersPresences.Succeeded || vUsersPresences.Data is null || !vUsersPresences.Data.Any())
        {
            return new Response<UsersPresencesPerDayDto>()
            {
                Message = $"Nie odnaleziono żadnych wpisów obecności w dniu {request.Date.Date}",
                Data = new UsersPresencesPerDayDto()
            };
        }

        var response = GetUsersPresencesPerDayDto(vUsersPresences.Data);

        return new Response<UsersPresencesPerDayDto>()
        {
            Succeeded = true,
            Data = response
        };
    }

    private UsersPresencesPerDayDto GetUsersPresencesPerDayDto(IEnumerable<VUsersPresence> vUsersPresences)
    {
        if (!vUsersPresences.Any())
            return new UsersPresencesPerDayDto();

        var vUsersPresenceList = new List<UserPresentsPerDayDto>();

        foreach (var vPresence in vUsersPresences)
        {
            var record = new UserPresentsPerDayDto()
            {
                StartTime = vPresence.StartTime,
                EndTime = vPresence.EndTime,
                UserName = vPresence.UserName,
                IsPresent = vPresence.IsPresent,
                AbsenceReason = vPresence.AbsenceReason.HasValue
                              ? EnumHelper.GetEnumDescription((AbsenceReasonsEnum)vPresence.AbsenceReason.Value)
                              : string.Empty,
                WorkHours = vPresence.WorkHours,
                ExtraWorkHours = vPresence.ExtraWorkHours
            };

            vUsersPresenceList.Add(record);
        }

        return new UsersPresencesPerDayDto()
        {
            UsersPresencesList = vUsersPresenceList
        };
    }
}
