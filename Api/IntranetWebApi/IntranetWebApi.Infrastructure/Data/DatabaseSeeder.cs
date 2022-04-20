using IntranetWebApi.Data;
using IntranetWebApi.Domain.Enums;
using IntranetWebApi.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntranetWebApi.Infrastructure.Data;

public class DatabaseSeeder
{
    private readonly IntranetDbContext _dbContext;

    public DatabaseSeeder(IntranetDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Seed()
    {
        if (_dbContext.Database.CanConnect())
        {
            if (!_dbContext.Roles.Any())
            {
                var roles = GetRoles();
                _dbContext.Roles.AddRange(roles);
                _dbContext.SaveChanges();
            }
        }
    }

    private IEnumerable<Role> GetRoles()
    {
        var roles = new List<Role>()
            {
                new Role() { Name = RolesEnum.User.ToString() },
                new Role() { Name = RolesEnum.Manager.ToString() },
                new Role() { Name = RolesEnum.Admin.ToString() },
            };

        return roles;
    }
}
