using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Consts;

public static class PasswordSpecialCharConst
{
    public static List<char> PossibleChars = new List<char> { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')' };
}
