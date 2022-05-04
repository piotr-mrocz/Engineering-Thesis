using IntranetWebApi.Domain.Models.Entities;

namespace IntranetWebApi.Models.Entities;
public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public DateTime DateOfEmployment { get; set; }
    public DateTime? DateOfRelease { get; set; }
    public int IdPosition { get; set; }
    public int IdDepartment { get; set; }
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int IdRole { get; set; }
    public int? IdPhoto { get; set; } = null;

    public virtual Role Role { get; set; } = null!;
    public virtual Photo Photo { get; set; } = null!;
    public virtual Department Department { get; set; } = null!;
}
