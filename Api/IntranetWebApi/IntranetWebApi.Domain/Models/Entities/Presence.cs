using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Models.Entities;

namespace IntranetWebApi.Domain.Models.Entities;

public class Presence
{
    public int Id { get; set; }

    [DataType(DataType.Date)]
    [Column(TypeName = "Date")]
    public DateTime Date { get; set; }
    [DataType(DataType.Time)]
    public TimeSpan StartTime { get; set; }
    [DataType(DataType.Time)]
    public TimeSpan? EndTime { get; set; }
    public int IdUser { get; set; }
    public bool IsPresent { get; set; }
    public int? AbsenceReason { get; set; }
    public decimal WorkHours { get; set; } // I created trigger to calculate this property
    public decimal ExtraWorkHours { get; set; } // I created trigger to calculate this property

    public User User { get; set; } = null!;
}
