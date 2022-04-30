using AutoMapper;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Application.Features.PresenceFeatures.Commands;

public class UpdatePresenceCommand : IRequest<BaseResponse>
{
    public int Id { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int IdUser { get; set; }
    public bool IsPresent { get; set; }
    public int? AbsenceReason { get; set; }
    public decimal WorkHours { get; set; }
    public decimal ExtraWorkHours { get; set; }
}

public class UpdatePresenceHandler : IRequestHandler<UpdatePresenceCommand, BaseResponse>
{
    private readonly IGenericRepository<Presence> _repo;
    private readonly IMapper _mapper;

    public UpdatePresenceHandler(IGenericRepository<Presence> repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<BaseResponse> Handle(UpdatePresenceCommand request, CancellationToken cancellationToken)
    {
        var presenceToUpdate = await _repo.GetEntityByExpression(x => x.Id == request.Id, cancellationToken);

        if (presenceToUpdate is null)
        {
            return new BaseResponse()
            {
                Succeeded = false,
                Message = "Nie odnaleziono pozycji w bazie danych"
            };
        }

        var presence = _mapper.Map<UpdatePresenceCommand, Presence>(request);

        var repsonse = await _repo.UpdateEntity(presence, cancellationToken);

        return repsonse;
    }
}

