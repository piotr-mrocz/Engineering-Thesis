using IntranetWebApi.Application.Exceptions;
using IntranetWebApi.Application.Helpers;
using IntranetWebApi.Data;
using IntranetWebApi.Domain.Enums;
using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Domain.Models.Settings;
using IntranetWebApi.Domain.Response;
using IntranetWebApi.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IntranetWebApi.Application.Services;
public class AccountService : IAccountService
{
    private readonly IntranetDbContext _dbContext;
    private readonly AuthenticationSettings _authSettings;

    public AccountService(IntranetDbContext dbContext, AuthenticationSettings authSettings)
    {
        _dbContext = dbContext;
        _authSettings = authSettings;
    }

    public async Task<AuthenticationResponse> GenerateToken(LoginDto dto, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Include(u => u.Role)
            .Include(x => x.Photo)
            .FirstOrDefaultAsync(x => x.Login == dto.Login);

        if (user is null)
            throw new ApiException("Niepoprawny login lub hasło");

        var veryfiedPassword = PasswordHelper.ValidatePassword(dto.Password, user.Password);

        if (!veryfiedPassword)
            throw new ApiException("Niepoprawny login lub hasło");

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
            new Claim(ClaimTypes.UserData, user.Photo.Name)
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.JwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(_authSettings.DurationInMinutes);

        var token = new JwtSecurityToken(
            _authSettings.JwtIssuer,
            _authSettings.JwtIssuer,
            claims,
            expires: expires,
            signingCredentials: cred);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenString = tokenHandler.WriteToken(token);
       
        return new AuthenticationResponse()
        {
            Role = EnumHelper.GetEnumDescription((RolesEnum)user.IdRole),
            Token = tokenString,
            UserName = user.FirstName,
            IsAuthorize = veryfiedPassword
        };
    }
}

