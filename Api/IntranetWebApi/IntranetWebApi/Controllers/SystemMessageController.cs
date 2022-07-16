using IntranetWebApi.Application.Features.SystemMessagesFeatures.Commands;
using IntranetWebApi.Application.Features.SystemMessagesFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntranetWebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class SystemMessageController : ControllerBase
{
    private readonly IMediator _mediator;

    public SystemMessageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> GetAllSystemMessage(GetAllSystemMessageQuery request)
        => Ok(await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> GetCountOnlyUnreadSystemMessages(GetCountOnlyUnreadSystemMessagesQuery request)
         => Ok(await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> UpdateUnreadSystemMessages(UpdateUnreadSystemMessagesCommand request)
        => Ok(await _mediator.Send(request));
}
