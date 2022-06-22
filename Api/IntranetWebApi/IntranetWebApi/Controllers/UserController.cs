using IntranetWebApi.Application.Features.UserFeatures.Commands;
using IntranetWebApi.Application.Features.UserFeatures.Queries;
using IntranetWebApi.Domain.Consts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntranetWebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
//[Authorize]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #region Commands
    [HttpPost]
    //[Authorize(Roles = RolesConst.Admin)]
    public async Task<IActionResult> AddNewUser(AddNewUserCommand request)
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

    #endregion Queries
}


