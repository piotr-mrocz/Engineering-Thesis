using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntranetWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            string token = _accountService.GenerateToken(loginDto);
            return Ok(token);
        }
    }
}
