using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Enums;

public enum SystemMessageTypeEnum
{
    [Description("Dodano wniosek o urlop")]
    AddNewRequestForLeave = 1,

    [Description("Wycofano wniosek o urlop")]
    RemoveRequestForLeave = 2,

    [Description("Wniosek o urlop został zaakceptowany")]
    AcceptRequestForLeave = 3,

    [Description("Wniosek o urlop został odrzucony")]
    RejectRequestForLeave = 4,

    [Description("Dodano nowe zadanie")]
    AddNewUserTask = 5,

    [Description("Usunięto zadanie")]
    RemoveUserTask = 6,

    [Description("Użytkownik rozpoczął zadanie")]
    StartUserTask = 7,

    [Description("Użytkownik zakończył zadanie")]
    EndUserTask = 8
}
