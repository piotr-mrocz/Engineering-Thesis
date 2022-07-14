using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Dto;

public class UserDetailsDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = null!;
    public string UserLastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string PhotoName { get; set; } = null!;
    public string Department { get; set; } = null!;
    public int IdRole { get; set; }
    public string Position { get; set; } = null!;
    public bool IsNewUser { get; set; }
}
