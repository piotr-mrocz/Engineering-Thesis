using System.ComponentModel;

namespace IntranetWebApi.Domain.Enums;

public enum RolesEnum
{
    [Description("User")]
    User = 1,

    [Description("Manager")]
    Manager = 2,

    [Description("Admin")]
    Admin = 3,

    [Description("Kierownik")]
    Supervisor = 1002
}
