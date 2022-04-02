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
    #endregion

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
