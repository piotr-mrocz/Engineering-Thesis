namespace IntranetWebApi.Models.Entities;
public class Users
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public DateTime DateOfEmployment { get; set; }
    public DateTime? DateOfRelease { get; set; }
    public int Position { get; set; }
    public int Department { get; set; }
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;

}
