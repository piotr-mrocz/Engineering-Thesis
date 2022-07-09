﻿using System;
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

public class AcceptRequestForLeaveCommand : IRequest<BaseResponse>
{
    public int IdRequest { get; set; }  
}

public class AcceptRequestForLeaveHandler : IRequestHandler<AcceptRequestForLeaveCommand, BaseResponse>
{
    private readonly IGenericRepository<RequestForLeave> _requestRepo;
    private readonly IGenericRepository<User> _userRepo;

    public AcceptRequestForLeaveHandler(IGenericRepository<RequestForLeave> requestRepo, IGenericRepository<User> userRepo)
    {
        _requestRepo = requestRepo;
        _userRepo = userRepo;
    }

    public async Task<BaseResponse> Handle(AcceptRequestForLeaveCommand request, CancellationToken cancellationToken)
    {
        var requestForLeave = await _requestRepo.GetEntityByExpression(x => x.Id == request.IdRequest, cancellationToken);

        if (requestForLeave == null || !requestForLeave.Succeeded || requestForLeave.Data == null)
        {
            return new BaseResponse()
            {
                Message = "Nie udało się odnaleźć wniosku w bazie danych!"
            };
        }

        var totalDaysVacation = DateTimeHelper.CalculateTotalDaysBetweenDatesWithoutWeekends(requestForLeave.Data.StartDate, requestForLeave.Data.EndDate);

        requestForLeave.Data.Status = (int)RequestStatusEnum.AcceptedBySupervisor;
        requestForLeave.Data.ActionDate = DateTime.Now;

        var response = await _requestRepo.UpdateEntity(requestForLeave.Data, cancellationToken);

        await UpdateUserVacationInfo(requestForLeave.Data.IdApplicant, totalDaysVacation, cancellationToken);

        return new BaseResponse()
        {
            Succeeded = response.Succeeded,
            Message = response.Succeeded ? response.Message : "Wystąpił błąd podczas akceptowania wniosku!"
        };
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

        var response = await _userRepo.UpdateEntity(user.Data, cancellationToken);

        return new BaseResponse()
        {
            Succeeded = response.Succeeded,
            Message = response.Succeeded ? response.Message : "Nie udało się wyzerować dni urlopowych w requestach"
        };
    }
}
