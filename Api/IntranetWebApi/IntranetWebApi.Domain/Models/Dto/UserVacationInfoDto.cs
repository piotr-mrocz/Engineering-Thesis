using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Dto;

public class UserVacationInfoDto
{
    public int VacationDaysThisYear { get; set; }
    public int VacationDaysLastYear { get; set; }
    public int VacationDaysInRequests { get; set; }
    public int StartJobYear { get; set; }
}
