using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Interfaces;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.MessageFeatures.Queries;

public class GetUserConversationQuery : IRequest<Response<List<UserConversationDto>>>
{
    public int IdSender { get; set; }
    public int IdAddressee { get; set; }
}

public class GetUserConversationHandler : IRequestHandler<GetUserConversationQuery, Response<List<UserConversationDto>>>
{
    private IGenericRepository<Message> _messageRepo;
    private IGenericRepository<User> _userRepo;

    public GetUserConversationHandler(IGenericRepository<Message> messageRepo, IGenericRepository<User> userRepo)
    {
        _messageRepo = messageRepo; 
        _userRepo = userRepo;
    }

    public async Task<Response<List<UserConversationDto>>> Handle(GetUserConversationQuery request, CancellationToken cancellationToken)
    {
        var messages = await _messageRepo.GetManyEntitiesByExpression(x =>
                (x.IdSender == request.IdSender && x.IdAddressee == request.IdAddressee) ||
                (x.IdSender == request.IdAddressee && x.IdAddressee == request.IdSender),
                cancellationToken);

        if (messages is null || !messages.Succeeded || !messages.Data.Any())
        {
            return new Response<List<UserConversationDto>>()
            {
                Succeeded = true,
                Message = "Nie rozpoczęto jeszcze konwersacji",
                Data = new List<UserConversationDto>()
            };
        }

        var usersConversationsDto = await GetUserConversationDtoList(
            messages.Data,
            request,
            cancellationToken);

        return new Response<List<UserConversationDto>>()
        {
            Succeeded = true,
            Data = usersConversationsDto
        };
    }

    private async Task<List<UserConversationDto>> GetUserConversationDtoList(
        IEnumerable<Message> messages, 
        GetUserConversationQuery request, 
        CancellationToken cancellationToken)
    {
        var users = await _userRepo.GetManyEntitiesByExpression(x =>
                x.Id == request.IdSender ||
                x.Id == request.IdAddressee,
                cancellationToken);

        var usersConversationsDto = new List<UserConversationDto>();

        foreach (var message in messages)
        {
            var sender = users.Data.FirstOrDefault(x => x.Id == message.IdSender);
            var addressee = users.Data.FirstOrDefault(x => x.Id == message.IdAddressee);

            var messageDetails = new UserConversationDto()
            {
                IdSender = message.IdSender,
                IdAddressee = message.IdAddressee,
                Sender = $"{sender.FirstName} {sender.LastName}",
                Adressee = $"{addressee.FirstName} {addressee.LastName}",
                Content = message.Content,
                SendDate = message.SendDate
            };

            usersConversationsDto.Add(messageDetails);
        }

        return usersConversationsDto;
    }
}

public class UserConversationDto
{
    public int IdSender { get; set; }   
    public string Sender { get; set; } = null!;
    public int IdAddressee { get; set; }
    public string Adressee { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime SendDate { get; set; }
}

