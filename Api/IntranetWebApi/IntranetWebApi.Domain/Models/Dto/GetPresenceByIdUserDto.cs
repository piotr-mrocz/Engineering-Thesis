using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Dto;

public class GetPresenceByIdUserDto
{
    public string Date { get; set; } = null!;
    public int DayNumber { get; set; }
    public bool IsFreeDay { get; set; } = true;
    public bool IsPresent { get; set; }
    public int PresentType { get; set; }
    public string AbsenceReason { get; set; } = null;
    public string StartTime { get; set; } = null!;
    public string EndTime { get; set; } = null!;
    public string WorkHours { get; set; } = null!;
    public string ExtraWorkHours { get; set; } = null!;
}
