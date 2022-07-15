using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Application.Helpers;
using IntranetWebApi.Domain.Enums;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.RequestForLeaveFeatures.Commands;

public class RemoveRequestForLeaveCommand : IRequest<BaseResponse>
{
    public int IdRequest { get; set; }
}

public class RemoveRequestForLeaveHandler : IRequestHandler<RemoveRequestForLeaveCommand, BaseResponse>
{
    private readonly IGenericRepository<RequestForLeave> _requestRepo;
    private readonly IGenericRepository<User> _userRepo;
    private readonly IGenericRepository<SystemMessage> _systemMessageRepo;

    public RemoveRequestForLeaveHandler(
        IGenericRepository<RequestForLeave> requestRepo, 
        IGenericRepository<User> userRepo,
        IGenericRepository<SystemMessage> systemMessageRepo)
    {
        _requestRepo = requestRepo;
        _userRepo = userRepo;
        _systemMessageRepo = systemMessageRepo;
    }

    public async Task<BaseResponse> Handle(RemoveRequestForLeaveCommand request, CancellationToken cancellationToken)
    {
        var requestForLeave = await _requestRepo.GetEntityByExpression(x => x.Id == request.IdRequest, cancellationToken);

        if (requestForLeave == null || !requestForLeave.Succeeded || requestForLeave.Data == null)
        {
            return new BaseResponse()
            {
                Message = "Nie udało się odnaleźć wniosku w bazie danych!"
            };
        }

        requestForLeave.Data.Status = (int)RequestStatusEnum.RemovedByUser;
        requestForLeave.Data.ActionDate = DateTime.Now;

        var totalDaysVacation = DateTimeHelper.CalculateTotalDaysBetweenDatesWithoutWeekends(requestForLeave.Data.StartDate.Date, requestForLeave.Data.EndDate.Date);

        var response = await _requestRepo.UpdateEntity(requestForLeave.Data, cancellationToken);

        if (!response.Succeeded)
        {
            return new BaseResponse()
            {
                Message = "Nie udało się wycofać wniosku!"
            };
        }

        var updateUserVacationDays = await UpdateUserVacationInfo(requestForLeave.Data.IdApplicant, totalDaysVacation, cancellationToken);

        if (!updateUserVacationDays.Succeeded)
            return updateUserVacationDays;

        await AddSystemMessage(requestForLeave.Data.IdSupervisor, cancellationToken);

        return response;
    }

    private async Task<BaseResponse> UpdateUserVacationInfo(int idUser, int totalDaysVacationInThisRequest, CancellationToken cancellationToken)
    {
        var user = await _userRepo.GetEntityByExpression(x => x.Id == idUser, cancellationToken);

        if (user == null || !user.Succeeded || user.Data == null)
        {
            return new()
            {
                Message = "Nie udało się odnaleźć użytkownika w bazie danych!"
            };
        }

        user.Data.VacationDaysInRequests = user.Data.VacationDaysInRequests - totalDaysVacationInThisRequest;
        user.Data.VacationDaysThisYear = user.Data.VacationDaysThisYear + totalDaysVacationInThisRequest;

        var response = await _userRepo.UpdateEntity(user.Data, cancellationToken);

        return new BaseResponse()
        {
            Succeeded = response.Succeeded,
            Message = response.Succeeded ? response.Message : "Nie udało się wyzerować dni urlopowych w requestach"
        };
    }

    private async System.Threading.Tasks.Task AddSystemMessage(int idUser, CancellationToken cancellationToken)
    {
        var systemMessage = new SystemMessage()
        {
            IdUser = idUser,
            Info = EnumHelper.GetEnumDescription(SystemMessageTypeEnum.RemoveRequestForLeave)
        };

        await _systemMessageRepo.CreateEntity(systemMessage, cancellationToken);
    }
}
