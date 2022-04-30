using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Dto;

public class GetPresenceByIdUserListDto
{
    public List<GetPresenceByIdUserDto> UserPresencesList { get; set; } = new();
}
