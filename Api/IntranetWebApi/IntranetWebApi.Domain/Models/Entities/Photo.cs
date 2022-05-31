using System.ComponentModel.DataAnnotations.Schema;
using IntranetWebApi.Models.Entities;

namespace IntranetWebApi.Domain.Models.Entities;

public class Photo
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int IdUser { get; set; }

    public User User { get; set; } = null!;
}
