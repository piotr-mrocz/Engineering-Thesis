using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Application.Helpers;
using IntranetWebApi.Domain.Enums;
using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Interfaces;
using IntranetWebApi.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IntranetWebApi.Application.Features.UserFeatures.Queries;

public class GetAllUsersQuery : IRequest<Response<List<UserDetailsDto>>>
{

}

public class GetUsersHandler : IRequestHandler<GetAllUsersQuery, Response<List<UserDetailsDto>>>
{
    private readonly IIntranetDbContext _dbContext;

    public GetUsersHandler(IIntranetDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response<List<UserDetailsDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _dbContext.Users
            .Include(u => u.Role)
            .Include(x => x.Photo)
            .Include(x => x.Department)
            .ToListAsync(cancellationToken);

        var usersList = new List<UserDetailsDto>();

        if (!users.Any())
        {
            return new()
            {
                Succeeded = true,
                Message = "Nie udało się odnaleźć żadnych pracowników",
                Data = usersList
            };
        }

        foreach (var user in users)
        {
            var rekord = new UserDetailsDto()
            {
                Id = user.Id,
                UserLastName = user.LastName,
                UserName = user.FirstName,
                Email = user.Email,
                PhoneNumber = user.Phone,
                PhotoName = user.Photo.Name,
                Department = user.Department.DepartmentName,
                IdRole = user.IdRole,
                Role = EnumHelper.GetEnumDescription((RolesEnum)user.IdRole)
            };

            usersList.Add(rekord);
        }

        return new()
        {
            Succeeded = true,
            Data = usersList
        };
    }
}
