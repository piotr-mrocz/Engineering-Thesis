﻿using IntranetWebApi.Application.Features.UserFeatures.Commands;
using IntranetWebApi.Application.Features.UserFeatures.Queries;
using IntranetWebApi.Domain.Consts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntranetWebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #region Commands

    [HttpPost]
    public async Task<IActionResult> AddNewUser([FromBody] AddNewUserCommand request)
        => Ok(await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> ReleaseUser(ReleaseUserCommand request)
        => Ok(await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> UpdateUserData(UpdateUserDataCommand request)
        => Ok(await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> ChangeUserPassword(ChangeUserPasswordCommand request)
         => Ok(await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> ResetUserPassword(ResetUserPasswordCommand request)
         => Ok(await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> AddVacationDaysToNewUser(AddVacationDaysToNewUserCommand request)
        => Ok(await _mediator.Send(request));

    #endregion Commands

    #region Queries

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> GetAllUsers(GetAllUsersQuery request)
        => Ok(await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> GetAllUsersByIdDepartment(GetAllUsersByIdDepartmentQuery request)
        => Ok(await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> GetUsersPositionsAndDepartmentsAndRoles(GetUsersPositionsAndDepartmentsAndRolesQuery request)
        => Ok(await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> GetAllUserInDepartmentByIdSupervisor(GetAllUserInDepartmentByIdSupervisorQuery request)
        => Ok(await _mediator.Send(request));

    #endregion Queries
}


