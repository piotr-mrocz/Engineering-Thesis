using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Entities;

public class RequestForLeave
{
    public int Id { get; set; }
    public int IdUser { get; set; }
    public DateTime CreateDate { get; set; }
    public int IdSupervisor { get; set; }
    public int AbsenceType { get; set; }
    public bool IsAcceptedBySupervisor { get; set; }
    public DateTime AcceptedDate { get; set; }
}
