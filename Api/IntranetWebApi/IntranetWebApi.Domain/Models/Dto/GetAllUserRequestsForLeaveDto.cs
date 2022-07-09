using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Dto;

public class GetAllUserRequestsForLeaveDto
{
    public int IdRequest { get; set; }
    public string CreatedDate { get; set; } = null!;
    public string StartDate { get; set; } = null!;
    public string EndDate { get; set; } = null!;
    public int TotalDays { get; set; }
    public string AbsenceType { get; set; } = null!;
    public int Status { get; set; }
    public string StatusDescription { get; set; } = null!;
}
