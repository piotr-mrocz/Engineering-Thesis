using IntranetWebApi.Application.Features.RoleFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntranetWebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class DepartmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public DepartmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> GetAllDepartments(GetAllDepartmentsQuery request)
        => Ok(await _mediator.Send(request));
}
