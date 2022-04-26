using System.ComponentModel;

namespace IntranetWebApi.Domain.Enums;

public enum AbsenceReasonsEnum
{
    [Description("Urlop wypoczynkowy")]
    Holiday = 1,

    [Description("Urlop macieżyński")]
    MaternityLeave = 2,

    [Description("Urlop na żądanie")]
    LeaveAtRequest = 3,

    [Description("Urlop okolicznościowy")]
    SpecialLeave = 3,

    [Description("Zwolnienie lekarskie")]
    DoctorExcuse = 4,

    [Description("Delegacja")]
    Delegation = 5,

    [Description("Nieusprawiedliwiona nieobecność")]
    UnauthorizedAbsence = 6
}
