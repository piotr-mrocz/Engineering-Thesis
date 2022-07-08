using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Domain.Enums;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.RequestForLeaveFeatures.Commands;

public class RemoveRequestForLeaveCommand : IRequest<BaseResponse>
{
    public int IdRequest { get; set; }
}

public class RemoveRequestForLeaveHandler : IRequestHandler<RemoveRequestForLeaveCommand, BaseResponse>
{
    private readonly IGenericRepository<RequestForLeave> _requestRepo;

    public RemoveRequestForLeaveHandler(IGenericRepository<RequestForLeave> requestRepo)
    {
        _requestRepo = requestRepo;
    }

    public async Task<BaseResponse> Handle(RemoveRequestForLeaveCommand request, CancellationToken cancellationToken)
    {
        var requestForLeave = await _requestRepo.GetEntityByExpression(x => x.Id == request.IdRequest, cancellationToken);

        if (requestForLeave == null || !requestForLeave.Succeeded || requestForLeave.Data == null)
        {
            return new BaseResponse()
            {
                Message = "Nie udało się odnaleźć wniosku w bazie danych!"
            };
        }

        requestForLeave.Data.Status = (int)RequestStatusEnum.RemovedByUser;
        requestForLeave.Data.ActionDate = DateTime.Now;

        var response = await _requestRepo.UpdateEntity(requestForLeave.Data, cancellationToken);

        return new BaseResponse()
        {
            Succeeded = response.Succeeded,
            Message = response.Succeeded ? response.Message : "Wystąpił błąd podczas akceptowania wniosku!"
        };
    }
}
