using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Dto;

public class GetPresenceByIdUserListDto
{
    public decimal TotalWorkHour { get; set; }
    public decimal TotalWorkExtraHour { get; set; }
    public List<GetPresenceByIdUserDto> UserPresencesList { get; set; } = new();
}
