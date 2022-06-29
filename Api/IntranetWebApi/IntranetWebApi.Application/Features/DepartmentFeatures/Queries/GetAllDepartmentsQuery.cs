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

namespace IntranetWebApi.Application.Features.RoleFeatures.Queries
{
    public class GetAllDepartmentsQuery : IRequest<Response<List<DepartmentDto>>>
    {
    }

    public class GetAllDepartmentsHandler : IRequestHandler<GetAllDepartmentsQuery, Response<List<DepartmentDto>>>
    {
        private readonly IGenericRepository<Department> _departmentRepo;

        public GetAllDepartmentsHandler(IGenericRepository<Department> departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }

        public async Task<Response<List<DepartmentDto>>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var departments = await _departmentRepo.GetManyEntitiesByExpression(x => true, cancellationToken);

            if (!departments.Succeeded || departments.Data is null || !departments.Data.Any())
            {
                return new Response<List<DepartmentDto>>()
                {
                    Message = departments.Message,
                    Data = new List<DepartmentDto>()
                };
            }

            var departmentsDtoList = new List<DepartmentDto>();

            foreach (var department in departments.Data)
            {
                var departmentDto = new DepartmentDto()
                {
                    Id = department.Id,
                    DepartmentName = department.DepartmentName
                };

                departmentsDtoList.Add(departmentDto);
            }

            return new Response<List<DepartmentDto>>()
            {
                Succeeded = true,
                Data = departmentsDtoList
            };
        }
    }
}
