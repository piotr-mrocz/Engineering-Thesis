using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Dto;

public class PresenceToUpdateDto
{
    public int Id { get; set; }
    public int IdUser { get; set; }
    public bool IsPresent { get; set; }
    public int? AbsenceReason { get; set; }
}
