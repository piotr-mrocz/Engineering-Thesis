using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Dto;

public class UserPresentsPerDayDto
{
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string UserName { get; set; } = null!;
    public bool IsPresent { get; set; }
    public int? AbsenceReason { get; set; }
    public decimal WorkHours { get; set; }
    public decimal ExtraWorkHours { get; set; }
}
