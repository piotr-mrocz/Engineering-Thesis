using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Application.Helpers;
using IntranetWebApi.Domain.Enums;
using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Domain.Models.ViewModels;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;
using Microsoft.AspNetCore.SignalR;

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
    private readonly IGenericRepository<SystemMessage> _systemMessageRepo;
    private readonly IHubContext<SystemMessageHubClient, ISystemMessageHubClient> _systemMessagesHub;

    public CreateRequestForLeaveHandler(
        IGenericRepository<RequestForLeave> requestForLeaveRepo,
        IGenericRepository<User> userRepo,
        IGenericRepository<Department> departmentRepo,
        IGenericRepository<SystemMessage> systemMessageRepo,
        IHubContext<SystemMessageHubClient, ISystemMessageHubClient> systemMessagesHub)
    {
        _requestForLeaveRepo = requestForLeaveRepo;
        _userRepo = userRepo;
        _departmentRepo = departmentRepo;
        _systemMessageRepo = systemMessageRepo;
        _systemMessagesHub = systemMessagesHub;
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

        var totalDaysVacation = DateTimeHelper.CalculateTotalDaysBetweenDatesWithoutWeekends(request.RequestInfo.StartDate, request.RequestInfo.EndDate);

        var canAddRequest = await CheckIfUserHaveEnoughFreeDays(totalDaysVacation, supervisorDepartment.Data.User, cancellationToken);

        if (!canAddRequest)
        {
            return new BaseResponse()
            {
                Message = "Nie masz wystarczającej ilości dni urlopowych do wykorzystania w tym roku! Procedura wstrzymana"
            };
        }

        var newRequest = new RequestForLeave()
        {
            IdApplicant = request.RequestInfo.IdUser,
            CreateDate = DateTime.Now,
            ActionDate = DateTime.Now,
            IdSupervisor = supervisorDepartment.Data.IdSupervisor,
            AbsenceType = request.RequestInfo.AbsenceType,
            StartDate = request.RequestInfo.StartDate,
            EndDate = request.RequestInfo.EndDate,
            DaysAbsence = totalDaysVacation,
            RejectReason = string.Empty,
            Status = (int)RequestStatusEnum.ForConsideration
        };

        // when user is manager, his request is automatically accepted
        if (request.RequestInfo.IsManager)
        {
            newRequest.Status = (int)RequestStatusEnum.AcceptedBySupervisor;
            newRequest.ActionDate = DateTime.Now;
            newRequest.IdSupervisor = request.RequestInfo.IdUser;
        }

        var response = await _requestForLeaveRepo.CreateEntity(newRequest, cancellationToken);

        if (!response.Succeeded)
        {
            return new BaseResponse()
            {
                Message = "Nie udało się przesłać wniosku!"
            };
        }

        if (newRequest.IdApplicant != newRequest.IdSupervisor)
            await AddSystemMessage(newRequest.IdSupervisor, cancellationToken);

        return new BaseResponse()
        {
            Succeeded = response.Succeeded,
            Message = response.Message
        };
    }

    private async Task<Response<UsersDepartmentVM>> GetIdSupervisorDepartment(int idUser, CancellationToken cancellationToken)
    {
        var user = await _userRepo.GetEntityByExpression(x => x.Id == idUser, cancellationToken);

        if (user == null || !user.Succeeded || user.Data == null)
        {
            return new()
            {
                Message = "Nie udało się odnaleźć użytkownika w bazie danych!"
            };
        }

        var idDepartment = user.Data.IdDepartment;

        var department = await _departmentRepo.GetEntityByExpression(x => x.Id == idDepartment, cancellationToken);

        if (department == null || !department.Succeeded || department.Data == null)
        {
            return new()
            {
                Message = "Nie udało się odnaleźć działu w bazie danych!"
            };
        }

        var idSupervisor = department.Data.IdSupervisor;

        if (idUser == department.Data.IdSupervisor)
        {
            var supervisor = await _userRepo.GetEntityByExpression(x => x.IdRole == (int)RolesEnum.Manager, cancellationToken);

            if (supervisor == null || !supervisor.Succeeded || supervisor.Data == null)
            {
                return new()
                {
                    Message = "Nie udało się odnaleźć członka zarządu!"
                };
            }

            idSupervisor = supervisor.Data.Id;
        }

        return new()
        {
            Succeeded = true,
            Data = new UsersDepartmentVM()
            {
                IdSupervisor = idSupervisor,
                User = user.Data
            }
        };
    }

    private async Task<bool> CheckIfUserHaveEnoughFreeDays(int totalDaysVacation, User user, CancellationToken cancellationToken)
    {
        var isEnoughDays = (user.VacationDaysThisYear + user.VacationDaysLastYear - totalDaysVacation) >= 0;

        if (!isEnoughDays)
            return isEnoughDays;

        var isLastYearDaysEnough = user.VacationDaysLastYear - totalDaysVacation >= 0;

        if (isLastYearDaysEnough)
        {
            user.VacationDaysLastYear = user.VacationDaysLastYear - totalDaysVacation;
        }
        else
        {
            user.VacationDaysLastYear = 0;
            user.VacationDaysThisYear = user.VacationDaysThisYear + user.VacationDaysLastYear - totalDaysVacation;
        }

        user.VacationDaysInRequests = totalDaysVacation;

        await _userRepo.UpdateEntity(user, cancellationToken);

        return true;
    }

    private async System.Threading.Tasks.Task AddSystemMessage(int idUser, CancellationToken cancellationToken)
    {
        var systemMessage = new SystemMessage()
        {
            IdUser = idUser,
            Info = EnumHelper.GetEnumDescription(SystemMessageTypeEnum.AddNewRequestForLeave),
            AddedDate = DateTime.Now
        };

        var response = await _systemMessageRepo.CreateEntity(systemMessage, cancellationToken);

        if (response.Succeeded)
            await _systemMessagesHub.Clients.All.NewSystemMessage();
    }
}


