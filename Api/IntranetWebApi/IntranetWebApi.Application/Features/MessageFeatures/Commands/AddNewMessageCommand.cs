using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace IntranetWebApi.Application.Features.MessageFeatures.Commands;

public class AddNewMessageCommand : IRequest<BaseResponse>
{
    public int IdSender { get; set; }
    public int IdAddressee { get; set; }
    public string Content { get; set; } = null!;
}

public class AddNewMessageHandler : IRequestHandler<AddNewMessageCommand, BaseResponse>
{
    private readonly IGenericRepository<Message> _messageRepo;
    private readonly IHubContext<MessageHubClient, IMessageHubClient> _messageHub;

    public AddNewMessageHandler(IGenericRepository<Message> messageRepo, IHubContext<MessageHubClient, IMessageHubClient> messageHub)
    {
        _messageRepo = messageRepo;
        _messageHub = messageHub;
    }

    public async Task<BaseResponse> Handle(AddNewMessageCommand request, CancellationToken cancellationToken)
    {
        var messageToAdd = new Message()
        {
            IdSender = request.IdSender,
            IdAddressee = request.IdAddressee,
            Content = request.Content,
            SendDate = DateTime.Now
        };

        var addMessageResponse = await _messageRepo.CreateEntity(messageToAdd, cancellationToken);

        if (!addMessageResponse.Succeeded)
        {
            return new BaseResponse()
            {
                Message = addMessageResponse.Message
            };
        }

        await _messageHub.Clients.All.NewMessageWasSend(); // for test
       
        return new BaseResponse()
        {
            Succeeded = addMessageResponse.Succeeded,
            Message = addMessageResponse.Succeeded
                    ? addMessageResponse.Message
                    : "Nie udało się wysłać wiadomości. Prosimy o kontakt z działem IT"
        };
    }
}
