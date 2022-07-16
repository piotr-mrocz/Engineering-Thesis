using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.SystemMessagesFeatures.Queries;

public class GetCountOnlyUnreadSystemMessagesQuery : IRequest<Response<CountUnReadSystemMessages>>
{
    public int IdUser { get; set; }
}

public class GetOnlyUnreadSystemMessagesHandler : IRequestHandler<GetCountOnlyUnreadSystemMessagesQuery, Response<CountUnReadSystemMessages>>
{
    private readonly IGenericRepository<SystemMessage> _systemMessageRepo;

    public GetOnlyUnreadSystemMessagesHandler(IGenericRepository<SystemMessage> systemMessageRepo)
    {
        _systemMessageRepo = systemMessageRepo;
    }

    public async Task<Response<CountUnReadSystemMessages>> Handle(GetCountOnlyUnreadSystemMessagesQuery request, CancellationToken cancellationToken)
    {
        var messages = await _systemMessageRepo.GetManyEntitiesByExpression(x => 
                x.IdUser == request.IdUser && !x.IsRead && !x.ReadDate.HasValue, 
                cancellationToken);

        if (messages == null || !messages.Succeeded || messages.Data == null || !messages.Data.Any())
        {
            return new()
            {
                Succeeded = true,
                Message = "Brak wiadomości systemowych",
                Data = new CountUnReadSystemMessages()
            };
        }

        return new()
        {
            Succeeded = true,
            Data = new CountUnReadSystemMessages() { CountUnreadMessages = messages.Data.Count() }
        };
    }
}
