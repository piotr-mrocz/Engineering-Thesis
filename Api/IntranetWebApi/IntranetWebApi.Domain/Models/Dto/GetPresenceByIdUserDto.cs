using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Dto;

public class GetPresenceByIdUserDto
{
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsPresent { get; set; }
    public string AbsenceReason { get; set; }
    public decimal WorkHours { get; set; }
    public decimal ExtraWorkHours { get; set; }
}
