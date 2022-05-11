using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
}
