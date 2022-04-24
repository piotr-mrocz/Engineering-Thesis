using IntranetWebApi.Domain.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntranetWebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize]
public class UserController : ControllerBase
{

    [HttpPost]
    [Authorize(Roles = RolesConst.Admin)]
    public void AddNewUser()
    {

    }

    [HttpGet]
    [Authorize]
    public void GetAllUsers()
    {

    }
}


