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
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.RequestForLeaveFeatures.Queries;

public class GetAllRequestsForLeaveByIdSupervisorQuery : IRequest<Response<GetAllRequestsForLeaveToAcceptListDto>>
{
    public int IdSupervisor { get; set; }
}

public class GetAllRequestsForLeaveByIdSupervisorHandler : IRequestHandler<GetAllRequestsForLeaveByIdSupervisorQuery, Response<GetAllRequestsForLeaveToAcceptListDto>>
{
    private readonly IGenericRepository<VUsersRequestForLeave> _vUsersRequestForLeaveRepo;

    public GetAllRequestsForLeaveByIdSupervisorHandler(IGenericRepository<VUsersRequestForLeave> vUsersRequestForLeaveRepo)
    {
        _vUsersRequestForLeaveRepo = vUsersRequestForLeaveRepo;
    }

    public async Task<Response<GetAllRequestsForLeaveToAcceptListDto>> Handle(GetAllRequestsForLeaveByIdSupervisorQuery request, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private async Task<List<VUsersRequestForLeave>> GetAllVUsersRequestForLeaveByIdSupervisor(int idSupervisor, CancellationToken cancellationToken)
    {
        var usersRequestForLeaveList = await _vUsersRequestForLeaveRepo.GetManyEntitiesByExpression(x =>
            x.IdSupervisor == idSupervisor, cancellationToken);

        return usersRequestForLeaveList.Succeeded && usersRequestForLeaveList.Data.Any()
            ? usersRequestForLeaveList.Data.ToList() 
            : new List<VUsersRequestForLeave>();
    }

    private GetAllRequestsForLeaveToAcceptListDto GetGetAllRequestsForLeaveToAcceptListDto(List<VUsersRequestForLeave> usersRequestList)
    {
        if (!usersRequestList.Any())
            return new GetAllRequestsForLeaveToAcceptListDto();

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

        return new GetAllRequestsForLeaveToAcceptListDto()
        {
            RequestsList = requestListDto
        };
    }
}
