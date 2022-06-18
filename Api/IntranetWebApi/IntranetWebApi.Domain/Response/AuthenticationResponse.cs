using IntranetWebApi.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Response;
public class AuthenticationResponse
{
    public string Role { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public bool IsAuthorize { get; set; }
}
