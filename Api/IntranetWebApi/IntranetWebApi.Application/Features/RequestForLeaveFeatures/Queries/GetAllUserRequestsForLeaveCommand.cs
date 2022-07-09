using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Application.Helpers;
using IntranetWebApi.Domain.Enums;
using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.RequestForLeaveFeatures.Queries;

public class GetAllUserRequestsForLeaveCommand : IRequest<Response<List<GetAllUserRequestsForLeaveDto>>>
{
    public int IdUser { get; set; }
    public int Year { get; set; }
}

public class GetAllUserRequestsForLeaveHandler : IRequestHandler<GetAllUserRequestsForLeaveCommand, Response<List<GetAllUserRequestsForLeaveDto>>>
{
    private readonly IGenericRepository<RequestForLeave> _requestRepo;

    public GetAllUserRequestsForLeaveHandler(IGenericRepository<RequestForLeave> requestRepo)
    {
        _requestRepo = requestRepo;
    }

    public async Task<Response<List<GetAllUserRequestsForLeaveDto>>> Handle(GetAllUserRequestsForLeaveCommand request, CancellationToken cancellationToken)
    {
        var requestForLeaveList = await _requestRepo.GetManyEntitiesByExpression(x =>
             x.IdApplicant == request.IdUser && x.StartDate.Year == request.Year, cancellationToken);

        if (requestForLeaveList == null || !requestForLeaveList.Succeeded || 
            requestForLeaveList.Data == null ||  !requestForLeaveList.Data.Any())
        {
            return new()
            {
                Message = "Nie udało się odnaleźć wniosku w bazie danych!",
                Data = new()
            };
        }

        var requestsDtoList = new List<GetAllUserRequestsForLeaveDto>();

        foreach (var requestForLeave in requestForLeaveList.Data.OrderByDescending(x => x.CreateDate))
        {
            var totalDaysVacation = (int)(requestForLeave.EndDate.Date - requestForLeave.StartDate.Date).TotalDays + 1;

            var record = new GetAllUserRequestsForLeaveDto()
            {
                IdRequest = requestForLeave.Id,
                CreatedDate = requestForLeave.CreateDate.ToString("dd MMMM yyyy  HH:mm"),
                StartDate = requestForLeave.StartDate.ToString("dd MMMM yyyy"),
                EndDate = requestForLeave.EndDate.ToString("dd MMMM yyyy"),
                TotalDays = totalDaysVacation,
                AbsenceType = EnumHelper.GetEnumDescription((AbsenceReasonsEnum)requestForLeave.AbsenceType),
                Status = requestForLeave.Status,
                StatusDescription = EnumHelper.GetEnumDescription((RequestStatusEnum)requestForLeave.Status)
            };

            requestsDtoList.Add(record);
        }

        return new Response<List<GetAllUserRequestsForLeaveDto>>()
        {
            Succeeded = true,
            Data = requestsDtoList
        };
    }
}
