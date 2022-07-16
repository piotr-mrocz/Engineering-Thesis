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

namespace IntranetWebApi.Application.Features.SystemMessagesFeatures.Commands;

public class UpdateUnreadSystemMessagesCommand : IRequest<BaseResponse>
{
    public List<SystemMessageDto> MessagesList { get; set; } = null!;
}

public class UpdateUnreadSystemMessagesHandler : IRequestHandler<UpdateUnreadSystemMessagesCommand, BaseResponse>
{
    private readonly IGenericRepository<SystemMessage> _systemMessageRepo;

    public UpdateUnreadSystemMessagesHandler(IGenericRepository<SystemMessage> systemMessageRepo)
    {
        _systemMessageRepo = systemMessageRepo;
    }

    public async Task<BaseResponse> Handle(UpdateUnreadSystemMessagesCommand request, CancellationToken cancellationToken)
    {
        if (!request.MessagesList.Any())
        {
            return new BaseResponse()
            {
                Message = "Wystąpił błąd podczas odczytywania wiadomości!"
            };
        }

        var unReadMessages = request.MessagesList.Where(x => !x.IsRead);

        if (!request.MessagesList.Any())
        {
            return new BaseResponse()
            {
                Succeeded = true,
                Message = "Nie ma żadnych nieprzeczytanych wiadomości"
            };
        }

        var response = await ReadMessages(unReadMessages, cancellationToken);
        return response;
    }

    private async Task<BaseResponse> ReadMessages(IEnumerable<SystemMessageDto> unReadMessagesList, CancellationToken cancellationToken)
    {
        var idsUnreadMessages = unReadMessagesList.Select(x => x.Id).ToList();
        var messagesSystem = await _systemMessageRepo.GetManyEntitiesByExpression(x => idsUnreadMessages.Contains(x.Id), cancellationToken);

        if (messagesSystem == null || !messagesSystem.Succeeded || messagesSystem.Data == null || !messagesSystem.Data.Any())
        {
            return new()
            {
                Message = "Nie odnaleziono wiadomości w bazie danych!"
            };
        }

        foreach (var message in messagesSystem.Data)
        {
            message.IsRead = true;
            message.ReadDate = DateTime.Now;
        }

        var response = await _systemMessageRepo.UpdateRangeEntities(messagesSystem.Data.ToList(), cancellationToken);

        return new BaseResponse()
        {
            Succeeded = response.Succeeded,
            Message = response.Succeeded ? response.Message : "Nie udało się odczytać wiadomosći"
        };
    }
}
