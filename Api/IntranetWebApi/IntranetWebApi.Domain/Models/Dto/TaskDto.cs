using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Dto;

public class TaskDto
{
    public int Id { get; set; }
    public int IdUser { get; set; }
    public int WhoAdded { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Deadline { get; set; } = null!;
    public string AddedDate { get; set; } = null!;
    public string ProgressDate { get; set; } = null!;
    public string FinishDate { get; set; } = null!;
    public int Status { get; set; } // I create special enum for this
    public string PriorityDescription { get; set; } = null!;
    public int Priority { get; set; }
}
