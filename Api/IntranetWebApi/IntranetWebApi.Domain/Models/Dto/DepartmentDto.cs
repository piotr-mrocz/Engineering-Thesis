using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Dto;

public class DepartmentDto
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = null!;
}
