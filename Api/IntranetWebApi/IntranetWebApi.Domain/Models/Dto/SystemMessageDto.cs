using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Dto;

public class SystemMessageDto
{
    public int Id { get; set; }
    public string Info { get; set; } = null!;
    public bool IsRead { get; set; }
    public string AddedDate { get; set; } = null!;
}
