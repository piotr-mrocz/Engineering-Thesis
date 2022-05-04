using IntranetWebApi.Domain.Models.Entities;

namespace IntranetWebApi.Models;
public class Computer
{
    public int Id { get; set; }
    public string Brand { get; set; } = null!;
    public string Model { get; set; } = null!;
    public int OperationSystem { get; set; }
    public string Disc { get; set; } = null!;
    public string Ram { get; set; } = null!;
    public int IdDepartment { get; set; }
    public int Person { get; set; }

    public virtual Department Department { get; set; } = null!;
}
