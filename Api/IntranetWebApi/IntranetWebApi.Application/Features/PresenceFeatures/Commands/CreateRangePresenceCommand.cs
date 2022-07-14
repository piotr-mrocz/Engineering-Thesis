using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Application.Helpers;
using IntranetWebApi.Domain.Consts;
using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.PresenceFeatures.Commands;

public class CreateRangePresenceCommand : IRequest<BaseResponse>
{
    public PresenceToAddRangeDto PresenceInfo { get; set; } = null!;
}

public class CreateRangePresenceHandler : IRequestHandler<CreateRangePresenceCommand, BaseResponse>
{
    private readonly IGenericRepository<Presence> _presenceRepo;

    public CreateRangePresenceHandler(IGenericRepository<Presence> presenceRepo)
    {
        _presenceRepo = presenceRepo;
    }

    public async Task<BaseResponse> Handle(CreateRangePresenceCommand request, CancellationToken cancellationToken)
    {
        var listPresencesToAdd = new List<Presence>();
        var freeDays = DateTimeHelper.GetFreeDays(DateTime.Now.Year);
        var freeDaysList = freeDays.Select(x => x.FreeDay);

        for (var day = request.PresenceInfo.StartDate.Date; day <= request.PresenceInfo.EndDate.Date; day = day.Date.AddDays(1))
        {
            if (day.DayOfWeek == DayOfWeek.Saturday || day.DayOfWeek == DayOfWeek.Sunday || freeDaysList.Contains(day.Date))
                continue;

            listPresencesToAdd.Add(new Presence()
            {
                Date = day.Date,
                StartTime = new TimeSpan(WorkTimeConst.StartWorkHour, 0, 0),
                EndTime = new TimeSpan(WorkTimeConst.EndWorkHour, 0, 0),
                IdUser = request.PresenceInfo.IdUser,
                IsPresent = request.PresenceInfo.IsPresent,
                AbsenceReason = request.PresenceInfo.AbsenceReason,
                WorkHours = WorkTimeConst.OfficialWorkHoursPerDay,
                ExtraWorkHours = 0
            });
        }

        var response = await _presenceRepo.CreateRangeEntities(listPresencesToAdd, cancellationToken);

        return new BaseResponse()
        {
            Succeeded = response.Succeeded,
            Message = response.Succeeded ? response.Message : "Błąd! Nie udało się dodać obecności!"
        };
    }
}
