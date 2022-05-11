using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Entities.Views;

public class VUsersRequestForLeave
{
    public int IdRequest { get; set; }
    public string DisplayUserName { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int AbsenceType { get; set; }
    public int IdSupervisor { get; set; }
    public int IdApplicant { get; set; }
}
