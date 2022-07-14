using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Dto;

public class UserPresentsPerDayDto
{
    public string UserName { get; set; } = null!;
    public int IdUser { get; set; }
    public bool IsPresent { get; set; }
    public int PresentType { get; set; }
    public string AbsenceReason { get; set; } = null;
    public string StartTime { get; set; } = null!;
    public string EndTime { get; set; } = null!;
}
