using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Entities;

public class Message
{
    public int Id { get; set; }
    public int IdSender{ get; set; }
    public int IdAddressee { get; set; }
    public string Content { get; set; } = null!;
    public DateTime SendDate { get; set; }
}
