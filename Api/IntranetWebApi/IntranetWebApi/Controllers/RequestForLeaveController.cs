using IntranetWebApi.Application.Features.RequestForLeaveFeatures.Commands;
using IntranetWebApi.Application.Features.RequestForLeaveFeatures.Queries;
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

    [HttpPost]
    public async Task<IActionResult> AcceptRequestForLeave(AcceptRequestForLeaveCommand request)
        => Ok(await _mediatr.Send(request));

    [HttpPost]
    public async Task<IActionResult> RejectRequestForLeave(RejectRequestForLeaveCommand request)
        => Ok(await _mediatr.Send(request));

    [HttpPost]
    public async Task<IActionResult> RemoveRequestForLeave(RemoveRequestForLeaveCommand request)
        => Ok(await _mediatr.Send(request));

    #endregion Commands

    #region Queries

    [HttpPost]
    public async Task<IActionResult> GetAllRequestsForLeaveToAcceptByIdSupervisor(GetAllRequestsForLeaveByIdSupervisorQuery request)
        => Ok (await _mediatr.Send(request));

    [HttpPost]
    public async Task<IActionResult> GetAllRequestsForLeaveToAcceptByManager(GetAllRequestsForLeaveToAcceptByManagerQuery request)
        => Ok(await _mediatr.Send(request));
    #endregion Queries
}
