using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Dto;

public class UserToAddDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public int IdDepartment { get; set; }
    public int IdRole { get; set; }
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int IdPosition { get; set; }
    public string PhotoName { get; set; } = null!;
}
