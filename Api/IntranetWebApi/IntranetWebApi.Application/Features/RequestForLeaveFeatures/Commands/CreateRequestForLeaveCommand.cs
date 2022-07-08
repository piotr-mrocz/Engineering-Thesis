using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Entities;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.RequestForLeaveFeatures.Commands;

public class CreateRequestForLeaveCommand : IRequest<BaseResponse>
{
    public RequestForLeaveToAddDto RequestInfo { get; set; } = null!;
}

public class CreateRequestForLeaveHandler : IRequestHandler<CreateRequestForLeaveCommand, BaseResponse>
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

    public async Task<BaseResponse> Handle(CreateRequestForLeaveCommand request, CancellationToken cancellationToken)
    {
        var supervisorDepartment = await GetIdSupervisorDepartment(request.RequestInfo.IdUser, cancellationToken);

        if (!supervisorDepartment.Succeeded || supervisorDepartment.Data == null)
        {
            return new BaseResponse()
            {
                Message = supervisorDepartment.Message
            };
        }

        var newRequest = new RequestForLeave()
        {
            IdApplicant = request.RequestInfo.IdUser,
            CreateDate = DateTime.Now,
            ActionDate = DateTime.Now,
            IdSupervisor = supervisorDepartment.Data,
            AbsenceType = request.RequestInfo.AbsenceType,
            StartDate = request.RequestInfo.StartDate,
            EndDate = request.RequestInfo.EndDate,
            DaysAbsence = (int)(request.RequestInfo.EndDate.Date - request.RequestInfo.StartDate.Date).TotalDays
        };

        var response = await _requestForLeaveRepo.CreateEntity(newRequest, cancellationToken);

        return new BaseResponse()
        {
            Succeeded = response.Succeeded,
            Message = response.Succeeded ? response.Message : "Nie udało się przesłać wniosku!"
        };
    }

    private async Task<ResponseStruct<int>> GetIdSupervisorDepartment(int idUser, CancellationToken cancellationToken)
    {
        var user = await _userRepo.GetEntityByExpression(x => x.Id == idUser, cancellationToken);

        if (user == null || !user.Succeeded || user.Data == null)
        {
            return new ResponseStruct<int>()
            {
                Message = "Nie udało się odnaleźć użytkownika w bazie danych!"
            };
        }

        var idDepartment = user.Data.IdDepartment;

        var department = await _departmentRepo.GetEntityByExpression(x => x.Id == idDepartment, cancellationToken);

        if (department == null || !department.Succeeded || department.Data == null)
        {
            return new ResponseStruct<int>()
            {
                Message = "Nie udało się odnaleźć działu w bazie danych!"
            };
        }

        return new ResponseStruct<int>()
        {
            Succeeded = true,
            Data = department.Data.IdSupervisor
        };
    }
}
