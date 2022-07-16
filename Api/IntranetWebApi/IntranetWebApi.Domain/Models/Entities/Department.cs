using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Entities;

public class Department
{
    public int Id { get; set; }
    public string DepartmentName { get; set; } = null!;
    public int IdSupervisor { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
}
