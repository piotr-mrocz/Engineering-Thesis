using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Entities;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.PresenceFeatures.Queries;

public class GetUsersPresencePerDayQuery : IRequest<Response<UsersPresencesPerDayDto>>
{
    public DateTime Date { get; set; }
}

public class GetUsersPresencePerDayHandler : IRequestHandler<GetUsersPresencePerDayQuery, Response<UsersPresencesPerDayDto>>
{
    private readonly IGenericRepository<Presence> _presenceRepo;
    private readonly IGenericRepository<User> _userRepo;

    public GetUsersPresencePerDayHandler(IGenericRepository<Presence> presenceRepo, IGenericRepository<User> userRepo)
    {
        _presenceRepo = presenceRepo;
        _userRepo = userRepo;
    }

    public Task<Response<UsersPresencesPerDayDto>> Handle(GetUsersPresencePerDayQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    //private async Task<Response<List<Presence>>> GetListPresenceByDate(DateTime date)
    //{

    //}

    //private async Task<Response<Dictionary<int, string>>> GetAllUsersInUsersPresencesList(List<int> idsUsersList,
    //    CancellationToken cancellationToken)
    //{

    //}
}
