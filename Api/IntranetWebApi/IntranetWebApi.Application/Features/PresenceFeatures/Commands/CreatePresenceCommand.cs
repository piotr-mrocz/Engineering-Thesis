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
        var worksHours = DateTimeHelper.GetWorkHours(request.PresenceInfo.StartTime, request.PresenceInfo.EndTime);

        var presenceToAdd = new Presence()
        {
            Date = DateTime.Now.Date,
            StartTime = request.PresenceInfo.StartTime,
            EndTime = request.PresenceInfo.EndTime,
            IdUser = request.PresenceInfo.IdUser,
            IsPresent = request.PresenceInfo.IsPresent,
            AbsenceReason = request.PresenceInfo.AbsenceReason,
            WorkHours = worksHours.workHours,
            ExtraWorkHours = worksHours.extraWorkHours
        };

        var response = await _presenceRepo.CreateEntity(presenceToAdd, cancellationToken);

        return new BaseResponse()
        {
            Succeeded = response.Succeeded,
            Message = response.Succeeded ? response.Message : "Błąd! Nie udało się dodać obecności!"
        };
    }
}
