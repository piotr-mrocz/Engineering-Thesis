using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Application.Helpers;
using IntranetWebApi.Domain.Enums;
using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Entities;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.PresenceFeatures.Queries;

public class GetUsersPresencePerDayQuery : IRequest<Response<UsersPresencesPerDayDto>>
{
    public int Day { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
}

public class GetUsersPresencePerDayHandler : IRequestHandler<GetUsersPresencePerDayQuery, Response<UsersPresencesPerDayDto>>
{
    private readonly IGenericRepository<Presence> _presenceRepo;
    private readonly IGenericRepository<User> _userRepo;

    public GetUsersPresencePerDayHandler(IGenericRepository<Presence> presenceRepo, IGenericRepository<User> userRepo)
    {
        _presenceRepo = presenceRepo;
        _userRepo = userRepo;
    }

    public async Task<Response<UsersPresencesPerDayDto>> Handle(GetUsersPresencePerDayQuery request, CancellationToken cancellationToken)
    {
        var date =new DateTime(request.Year, request.Month, request.Day);

        var users = await _userRepo.GetManyEntitiesByExpression(x => x.IsActive && !x.DateOfRelease.HasValue, cancellationToken);

        if (users is null || !users.Succeeded || users.Data is null || !users.Data.Any())
        {
            return new Response<UsersPresencesPerDayDto>()
            {
                Message = "Nie znaleziono użytkowników w bazie danych",
                Data = new()
            };
        }

        var usersIds = users.Data.Select(x => x.Id).ToList();

        var presences = await _presenceRepo.GetManyEntitiesByExpression(x => x.Date.Date == date && usersIds.Contains(x.IdUser), cancellationToken);

        if (presences is null || !presences.Succeeded || presences.Data is null || !presences.Data.Any())
        {
            return new Response<UsersPresencesPerDayDto>()
            {
                Message = "Nie ma żadnych obecności w bazie danych",
                Data = new()
            };
        }

        var presenceUsersListDto = GetUsersPresencesPerDayDto(users.Data, presences.Data);

        return new Response<UsersPresencesPerDayDto>()
        {
            Succeeded = true,
            Data = new UsersPresencesPerDayDto()
            {
                UsersPresencesList = presenceUsersListDto
            }
        };
    }

    private List<UserPresentsPerDayDto> GetUsersPresencesPerDayDto(IEnumerable<User> users, IEnumerable<Presence> presences)
    {
        var presenceUsersListDto = new List<UserPresentsPerDayDto>();

        foreach (var user in users)
        {
            var presence = presences.FirstOrDefault(x => x.IdUser == user.Id);
            var absenceInfo = GetAbsenceReason(presence);

            var rekord = new UserPresentsPerDayDto()
            {
                UserName = $"{user.FirstName} {user.LastName}",
                IsPresent = presence != null ? presence.IsPresent : false,
                PresentType = absenceInfo.presentType,
                AbsenceReason = absenceInfo.absenceDescription,
                StartTime = presence != null ? presence.StartTime.ToString("h'h 'm'm") : "Brak odbicia",
                EndTime = presence != null
                        ? presence.EndTime.HasValue

                            ? presence.EndTime.Value.ToString("h'h 'm'm 's's'")
                            : "Pracuje"
                        : "Brak odbicia"
            };
        }

        return presenceUsersListDto;
    }

    private (string absenceDescription, int presentType) GetAbsenceReason(Presence presence)
    {
        if (presence == null)
            return (EnumHelper.GetEnumDescription(AbsenceReasonsEnum.UnauthorizedAbsence), (int)AbsenceReasonsEnum.UnauthorizedAbsence);

        if (presence.IsPresent)
            return (EnumHelper.GetEnumDescription(AbsenceReasonsEnum.Present), (int)AbsenceReasonsEnum.Present);

        if (presence.AbsenceReason.HasValue)
            return (EnumHelper.GetEnumDescription((AbsenceReasonsEnum)presence.AbsenceReason), (int)presence.AbsenceReason);

        return ("Brak danych!", 0);
    }
}
