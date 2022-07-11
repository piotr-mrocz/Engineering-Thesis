using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Entities;

public class PresenceToAddDto
{
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int IdUser { get; set; }
    public bool IsPresent { get; set; }
    public int? AbsenceReason { get; set; }
}
