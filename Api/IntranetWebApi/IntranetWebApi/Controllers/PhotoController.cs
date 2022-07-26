using IntranetWebApi.Application.Features.PhotoFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntranetWebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class PhotoController : ControllerBase
{
    private readonly IMediator _mediator;

    public PhotoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #region Queries

    [HttpPost]
    public async Task<IActionResult> GetUserPhotoName([FromBody] GetUserPhotoNameQuery request)
        => Ok(await _mediator.Send(request));

    #endregion Queries
}
