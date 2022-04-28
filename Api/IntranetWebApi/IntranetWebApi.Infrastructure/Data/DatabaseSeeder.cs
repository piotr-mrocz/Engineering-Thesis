using IntranetWebApi.Data;
using IntranetWebApi.Domain.Enums;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Models.Entities;
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

            if (!_dbContext.Photos.Any())
            {
                var photos = GetPhotos();
                _dbContext.Photos.AddRange(photos);
                _dbContext.SaveChanges();
            }

            if (!_dbContext.Users.Any())
            {
                var users = GetUsers();
                _dbContext.Users.AddRange(users);
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

    private IEnumerable<User> GetUsers()
    {
        var users = new List<User>()
        {
            new User()
            {
                FirstName = "Adam",
                LastName = "Mickiewicz",
                DateOfEmployment = new DateTime(2022, 4, 20),
                IdPosition = 1,
                IdDepartment = 1, // change harcoded parts
                Login = "123",
                Password = SecurePassword("123"),
                IdRole = (int)RolesEnum.User,
                IdPhoto = 1
            }
        };

        return users;
    }

    private IEnumerable<Photo> GetPhotos()
    {
        var photos = new List<Photo>()
        {
            new Photo()
            {
                Description = "Zdjęcie testowego usera Adama Mickiewicza",
                Path = "Chwilowo jest tutaj testowy string"
            }
        };

        return photos;
    }

    public string SecurePassword(string password)
    {
        var saltPassword = BCrypt.Net.BCrypt.GenerateSalt();
        var passwordEnhanced = BCrypt.Net.BCrypt.HashPassword(password, saltPassword, true);

        return passwordEnhanced;
    }
}
