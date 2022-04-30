using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Entities.Views;

public class VUsersPresence
{
    public int IdUser { get; set; }
    public string UserName { get; set; } = null!;

    [DataType(DataType.Date)]
    [Column(TypeName = "Date")]
    public DateTime Date { get; set; }

    [DataType(DataType.Time)]
    public TimeSpan StartTime { get; set; }

    [DataType(DataType.Time)]
    public TimeSpan EndTime { get; set; }

    public bool IsPresent { get; set; }
    public int? AbsenceReason { get; set; }
    public decimal WorkHours { get; set; }
    public decimal ExtraWorkHours { get; set; }
    public int IdDepartment { get; set; }
}
