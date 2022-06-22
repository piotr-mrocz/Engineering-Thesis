using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.UserFeatures.Queries;

public class GetUsersPositionsAndDepartmentsAndRolesQuery : IRequest<Response<GetUsersPositionsAndDepartmentsAndRolesDto>>
{
}

public class GetUsersPositionsAndDepartmentsHandler : IRequestHandler<GetUsersPositionsAndDepartmentsAndRolesQuery, Response<GetUsersPositionsAndDepartmentsAndRolesDto>>
{
    private readonly IGenericRepository<Department> _departmentRepo;
    private readonly IGenericRepository<Position> _positionRepo;
    private readonly IGenericRepository<Role> _roleRepo;

    public GetUsersPositionsAndDepartmentsHandler(IGenericRepository<Department> departmentRepo, 
        IGenericRepository<Position> positionRepo,
        IGenericRepository<Role> roleRepo)
    {
        _departmentRepo = departmentRepo;
        _positionRepo = positionRepo;
        _roleRepo = roleRepo;
    }

    public async Task<Response<GetUsersPositionsAndDepartmentsAndRolesDto>> Handle(GetUsersPositionsAndDepartmentsAndRolesQuery request, CancellationToken cancellationToken)
    {
        return new Response<GetUsersPositionsAndDepartmentsAndRolesDto>()
        {
            Succeeded = true,
            Data = new GetUsersPositionsAndDepartmentsAndRolesDto()
            {
                Departments = await GetDepartmentList(cancellationToken),
                Positions = await GetPositionsList(cancellationToken),
                Roles = await GetRolesList(cancellationToken)
            }
        };
    }

    private async Task<List<Department>> GetDepartmentList(CancellationToken cancellationToken)
    {
        var departments = await _departmentRepo.GetManyEntitiesByExpression(x => true, cancellationToken);

        return departments is not null && departments.Succeeded && departments.Data is not null && departments.Data.Any()
            ? departments.Data.ToList()
            : new List<Department>();
    }

    private async Task<List<Position>> GetPositionsList(CancellationToken cancellationToken)
    {
        var positions = await _positionRepo.GetManyEntitiesByExpression(x => true, cancellationToken);

        return positions is not null && positions.Succeeded && positions.Data is not null && positions.Data.Any()
            ? positions.Data.ToList()
            : new List<Position>();
    }
    
    private async Task<List<Role>> GetRolesList(CancellationToken cancellationToken)
    {
        var roles = await _roleRepo.GetManyEntitiesByExpression(x => true, cancellationToken);

        return roles is not null && roles.Succeeded && roles.Data is not null && roles.Data.Any()
            ? roles.Data.ToList()
            : new List<Role>();
    }
}

