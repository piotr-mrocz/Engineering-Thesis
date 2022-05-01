using IntranetWebApi.Application.Features.RequestForLeaveFeatures.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntranetWebApi.Controllers;

public class RequestForLeaveController : ControllerBase
{
    private readonly IMediator _mediatr;

    public RequestForLeaveController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    #region Commands
    [HttpPost]
    public async Task<IActionResult> CreateRequestForLeave(CreateRequestForLeaveCommand request)
        => Ok(await _mediatr.Send(request));
    #endregion Commands

    #region Queries

    #endregion Queries
}
