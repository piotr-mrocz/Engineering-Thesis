using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Enums;

public enum RequestStatusEnum
{
    [Description("Do rozpatrzenia")]
    ForConsideration = 1,

    [Description("Zaakceptowany przez przełożonego")]
    AcceptedBySupervisor = 2,

    [Description("Odrzucony")]
    RejectedBySupervisor = 3,

    [Description("Wycofany przez użytkownika")]
    RemoveByUser = 4
}
