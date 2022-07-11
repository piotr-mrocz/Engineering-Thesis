using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.ViewModels;

public class FreeDaysListVM
{
    public DateTime FreeDay { get; set; }
    public string FreeDayName { get; set; } = null!;
}
