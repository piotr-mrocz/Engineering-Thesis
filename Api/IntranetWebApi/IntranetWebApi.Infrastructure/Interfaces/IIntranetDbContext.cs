using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Domain.Models.Entities.Views;
using IntranetWebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntranetWebApi.Infrastructure.Interfaces;

public interface IIntranetDbContext
{
    public DbSet<Test> TestowaTabela { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Presence> Presences { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<VUsersPresence> VUsersPresences { get; set; }
    public DbSet<VUsersRequestForLeave> VUsersRequestsForLeave { get; set; }
    public DbSet<RequestForLeave> RequestForLeaves { get; set; }
}
