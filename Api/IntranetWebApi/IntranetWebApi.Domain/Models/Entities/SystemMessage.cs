using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Entities;

public class SystemMessage
{
    public int Id { get; set; }
    public int IdUser { get; set; }
    public string Info { get; set; } = null!;
    public bool IsRead { get; set; }
    public DateTime? ReadDate { get; set; }

    public User User { get; set; } = null!;
}
