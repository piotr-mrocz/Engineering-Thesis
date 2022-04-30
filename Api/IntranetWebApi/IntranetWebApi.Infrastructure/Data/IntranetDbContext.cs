using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Domain.Models.Entities.Views;
using IntranetWebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntranetWebApi.Data;
public class IntranetDbContext : DbContext
{
    public IntranetDbContext(DbContextOptions options) : base(options)
    {
    }

    #region DbSets
    public DbSet<Test> TestowaTabela { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Presence> Presences { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<VUsersPresence> VUsersPresences { get; set; }
    #endregion DbSets

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Test>()
                .Property(x => x.Name)
                .IsRequired();

        builder.Entity<Test>()
                .Property(x => x.Number)
                .IsRequired();

        builder.Entity<Role>()
                .Property(x => x.Name)
                .IsRequired();

        builder.Entity<User>()
               .Property(u => u.FirstName)
               .IsRequired();

        builder.Entity<User>()
               .Property(u => u.LastName)
               .IsRequired();

        builder.Entity<User>()
               .Property(u => u.Login)
               .IsRequired();

        builder.Entity<User>()
               .Property(u => u.Password)
               .IsRequired();

        builder.Entity<User>()
               .Property(u => u.IdRole)
               .IsRequired();

        builder.Entity<User>()
               .Property(u => u.DateOfEmployment)
               .IsRequired();

        builder.Entity<Photo>()
               .Property(p => p.Path)
               .IsRequired();

        builder.Entity<VUsersPresence>()
            .ToView(nameof(VUsersPresences))
            .HasKey(x => x.IdUser);

        base.OnModelCreating(builder);
    }
}
