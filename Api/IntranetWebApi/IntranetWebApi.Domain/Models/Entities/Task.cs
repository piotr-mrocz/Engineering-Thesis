namespace IntranetWebApi.Domain.Models.Entities;

public class Task
{
    public int Id { get; set; }
    public int IdUser { get; set; }
    public int WhoAdd { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime? Deadline { get; set; }
    public DateTime AddedDate { get; set; }
    public DateTime? ProgressDate { get; set; }
    public DateTime? FinishDate { get; set; }
    public int Status { get; set; } // I create special enum for this
    public int Priority { get; set; } // I create special enum for this

    public User User { get; set; } = null!;
}
