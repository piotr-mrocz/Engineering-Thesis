using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Application.Helpers;
using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Enums;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.UserFeatures.Queries;

public class GetAllUserInDepartmentByIdSupervisorQuery : IRequest<Response<List<UserDto>>>
{
    public int IdSupervisor { get; set; }
}

public class GetAllUserInDepartmentByIdSupervisorHandler : IRequestHandler<GetAllUserInDepartmentByIdSupervisorQuery, Response<List<UserDto>>>
{
    private readonly IGenericRepository<User> _userRepo;

    public GetAllUserInDepartmentByIdSupervisorHandler(IGenericRepository<User> userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<Response<List<UserDto>>> Handle(GetAllUserInDepartmentByIdSupervisorQuery request, CancellationToken cancellationToken)
    {
        var department = await _userRepo.GetEntityByExpression(x => x.Id == request.IdSupervisor, cancellationToken);

        if (department is null || !department.Succeeded || department.Data is null)
        {
            return new()
            {
                Message = "Nie udało się odnaleźć działu!"
            };
        }

        var allDepartmentUsers = await _userRepo.GetManyEntitiesByExpression(x =>
                x.IdDepartment == department.Data.IdDepartment &&
                x.Id != request.IdSupervisor, cancellationToken);

        if (allDepartmentUsers is null || !allDepartmentUsers.Succeeded || allDepartmentUsers.Data is null || !allDepartmentUsers.Data.Any())
        {
            var departmentName = EnumHelper.GetEnumDescription((DepartmentsEnum)department.Data.IdDepartment);

            return new()
            {
                Message = $"Nie odnaleziono żadnych użytkowników pracujących w dziale: {departmentName}",
                Data = new()
            };
        }

        var usersListDto = new List<UserDto>();

        foreach (var user in allDepartmentUsers.Data)
        {
            var rekord = new UserDto()
            {
                IdUser = user.Id,
                UserName = $"{user.FirstName} {user.LastName}"
            };

            usersListDto.Add(rekord);
        }

        return new Response<List<UserDto>>()
        {
            Succeeded = true,
            Data = usersListDto
        };
    }
}


