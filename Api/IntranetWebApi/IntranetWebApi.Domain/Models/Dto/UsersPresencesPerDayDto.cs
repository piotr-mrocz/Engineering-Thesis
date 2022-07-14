using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Dto;

public class UsersPresencesPerDayDto
{
    public List<UserPresentsPerDayDto> UsersNNPresencesList { get; set; } = new();
    public List<UserPresentsPerDayDto> UsersPresentPresencesList { get; set; } = new();
    public List<UserPresentsPerDayDto> UsersAbsentPresencesList { get; set; } = new();
}
