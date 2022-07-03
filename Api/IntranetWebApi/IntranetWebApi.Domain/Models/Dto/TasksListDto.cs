using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Dto;

public class TasksListDto
{
    public List<IntranetWebApi.Domain.Models.Entities.Task> ToDoTasks { get; set; } = new();
    public List<IntranetWebApi.Domain.Models.Entities.Task> InProgressTasks { get; set; } = new();
    public List<IntranetWebApi.Domain.Models.Entities.Task> DoneTasks { get; set; } = new();
}
