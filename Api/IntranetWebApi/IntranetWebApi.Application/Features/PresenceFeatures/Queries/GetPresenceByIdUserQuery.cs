using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Domain.Models.Entities.Views;
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
    private readonly IGenericRepository<VUsersPresence> _vUsersPresenceRepo;

    public GetPresenceByIdUserHandler(IGenericRepository<VUsersPresence> vUsersPresenceRepo)
    {
        _vUsersPresenceRepo = vUsersPresenceRepo;
    }

    public Task<Response<GetPresenceByIdUserListDto>> Handle(GetPresenceByIdUserQuery request, CancellationToken cancellationToken)
    {

        throw new NotImplementedException();
    }
}
