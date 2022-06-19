using System.ComponentModel.DataAnnotations.Schema;
using IntranetWebApi.Domain.Models.Entities;

namespace IntranetWebApi.Domain.Models.Entities;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public DateTime DateOfEmployment { get; set; }
    public DateTime? DateOfRelease { get; set; }
    public int IdDepartment { get; set; }
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int IdRole { get; set; }
    public bool IsActive { get; set; } = true;
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int IdPosition { get; set; }

    public Role Role { get; set; } = null!;
    public Photo Photo { get; set; } = null!;
    public Position Position { get; set; } = null!;
    public Department Department { get; set; } = null!;
    public ICollection<RequestForLeave> RequestForLeaves { get; set; } = new List<RequestForLeave>();
    public ICollection<Presence> Presences { get; set; } = new List<Presence>();


}
