using System.ComponentModel;

namespace IntranetWebApi.Domain.Enums;

public enum TaskStatusEnum
{
    [Description("Do zrobienia")]
    ToDo = 1,

    [Description("W trakcie wykonywania")]
    InProgress = 2,

    [Description("Gotowe")]
    Done = 3
}
