using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Domain.Models.Entities;

namespace IntranetWebApi.Domain.Models.Dto;

public class GetUsersPositionsAndDepartmentsAndRolesDto
{
    public List<Department> Departments { get; set; } = new();
    public List<Position> Positions { get; set; } = new();
    public List<Role> Roles { get; set; } = new();
}

