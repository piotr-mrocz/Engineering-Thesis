using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Application.Features.SystemMessagesFeatures.Commands;
using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.SystemMessagesFeatures.Queries;

public class GetAllSystemMessageQuery : IRequest<Response<List<SystemMessageDto>>>
{
    public int IdUser { get; set; }
}

public class GetAllUnReadSystemMessageHandler : IRequestHandler<GetAllSystemMessageQuery, Response<List<SystemMessageDto>>>
{
    private readonly IGenericRepository<SystemMessage> _systemMessageRepo;
    private readonly IMediator _mediator;

    public GetAllUnReadSystemMessageHandler(IGenericRepository<SystemMessage> systemMessageRepo, IMediator mediator)
    {
        _systemMessageRepo = systemMessageRepo;
        _mediator = mediator;
    }

    public async Task<Response<List<SystemMessageDto>>> Handle(GetAllSystemMessageQuery request, CancellationToken cancellationToken)
    {
        var messages = await _systemMessageRepo.GetManyEntitiesByExpression(x => x.IdUser == request.IdUser, cancellationToken);

        if (messages == null || !messages.Succeeded || messages.Data == null || !messages.Data.Any())
        {
            return new Response<List<SystemMessageDto>>()
            {
                Message = "Brak wiadomości systemowych",
                Data = new()
            };
        }

        var messagesListDto = GetSystemMessages(messages.Data.ToList());

        await _mediator.Send(new UpdateUnreadSystemMessagesCommand() { MessagesList = messagesListDto });

        return new Response<List<SystemMessageDto>>()
        {
            Succeeded = true,
            Data = messagesListDto
        };
    }

    private List<SystemMessageDto> GetSystemMessages(List<SystemMessage> messages)
    {
        var messagesListDto = new List<SystemMessageDto>();

        foreach (var message in messages)
        {
            messagesListDto.Add(new SystemMessageDto()
            {
                Id = message.Id,
                IsRead = message.IsRead,
                Info = message.Info,
                AddedDate = message.AddedDate.ToString("dd MMMM yyyy HH:mm")
            });
        }

        return messagesListDto;
    }
}
