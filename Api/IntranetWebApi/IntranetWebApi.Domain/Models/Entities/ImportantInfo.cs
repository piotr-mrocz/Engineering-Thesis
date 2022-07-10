using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Entities;

public class ImportantInfo
{
    public int Id { get; set; }
    public string Info { get; set; } = null!;
    public DateTime EndDate { get; set; }
    public int IdWhoAdded { get; set; }

    public User WhoAdded { get; set; } = null!;
}
