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
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string AbsenceType { get; set; } = null!;
}

public class GetAllRequestsForLeaveToAcceptListDto
{
    public List<GetAllRequestsForLeaveToAcceptDto> RequestsList { get; set; } = new();
}
