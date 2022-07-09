using IntranetWebApi.Application.Features.RequestForLeaveFeatures.Commands;
using IntranetWebApi.Application.Features.RequestForLeaveFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntranetWebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class RequestForLeaveController : ControllerBase
{
    private readonly IMediator _mediator;

    public RequestForLeaveController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #region Commands

    [HttpPost]
    public async Task<IActionResult> CreateRequestForLeave(CreateRequestForLeaveCommand request)
        => Ok(await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> AcceptRequestForLeave(AcceptRequestForLeaveCommand request)
        => Ok(await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> RejectRequestForLeave(RejectRequestForLeaveCommand request)
        => Ok(await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> RemoveRequestForLeave(RemoveRequestForLeaveCommand request)
        => Ok(await _mediator.Send(request));

    #endregion Commands

    #region Queries

    [HttpPost]
    public async Task<IActionResult> GetUserRequestsForLeave(GetAllUserRequestsForLeaveQuery request)
        => Ok(await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> GetAllRequestsForLeaveToAcceptByIdSupervisor(GetAllRequestsForLeaveByIdSupervisorQuery request)
        => Ok (await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> GetAllRequestsForLeaveToAcceptByManager(GetAllRequestsForLeaveToAcceptByManagerQuery request)
        => Ok(await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> GetInformationAboutUserVacationDays(GetInformationAboutUserVacationDaysQuery request)
         => Ok(await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> GetAllPossibleAbsenceTypeToChoose(GetAllPossibleAbsenceTypeToChooseCommand request)
        => Ok(await _mediator.Send(request));

    #endregion Queries
}
