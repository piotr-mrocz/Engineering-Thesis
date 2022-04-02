namespace IntranetWebApi.Models;
public class Printer
{
    public int Id { get; set; }
    public string Brand { get; set; } = null!;
    public string Model { get; set; } = null!;
    public int Department { get; set; }
    public DateTime DateOfProduction { get; set; }
}
