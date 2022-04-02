namespace IntranetWebApi.Models;
public class Computer
{
    public int Id { get; set; }
    public string Brand { get; set; } = null!;
    public string Model { get; set; } = null!;
    public int OperationSystem { get; set; }
    public string Disc { get; set; } = null!;
    public string Ram { get; set; } = null!;
    public int Department { get; set; }
    public int Person { get; set; }
}