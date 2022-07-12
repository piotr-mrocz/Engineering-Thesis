using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Application.Helpers;
using IntranetWebApi.Domain.Enums;
using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.PresenceFeatures.Queries;

public class GetPresencesUsersPerMonthQuery : IRequest<Response<GetPresenceByIdUserListDto>>
{
    public int IdUser { get; set; }
    public int MonthNumber { get; set; }
    public int Year { get; set; }
}

public class GetPresencesUsersPerMonthHandler : IRequestHandler<GetPresencesUsersPerMonthQuery, Response<GetPresenceByIdUserListDto>>
{
    private readonly IGenericRepository<Presence> _presenceRepo;

    public GetPresencesUsersPerMonthHandler(IGenericRepository<Presence> presenceRepo)
    {
        _presenceRepo = presenceRepo;
    }

    public async Task<Response<GetPresenceByIdUserListDto>> Handle(GetPresencesUsersPerMonthQuery request, CancellationToken cancellationToken)
    {
        var today = DateTime.Now.Date;

        var presences = await _presenceRepo.GetManyEntitiesByExpression(x => 
                x.Date.Month == request.MonthNumber && 
                x.Date.Year == request.Year &&
                x.Date.Day <= today.Day &&
                x.IdUser == request.IdUser, cancellationToken);

        if (presences is null || !presences.Succeeded || presences.Data is null || !presences.Data.Any())
        {
            return new Response<GetPresenceByIdUserListDto>()
            {
                Message = "Nie ma żadnych obecności w bazie danych",
                Data = new()
            };
        }

        var response = GetUsersPresencesPerDayDto(presences.Data, request.MonthNumber, request.Year);

        return new Response<GetPresenceByIdUserListDto>()
        {
            Succeeded = true,
            Data = response
        };
    }

    private GetPresenceByIdUserListDto GetUsersPresencesPerDayDto(IEnumerable<Presence> presences, int month, int year)
    {
        var presenceUsersListDto = new List<GetPresenceByIdUserDto>();
        var firstDay = new DateTime(year, month, 1);
        var endDate = DateTime.Now.Date;
        var freeDaysVM = DateTimeHelper.GetFreeDays(year);
        var freeDays = freeDaysVM.Select(x => x.FreeDay).ToList();

        for (var day = firstDay.Date; day <= endDate; day = day.Date.AddDays(1))
        {
            var presence = presences.FirstOrDefault(x => x.Date.Date == day.Date);
            var absenceInfo = GetAbsenceReason(presence);

            var rekord = new GetPresenceByIdUserDto()
            {
                Date = day.Date.ToString("dd.MM.yyyy"),
                DayNumber = day.Day,
                IsPresent = presence != null ? presence.IsPresent : false,
                IsFreeDay = false,
                PresentType = absenceInfo.presentType,
                AbsenceReason = absenceInfo.absenceDescription,
                StartTime = presence != null ? presence.StartTime.ToString() : "Brak odbicia",
                EndTime = presence != null
                        ? presence.EndTime.HasValue
                            ? presence.EndTime.Value.ToString()
                            : "Pracuje"
                        : "Brak odbicia"
            };

            var freeDay = freeDaysVM.FirstOrDefault(x => x.FreeDay.Date == day.Date);

            if (freeDays.Contains(day))
            {
                rekord.IsPresent = false;
                rekord.IsFreeDay = true;
                rekord.PresentType = (int)AbsenceReasonsEnum.FreeDay;
                rekord.AbsenceReason = freeDay != null ? freeDay.FreeDayName : "Brak danych";
                rekord.StartTime = day.ToString("dd.MM.yyyy");
                rekord.EndTime = day.ToString("dd.MM.yyyy");
            }

            if ((day.DayOfWeek == DayOfWeek.Sunday || day.DayOfWeek == DayOfWeek.Saturday) && presence == null)
            {
                rekord.IsPresent = false;
                rekord.IsFreeDay = true;
                rekord.PresentType = (int)AbsenceReasonsEnum.Weekend;
                rekord.AbsenceReason = EnumHelper.GetEnumDescription(AbsenceReasonsEnum.Weekend);
                rekord.StartTime = day.ToString("dd.MM.yyyy");
                rekord.EndTime = day.ToString("dd.MM.yyyy");
            }

            if (day.DayOfWeek == DayOfWeek.Saturday && presence != null)
            {
                rekord.IsFreeDay = true;
                rekord.IsPresent = presence != null ? presence.IsPresent : false;
                rekord.PresentType = (int)AbsenceReasonsEnum.Present;
            }

            presenceUsersListDto.Add(rekord);
        }

        return new GetPresenceByIdUserListDto()
        {
            UserPresencesList = presenceUsersListDto,
            TotalWorkHour = presences.Sum(x => x.WorkHours),
            TotalWorkExtraHour = presences.Sum(x => x.ExtraWorkHours)
        };
    }

    private (string absenceDescription, int presentType) GetAbsenceReason(Presence presence)
    {
        if (presence == null)
            return (EnumHelper.GetEnumDescription(AbsenceReasonsEnum.UnauthorizedAbsence), (int)AbsenceReasonsEnum.UnauthorizedAbsence);

        if (presence.IsPresent)
            return (EnumHelper.GetEnumDescription(AbsenceReasonsEnum.Present), (int)AbsenceReasonsEnum.Present);

        if (presence.AbsenceReason.HasValue)
            return (EnumHelper.GetEnumDescription((AbsenceReasonsEnum)presence.AbsenceReason), (int)presence.AbsenceReason);

        return ("Brak danych!", default);
    }
}
