using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Domain.Models.Entities.Views;
using IntranetWebApi.Infrastructure.Interfaces;
using IntranetWebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntranetWebApi.Data;
public class IntranetDbContext : DbContext, IIntranetDbContext
{
    public IntranetDbContext(DbContextOptions options) : base(options) { }

    #region DbSets
    public DbSet<Test> TestowaTabela { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Presence> Presences { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<VUsersPresence> VUsersPresences { get; set; }
    public DbSet<VUsersRequestForLeave> VUsersRequestsForLeave { get; set; }
    public DbSet<RequestForLeave> RequestForLeaves { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Domain.Models.Entities.Task> Tasks { get; set; }
    #endregion DbSets

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Test>()
                .Property(x => x.Name)
                .IsRequired();

        builder.Entity<Test>()
                .Property(x => x.Number)
                .IsRequired();


        builder.Entity<VUsersPresence>()
            .ToView(nameof(VUsersPresences))
            .HasKey(x => x.IdUser);

        builder.Entity<VUsersRequestForLeave>()
           .ToView(nameof(VUsersRequestsForLeave))
           .HasKey(x => x.IdRequest);

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

        base.OnModelCreating(builder);
    }
}
