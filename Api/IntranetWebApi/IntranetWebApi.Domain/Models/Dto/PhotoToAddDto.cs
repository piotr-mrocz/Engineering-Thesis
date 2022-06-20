using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Dto;

public class PhotoToAddDto
{
    public string Name { get; set; } = null!;
    public int IdUser { get; set; }
}
