using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Application.Helpers;
using IntranetWebApi.Domain.Enums;
using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Infrastructure.Interfaces;
using IntranetWebApi.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IntranetWebApi.Application.Features.UserFeatures.Queries;

public class GetAllUsersByIdDepartmentQuery : IRequest<Response<List<UserDetailsDto>>>
{
    public int IdDepartment { get; set; }
}

public class GetAllUsersByIdDepartmentHandler : IRequestHandler<GetAllUsersByIdDepartmentQuery, Response<List<UserDetailsDto>>>
{
    private readonly IIntranetDbContext _dbContext;

    public GetAllUsersByIdDepartmentHandler(IIntranetDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response<List<UserDetailsDto>>> Handle(GetAllUsersByIdDepartmentQuery request, CancellationToken cancellationToken)
    {
        var users = await _dbContext.Users
            .Include(x => x.Position)
            .Include(x => x.Photo)
            .Include(x => x.Department)
            .Where(x => x.IdDepartment == request.IdDepartment)
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
                Position = user.Position.Name
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
