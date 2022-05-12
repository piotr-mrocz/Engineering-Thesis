using IntranetWebApi.Application.Helpers;
using IntranetWebApi.Domain.Enums;
using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Domain.Models.Entities.Views;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.RequestForLeaveFeatures.Queries;

public class GetAllRequestsForLeaveToAcceptByManagerQuery : IRequest<Response<List<GetAllRequestsForLeaveToAcceptDto>>>
{
}

public class GetAllRequestsForLeaveToAcceptByManagerHandler : IRequestHandler<GetAllRequestsForLeaveToAcceptByManagerQuery, Response<List<GetAllRequestsForLeaveToAcceptDto>>>
{
    private readonly IGenericRepository<VUsersRequestForLeave> _vUsersRequestForLeaveRepo;
    private readonly IGenericRepository<Presence> _presenceRepo;
    private readonly IGenericRepository<User> _userRepo;
    private readonly IMediator _mediator;

    public GetAllRequestsForLeaveToAcceptByManagerHandler(IGenericRepository<VUsersRequestForLeave> vUsersRequestForLeaveRepo,
        IGenericRepository<Presence> presenceRepo,
        IGenericRepository<User> userRepo,
        IMediator mediator)
    {
        _vUsersRequestForLeaveRepo = vUsersRequestForLeaveRepo;
        _presenceRepo = presenceRepo;
        _userRepo = userRepo;
        _mediator = mediator;
    }

    public async Task<Response<List<GetAllRequestsForLeaveToAcceptDto>>> Handle(GetAllRequestsForLeaveToAcceptByManagerQuery request, CancellationToken cancellationToken)
    {
        var idsSupervisor = await GetIdsSupervisors(cancellationToken);

        if (!idsSupervisor.Any())
        {
            return new Response<List<GetAllRequestsForLeaveToAcceptDto>>()
            {
                Message = "Nie udało się odnaleźć kierowników w bazie danych",
                Data = new List<GetAllRequestsForLeaveToAcceptDto>()
            };
        }

        var absentSupervisor = await GetAbsentSupervisors(idsSupervisor, cancellationToken);
        
        if (!absentSupervisor.Any())
        {
            return new Response<List<GetAllRequestsForLeaveToAcceptDto>>()
            {
                Message = "Wszyscy kierownicy są dzisiaj obecni",
                Data = new List<GetAllRequestsForLeaveToAcceptDto>()
            };
        }

        var requests = await GetAllVUsersRequestForLeaveByIdSupervisor(idsSupervisor, cancellationToken);

        if (!requests.Any())
        {
            return new Response<List<GetAllRequestsForLeaveToAcceptDto>>()
            {
                Message = "Nie ma żadnych wniosków o urlop do zaakceptowania",
                Data = new List<GetAllRequestsForLeaveToAcceptDto>()
            };
        }

        var resposne = GetGetAllRequestsForLeaveToAcceptListDto(requests);

        return new Response<List<GetAllRequestsForLeaveToAcceptDto>>()
        {
            Succeeded = true,
            Data = resposne
        };
    }

    private async Task<List<int>> GetIdsSupervisors(CancellationToken cancellationToken)
    {
        var users = await _userRepo.GetManyEntitiesByExpression(x => x.IdRole == (int)RolesEnum.Supervisor, cancellationToken);

        return users.Succeeded && users.Data.Any()
            ? users.Data.Select(x => x.Id).ToList()
            : new List<int>();
    }

    private async Task<List<int>> GetAbsentSupervisors(List<int> idsSupervisors, CancellationToken cancellationToken)
    {
        if (!idsSupervisors.Any())
            return new List<int>();

        // get all absent supervisors today
        var presences = await _presenceRepo.GetManyEntitiesByExpression(x => 
        idsSupervisors.Contains(x.IdUser) && 
        x.Date.Date == DateTime.Now.Date &&
        !x.IsPresent, 
        cancellationToken);

        return presences.Succeeded && presences.Data.Any()
            ? presences.Data.Select(x => x.IdUser).ToList()
            : new List<int>();
    }

    private async Task<List<VUsersRequestForLeave>> GetAllVUsersRequestForLeaveByIdSupervisor(List<int> idsSupervisors, CancellationToken cancellationToken)
    {
        if (!idsSupervisors.Any())
            return new List<VUsersRequestForLeave>();

        var usersRequestForLeaveList = await _vUsersRequestForLeaveRepo.GetManyEntitiesByExpression(x =>
            idsSupervisors.Contains(x.IdSupervisor), cancellationToken);

        return usersRequestForLeaveList.Succeeded && usersRequestForLeaveList.Data.Any()
            ? usersRequestForLeaveList.Data.ToList()
            : new List<VUsersRequestForLeave>();
    }

    private List<GetAllRequestsForLeaveToAcceptDto> GetGetAllRequestsForLeaveToAcceptListDto(List<VUsersRequestForLeave> usersRequestList)
    {
        if (!usersRequestList.Any())
            return new List<GetAllRequestsForLeaveToAcceptDto>();

        var requestListDto = new List<GetAllRequestsForLeaveToAcceptDto>();

        foreach (var request in usersRequestList)
        {
            var record = new GetAllRequestsForLeaveToAcceptDto()
            {
                IdRequest = request.IdRequest,
                DisplayUserName = request.DisplayUserName,
                EndDate = request.EndDate,
                StartDate = request.StartDate,
                AbsenceType = EnumHelper.GetEnumDescription((AbsenceReasonsEnum)request.AbsenceType)
            };

            requestListDto.Add(record);
        }

        return requestListDto;
    }
}
