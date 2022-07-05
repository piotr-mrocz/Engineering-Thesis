using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Dto;

public class UserDto
{
    public int IdUser { get; set; }
    public string UserName { get; set; } = null!;
}
