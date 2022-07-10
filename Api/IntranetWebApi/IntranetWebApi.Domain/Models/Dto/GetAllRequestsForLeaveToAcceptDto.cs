using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Dto;

public class GetAllRequestsForLeaveToAcceptDto
{
    public int IdRequest { get; set; }
    public string DisplayUserName { get; set; } = null!;
    public int IdApplicant { get; set; }
    public string StartDate { get; set; } = null!;
    public string EndDate { get; set; } = null!;
    public string AddedDate { get; set; } = null!;
    public int TotalDays { get; set; }
    public string AbsenceType { get; set; } = null!;
}
