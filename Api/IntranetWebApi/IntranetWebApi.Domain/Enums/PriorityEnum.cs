using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Enums;

public enum PriorityEnum
{
    [Description("Bardzo pilne")]
    VeryUrgent = 1,

    [Description("Pilne")]
    Urgent = 2,

    [Description("Ważne")]
    Important = 3,

    [Description("Może poczekać")]
    CanWait = 4
}
