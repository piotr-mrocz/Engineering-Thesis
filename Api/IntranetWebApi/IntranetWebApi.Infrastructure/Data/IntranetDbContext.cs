using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IntranetWebApi.Data;
public class IntranetDbContext : DbContext, IIntranetDbContext
{
    public IntranetDbContext(DbContextOptions options) : base(options) { }

    #region DbSets
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Presence> Presences { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<RequestForLeave> RequestForLeaves { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Domain.Models.Entities.Task> Tasks { get; set; }
    public DbSet<ImportantInfo> ImportantInfos { get; set; }
    public DbSet<SystemMessage> SystemMessages { get; set; }
    #endregion DbSets

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>(user =>
        {
            user.HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(x => x.IdRole);

            user.HasOne(u => u.Photo)
            .WithOne(p => p.User)
            .HasForeignKey<Photo>(x => x.IdUser);

            user.HasOne(u => u.Department)
            .WithMany(d => d.Users)
            .HasForeignKey(x => x.IdDepartment);

            user.HasOne(u => u.Position)
            .WithMany(d => d.Users)
            .HasForeignKey(x => x.IdPosition);
        });

        builder.Entity<RequestForLeave>()
            .HasOne(r => r.Applicant)
            .WithMany(u => u.RequestForLeaves)
            .HasForeignKey(x => x.IdApplicant);

        builder.Entity<Presence>()
            .HasOne(p => p.User)
            .WithMany(u => u.Presences)
            .HasForeignKey(x => x.IdUser);

        builder.Entity<Message>()
            .HasKey(x => x.Id);

        builder.Entity<Domain.Models.Entities.Task>()
            .HasOne(t => t.User)
            .WithMany(u => u.Tasks)
            .HasForeignKey(x => x.IdUser);

        builder.Entity<ImportantInfo>()
            .HasOne(i => i.WhoAdded)
            .WithMany(u => u.ImportantInfos)
            .HasForeignKey(x => x.IdWhoAdded);

        builder.Entity<SystemMessage>()
            .HasOne(m => m.User)
            .WithMany(u => u.SystemMessages)
            .HasForeignKey(x => x.IdUser);

        base.OnModelCreating(builder);
    }
}
