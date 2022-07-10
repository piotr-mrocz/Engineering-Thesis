using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Dto;

public class GetImportantInfoDto
{
    public string Info { get; set; } = null!;
    public string UserName { get; set; } = null!;
}
