using IntranetWebApi.Domain.Models.Entities;
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
    #endregion

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

        base.OnModelCreating(builder);
    }
}
