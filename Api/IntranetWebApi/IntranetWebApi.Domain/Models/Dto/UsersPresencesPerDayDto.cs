using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Dto;

public class UsersPresencesPerDayDto
{
    public List<UserPresentsPerDayDto> UsersPresencesList { get; set; } = new();
}
