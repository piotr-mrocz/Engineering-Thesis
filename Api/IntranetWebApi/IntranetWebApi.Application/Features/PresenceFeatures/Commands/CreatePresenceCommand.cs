using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Application.Helpers;
using IntranetWebApi.Domain.Consts;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.PresenceFeatures.Commands;

public class CreatePresenceCommand : IRequest<BaseResponse>
{
    public PresenceToAddDto PresenceInfo { get; set; } = null!;
}

public class CreatePresenceHandler : IRequestHandler<CreatePresenceCommand, BaseResponse>
{
    private readonly IGenericRepository<Presence> _presenceRepo;

    public CreatePresenceHandler(IGenericRepository<Presence> presenceRepo)
    {
        _presenceRepo = presenceRepo;
    }

    public async Task<BaseResponse> Handle(CreatePresenceCommand request, CancellationToken cancellationToken)
    {
        decimal workHour = WorkTimeConst.OfficialWorkHoursPerDay;
        decimal extraWorkHour = 0;

        TimeSpan startTime;
        TimeSpan endTime;

        var presenceToAdd = new Presence()
        {
            Date = DateTime.Now.Date,
            IdUser = request.PresenceInfo.IdUser,
            IsPresent = request.PresenceInfo.IsPresent,
            AbsenceReason = request.PresenceInfo.AbsenceReason,
            StartTime = new TimeSpan(WorkTimeConst.StartWorkHour, 0, 0),
            EndTime = new TimeSpan(WorkTimeConst.EndWorkHour, 0, 0)
        };

        if (!string.IsNullOrEmpty(request.PresenceInfo.StartTime) && !string.IsNullOrEmpty(request.PresenceInfo.EndTime))
        {
            if (!TimeSpan.TryParse(request.PresenceInfo.StartTime, out startTime) || !TimeSpan.TryParse(request.PresenceInfo.EndTime, out endTime))
            {
                return new BaseResponse()
                {
                    Message = "Niepoprawna data!"
                };
            }

            var worksHours = DateTimeHelper.GetWorkHours(startTime, endTime);
            workHour = worksHours.workHours;
            extraWorkHour = worksHours.extraWorkHours;

            presenceToAdd.StartTime = startTime;
            presenceToAdd.EndTime = endTime;
        }

        presenceToAdd.WorkHours = workHour;
        presenceToAdd.ExtraWorkHours = extraWorkHour;

        var response = await _presenceRepo.CreateEntity(presenceToAdd, cancellationToken);

        return new BaseResponse()
        {
            Succeeded = response.Succeeded,
            Message = response.Succeeded ? response.Message : "Błąd! Nie udało się dodać obecności!"
        };
    }
}
