using IntranetWebApi.Application.Helpers;
using IntranetWebApi.Domain.Enums;
using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.RequestForLeaveFeatures.Queries;

public class GetAllRequestsForLeaveToAcceptByManagerQuery : IRequest<Response<List<GetAllRequestsForLeaveToAcceptDto>>>
{
}

public class GetAllRequestsForLeaveToAcceptByManagerHandler : IRequestHandler<GetAllRequestsForLeaveToAcceptByManagerQuery, Response<List<GetAllRequestsForLeaveToAcceptDto>>>
{
    private readonly IGenericRepository<RequestForLeave> _requestRepo;
    private readonly IGenericRepository<User> _userRepo;

    public GetAllRequestsForLeaveToAcceptByManagerHandler(IGenericRepository<RequestForLeave> requestRepo, IGenericRepository<User> userRepo)
    {
        _requestRepo = requestRepo;
        _userRepo = userRepo;
    }

    public async Task<Response<List<GetAllRequestsForLeaveToAcceptDto>>> Handle(GetAllRequestsForLeaveToAcceptByManagerQuery request, CancellationToken cancellationToken)
    {
        var usersRequestForLeaveList = await GetAllRequest(cancellationToken);

        if (!usersRequestForLeaveList.Any())
        {
            return new Response<List<GetAllRequestsForLeaveToAcceptDto>>()
            {
                Message = "Nie ma żadnych wniosków urlopowych do rozpatrzenia",
                Data = new List<GetAllRequestsForLeaveToAcceptDto>()
            };
        }

        var response = await GetGetAllRequestsForLeaveToAcceptListDto(usersRequestForLeaveList, cancellationToken);

        return new Response<List<GetAllRequestsForLeaveToAcceptDto>>()
        {
            Succeeded = response.Any(),
            Message = response.Any()
                    ? string.Empty
                    : "Nie ma żadnych wniosków urlopowych do rozpatrzenia",
            Data = response.Any()
                 ? response
                 : new List<GetAllRequestsForLeaveToAcceptDto>()
        };
    }

    private async Task<List<RequestForLeave>> GetAllRequest(CancellationToken cancellationToken)
    {
        var requests = await _requestRepo.GetManyEntitiesByExpression(x =>


                x.Status == (int)RequestStatusEnum.ForConsideration,
                cancellationToken);

        return requests == null || !requests.Succeeded || requests.Data == null || !requests.Data.Any()
            ? new List<RequestForLeave>()
            : requests.Data.ToList();
    }

    private async Task<List<GetAllRequestsForLeaveToAcceptDto>> GetGetAllRequestsForLeaveToAcceptListDto(List<RequestForLeave> usersRequestList, CancellationToken cancellationToken)
    {
        if (!usersRequestList.Any())
            return new List<GetAllRequestsForLeaveToAcceptDto>();

        var requestListDto = new List<GetAllRequestsForLeaveToAcceptDto>();

        var idsUsers = usersRequestList.Select(x => x.IdApplicant).ToList();
        var users = await _userRepo.GetManyEntitiesByExpression(x => idsUsers.Contains(x.Id), cancellationToken);

        if (users == null || !users.Succeeded || users.Data == null || !users.Data.Any())
            return new List<GetAllRequestsForLeaveToAcceptDto>();

        foreach (var request in usersRequestList)
        {
            var user = users.Data.FirstOrDefault(x => x.Id == request.IdApplicant);

            var record = new GetAllRequestsForLeaveToAcceptDto()
            {
                IdRequest = request.Id,
                DisplayUserName = user != null ? $"{user.FirstName} {user.LastName}" : "Brak danych",
                IdApplicant = request.IdApplicant,
                EndDate = request.EndDate.ToString("dd.MM.yyyy"),
                StartDate = request.StartDate.ToString("dd.MM.yyyy"),
                AddedDate = request.CreateDate.ToString("dd.MM.yyyy  HH:mm"),
                AbsenceType = EnumHelper.GetEnumDescription((AbsenceReasonsEnum)request.AbsenceType),
                TotalDays = DateTimeHelper.CalculateTotalDaysBetweenDatesWithoutWeekends(request.StartDate, request.EndDate)
            };

            requestListDto.Add(record);
        }

        return requestListDto;
    }
}
