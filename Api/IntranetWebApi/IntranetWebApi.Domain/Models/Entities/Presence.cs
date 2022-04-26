using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Entities
{
    public class Presence
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int IdUser { get; set; }
        public bool IsPresent { get; set; }
        public int? AbsenceReason { get; set; }
    }
}
