using IntranetWebApi.Application.Features.MessageFeatures.Commands;
using IntranetWebApi.Application.Features.MessageFeatures.Queries;
using IntranetWebApi.Application.Features.UserFeatures.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntranetWebApi.Controllers;

public class MessageController : ControllerBase
{
    private readonly IMediator _mediator;

    public MessageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetConversation(GetUserConversationQuery request)
        => Ok(await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> AddNewMessage(AddNewMessageCommand request)
        => Ok(await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> AddNewRangeMessages(AddRangeNewMessagesCommand request)
        => Ok(await _mediator.Send(request));
}
