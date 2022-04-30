using IntranetWebApi.Application.Features.PresenceFeatures.Commands;
using IntranetWebApi.Application.Features.PresenceFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntranetWebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class PresenceController : ControllerBase
{
    private readonly IMediator _mediator;
    public PresenceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #region Commands
    [HttpPost]
    public async Task<IActionResult> CrreateRangePresences(CreateRangePresencesCommand request)
        => Ok(await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> UpdatePresence(UpdatePresenceCommand request)
        => Ok(await _mediator.Send(request));
    #endregion Commands

    #region Queries
    [HttpPost]
    public async Task<IActionResult> GetUsersPresensePerDay(GetUsersPresencePerDayQuery request)
        => Ok(await _mediator.Send(request));
    #endregion Queries
}
