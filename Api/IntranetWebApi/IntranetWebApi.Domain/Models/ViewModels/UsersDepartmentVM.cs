using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Domain.Models.Entities;

namespace IntranetWebApi.Domain.Models.ViewModels;

public class UsersDepartmentVM
{
    public int IdSupervisor { get; set; }
    public User User { get; set; } = new();
}
