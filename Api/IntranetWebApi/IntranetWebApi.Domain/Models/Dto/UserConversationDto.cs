using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Dto;

public class UserConversationDto
{
    public int IdSender { get; set; }
    public string Sender { get; set; } = null!;
    public int IdAddressee { get; set; }
    public string Adressee { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string SendDate { get; set; } = null!;
    public string SenderPhotoName { get; set; } = null!;
}
