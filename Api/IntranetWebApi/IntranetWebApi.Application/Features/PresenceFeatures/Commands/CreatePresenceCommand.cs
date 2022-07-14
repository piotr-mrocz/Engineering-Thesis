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

        if (request.PresenceInfo.StartTime.HasValue && request.PresenceInfo.EndTime.HasValue)
        {
            var worksHours = DateTimeHelper.GetWorkHours(request.PresenceInfo.StartTime.Value, request.PresenceInfo.EndTime.Value);
            workHour = worksHours.workHours;
            extraWorkHour = worksHours.extraWorkHours;
        }

        var presenceToAdd = new Presence()
        {
            Date = DateTime.Now.Date,
            StartTime = request.PresenceInfo.StartTime.HasValue ? request.PresenceInfo.StartTime.Value : new TimeSpan(WorkTimeConst.StartWorkHour, 0, 0),
            EndTime = request.PresenceInfo.EndTime.HasValue ? request.PresenceInfo.EndTime.Value : new TimeSpan(WorkTimeConst.EndWorkHour, 0, 0),
            IdUser = request.PresenceInfo.IdUser,
            IsPresent = request.PresenceInfo.IsPresent,
            AbsenceReason = request.PresenceInfo.AbsenceReason,
            WorkHours = workHour,
            ExtraWorkHours = extraWorkHour
        };

        var response = await _presenceRepo.CreateEntity(presenceToAdd, cancellationToken);

        return new BaseResponse()
        {
            Succeeded = response.Succeeded,
            Message = response.Succeeded ? response.Message : "Błąd! Nie udało się dodać obecności!"
        };
    }
}
