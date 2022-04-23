using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public string Path { get; set; } = null!;
    }
}
