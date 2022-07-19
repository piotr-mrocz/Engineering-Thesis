using System.ComponentModel;

namespace IntranetWebApi.Domain.Enums;

public enum AbsenceReasonsEnum
{
    [Description("Urlop wypoczynkowy")]
    Holiday = 1,

    [Description("Urlop macierzyński")]
    MaternityLeave = 2,

    [Description("Urlop na żądanie")]
    LeaveAtRequest = 3,

    [Description("Urlop okolicznościowy")]
    SpecialLeave = 4,

    [Description("Zwolnienie lekarskie")]
    DoctorExcuse = 5,

    [Description("Delegacja")]
    Delegation = 6,

    [Description("Nieusprawiedliwiona nieobecność")]
    UnauthorizedAbsence = 7,

    [Description("Urlop bezpłatny")]
    UnpaidLeave = 8,

    [Description("Obecny")]
    Present = 9,

    [Description("Święto")]
    FreeDay = 10,

    [Description("Weekend")]
    Weekend = 11
}
