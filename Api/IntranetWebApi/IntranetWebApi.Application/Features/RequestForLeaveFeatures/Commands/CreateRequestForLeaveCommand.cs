using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Entities;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.RequestForLeaveFeatures.Commands;

public class CreateRequestForLeaveCommand : IRequest<ResponseStruct<int>>
{
    public int IdUser { get; set; }
    public DateTime CreateDate { get; set; }
    public int AbsenceType { get; set; }
}

public class CreateRequestForLeaveHandler : IRequestHandler<CreateRequestForLeaveCommand, ResponseStruct<int>>
{
    private readonly IGenericRepository<RequestForLeave> _requestForLeaveRepo;
    private readonly IGenericRepository<User> _userRepo;
    private readonly IGenericRepository<Department> _departmentRepo;

    public CreateRequestForLeaveHandler(IGenericRepository<RequestForLeave> requestForLeaveRepo,
        IGenericRepository<User> userRepo,
        IGenericRepository<Department> departmentRepo)
    {
        _requestForLeaveRepo = requestForLeaveRepo;
        _userRepo = userRepo;
        _departmentRepo = departmentRepo;
    }

    public Task<ResponseStruct<int>> Handle(CreateRequestForLeaveCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private async Task<int> GetIdDepartmentFromUser(int idUser, CancellationToken cancellationToken)
    {
        return 1;
    }

    private async Task<int> GetIdSupervisorDepartment(int idDepartment, CancellationToken cancellationToken)
    {
        return 1;
    }

    private async Task<int> AddNewRequestForLeave(CreateRequestForLeaveCommand request, 
        CancellationToken cancellationToken)
    {
        return 1;
    }

    // add migrations o database
}
