using AutoMapper;
using IntranetWebApi.Domain.Models.Dto;
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
    public PresenceToUpdateDto PresenceToUpdate { get; set; } = null!;
}

public class UpdatePresenceHandler : IRequestHandler<UpdatePresenceCommand, BaseResponse>
{
    private readonly IGenericRepository<Presence> _repo;

    public UpdatePresenceHandler(IGenericRepository<Presence> repo)
    {
        _repo = repo;
    }

    public async Task<BaseResponse> Handle(UpdatePresenceCommand request, CancellationToken cancellationToken)
    {
        var presenceToUpdate = await _repo.GetEntityByExpression(x => x.Id == request.PresenceToUpdate.Id, cancellationToken);

        if (presenceToUpdate is null || !presenceToUpdate.Succeeded || presenceToUpdate.Data is null)
        {
            return new BaseResponse()
            {
                Succeeded = false,
                Message = "Nie odnaleziono pozycji w bazie danych"
            };
        }

        presenceToUpdate.Data.IsPresent = request.PresenceToUpdate.IsPresent;
        presenceToUpdate.Data.AbsenceReason = request.PresenceToUpdate.AbsenceReason;

        var repsonse = await _repo.UpdateEntity(presenceToUpdate.Data, cancellationToken);

        return repsonse;
    }
}
