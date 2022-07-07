using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.UserFeatures.Commands;

public class AddRangeNewMessagesCommand : IRequest<BaseResponse>
{
    public int IdSender { get; set; }
    public List<int> IdAddressee { get; set; } = new();
    public string Content { get; set; } = null!;
}

public class AddRangeNewMessagesHandler : IRequestHandler<AddRangeNewMessagesCommand, BaseResponse>
{
    private readonly IGenericRepository<Message> _messageRepo;

    public AddRangeNewMessagesHandler(IGenericRepository<Message> messageRepo)
    {
        _messageRepo = messageRepo;
    }

    public async Task<BaseResponse> Handle(AddRangeNewMessagesCommand request, CancellationToken cancellationToken)
    {
        var messagesToAdd = new List<Message>();

        foreach (var idAdressee in request.IdAddressee)
        {
            var messageToAdd = new Message()
            {
                IdSender = request.IdSender,
                IdAddressee = idAdressee,
                Content = request.Content,
                SendDate = DateTime.Now
            };

            messagesToAdd.Add(messageToAdd);
        }

        var addMessageResponse = await _messageRepo.CreateRangeEntities(messagesToAdd, cancellationToken);

        return new BaseResponse()
        {
            Succeeded = addMessageResponse.Succeeded,
            Message = addMessageResponse.Succeeded
                    ? addMessageResponse.Message
                    : "Nie udało się wysłać wiadomości. Prosimy o kontakt z działem IT"
        };
    }
}
