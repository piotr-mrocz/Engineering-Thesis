using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Dto;

public class AddImportantInfoDto
{
    public string Info { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int IdUser { get; set; }
}
