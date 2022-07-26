using IntranetWebApi.Application.Features.ImportantInfoFeatures.Commands;
using IntranetWebApi.Application.Features.ImportantInfoFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntranetWebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ImportantInfoController : ControllerBase
{
    private readonly IMediator _mediator;

    public ImportantInfoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AddImportantInfo(AddImportantInfoCommand request)
        => Ok(await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> GetImportantInfo(GetImportantInfoQuery request)
      => Ok(await _mediator.Send(request));
}
