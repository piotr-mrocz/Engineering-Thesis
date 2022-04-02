using IntranetWebApi.Features.TestowaTabelaFeatures.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntranetWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestowaTabelaController : Controller
    {
        private readonly IMediator _mediator;
        public TestowaTabelaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllTestowaTabela(GetManyTestowaTabelaQuery request)
        //    => Ok(await _mediator.Send(request));

        [HttpPost]
        public async Task<IActionResult> CreateTestowaTabela([FromQuery] CreateTestowaTabelaCommand request)
           => Ok(await _mediator.Send(request));
    }
}
