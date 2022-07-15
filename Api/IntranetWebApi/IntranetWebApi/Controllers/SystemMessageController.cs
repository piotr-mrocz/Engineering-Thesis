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
    public async Task<IActionResult> GetAllUnReadSystemMessage(GetAllUnReadSystemMessageQuery request)
        => Ok(await _mediator.Send(request));
}
