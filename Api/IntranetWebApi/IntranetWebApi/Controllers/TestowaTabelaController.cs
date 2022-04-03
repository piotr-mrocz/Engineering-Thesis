using IntranetWebApi.Application.Features.TestowaTabelaFeatures.Command;
using IntranetWebApi.Application.Features.TestowaTabelaFeatures.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntranetWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TestowaTabelaController : Controller
    {
        private readonly IMediator _mediator;
        public TestowaTabelaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Commands
        [HttpPost]
        public async Task<IActionResult> CreateTestowaTabela([FromQuery] CreateTestowaTabelaCommand request)
           => Ok(await _mediator.Send(request));

        [HttpPost]
        public async Task<IActionResult> UpdateTestowaTabela(UpdateTesowaTabelaCommand request)
            => Ok(await _mediator.Send(request));

        [HttpPost]
        public async Task<IActionResult> DeleteTestowaTabela(DeleteTestowaTabelaCommand request)
            => Ok(await _mediator.Send(request));
        #endregion

        #region Queries
        [HttpPost]
        public async Task<IActionResult> GetAllTestowaTabela(GetManyTestowaTabelaQuery request)
           => Ok(await _mediator.Send(request));

        [HttpPost]
        public async Task<IActionResult> GetTestowaTabelaById(GetEntityTestowaTabelaQuery request)
           => Ok(await _mediator.Send(request));
        #endregion
    }
}
 