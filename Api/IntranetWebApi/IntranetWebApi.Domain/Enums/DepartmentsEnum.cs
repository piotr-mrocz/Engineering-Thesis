using System.ComponentModel;

namespace IntranetWebApi.Models.Enums;
public enum DepartmentsEnum
{
    [Description("Magazyn")]
    Warehouse = 1,

    [Description("IT")]
    It = 2,

    [Description("Zarząd")]
    Managers = 1002,

    [Description("Kadry")]
    HR = 1003,

    [Description("Księgowość")]
    Accounts = 1004,
}
