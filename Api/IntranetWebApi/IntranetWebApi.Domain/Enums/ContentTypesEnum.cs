using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Enums;
public enum ContentTypesEnum
{
    [Description("text/plain")]
    TextPlain = 1,

    [Description("application/json")]
    ApplicationJson = 2
}
