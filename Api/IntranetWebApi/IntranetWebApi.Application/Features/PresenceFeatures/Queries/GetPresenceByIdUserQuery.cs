using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Application.Helpers;
using IntranetWebApi.Domain.Enums;
using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.PresenceFeatures.Queries;

public class GetPresenceByIdUserQuery : IRequest<Response<GetPresenceByIdUserListDto>>
{
    public int IdUser { get; set; }
    public DateTime? Date { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class GetPresenceByIdUserHandler : IRequestHandler<GetPresenceByIdUserQuery, Response<GetPresenceByIdUserListDto>>
{
    //private readonly IGenericRepository<VUsersPresence> _vUsersPresenceRepo;

    //public GetPresenceByIdUserHandler(IGenericRepository<VUsersPresence> vUsersPresenceRepo)
    //{
    //    _vUsersPresenceRepo = vUsersPresenceRepo;
    //}

    //public async Task<Response<GetPresenceByIdUserListDto>> Handle(GetPresenceByIdUserQuery request, CancellationToken cancellationToken)
    //{
    //    Expression<Func<VUsersPresence, bool>> expression = x => x.IdUser == request.IdUser;

    //    if (request.Date.HasValue)
    //    {
    //        expression = x => x.IdUser == request.IdUser && 
    //                     x.Date == request.Date.Value.Date;
    //    }
    //    else if(request.StartDate.HasValue && request.EndDate.HasValue)
    //    {
    //        expression = x => x.IdUser == request.IdUser &&
    //                     x.Date >= request.StartDate.Value.Date &&
    //                     x.Date <= request.EndDate.Value.Date;
    //    }

    //    var usersPresence = await _vUsersPresenceRepo.GetManyEntitiesByExpression(expression, cancellationToken);

    //    if (usersPresence is null || !usersPresence.Succeeded || usersPresence.Data is null || !usersPresence.Data.Any())
    //    {
    //        return new Response<GetPresenceByIdUserListDto>()
    //        {
    //            Message = "Nie odnaleziono obecności użytkownika!",
    //            Data = new GetPresenceByIdUserListDto()
    //        };
    //    }

    //    var response = GetGetPresenceByIdUserListDto(usersPresence.Data);

    //    return new Response<GetPresenceByIdUserListDto>()
    //    {
    //        Succeeded = true,
    //        Data = response
    //    };
    //}

    //private GetPresenceByIdUserListDto GetGetPresenceByIdUserListDto(IEnumerable<VUsersPresence> vUsersPresences)
    //{
    //    if (!vUsersPresences.Any())
    //        return new GetPresenceByIdUserListDto();

    //    var usersPresenceDtoList = new List<GetPresenceByIdUserDto>();

    //    foreach (var userPresence in vUsersPresences.OrderBy(x => x.Date))
    //    {
    //        var record = new GetPresenceByIdUserDto()
    //        {
    //            Date = userPresence.Date,
    //            StartTime = userPresence.StartTime,
    //            EndTime = userPresence.EndTime,
    //            IsPresent = userPresence.IsPresent,
    //            AbsenceReason = userPresence.AbsenceReason.HasValue 
    //                          ? EnumHelper.GetEnumDescription((AbsenceReasonsEnum)userPresence.AbsenceReason.Value)
    //                          : string.Empty,
    //            WorkHours = userPresence.WorkHours,
    //            ExtraWorkHours = userPresence.ExtraWorkHours
    //        };

    //        usersPresenceDtoList.Add(record);
    //    }

    //    return new GetPresenceByIdUserListDto()
    //    {
    //        UserPresencesList = usersPresenceDtoList
    //    };
    //}
    public Task<Response<GetPresenceByIdUserListDto>> Handle(GetPresenceByIdUserQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
