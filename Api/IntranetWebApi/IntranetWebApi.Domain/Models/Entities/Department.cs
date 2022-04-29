using IntranetWebApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Entities;

public class Department
{
    public int Id { get; set; }
    public string DepartmentName { get; set; } = null!;
    public int IdSupervisor { get; set; }

    public virtual User IdUser { get; set; } = null!;
}
