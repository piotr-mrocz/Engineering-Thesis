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

namespace IntranetWebApi.Application.Features.RequestForLeaveFeatures.Queries;

public class GetInformationAboutUserVacationDaysQuery : IRequest<Response<UserVacationInfoDto>>
{
    public int IdUser { get; set; }
}

public class GetInformationAboutUserVacationDaysHandler : IRequestHandler<GetInformationAboutUserVacationDaysQuery, Response<UserVacationInfoDto>>
{
    private readonly IGenericRepository<User> _userRepo;

    public GetInformationAboutUserVacationDaysHandler(IGenericRepository<User> userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<Response<UserVacationInfoDto>> Handle(GetInformationAboutUserVacationDaysQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepo.GetEntityByExpression(x => x.Id == request.IdUser, cancellationToken);

        if (user == null || !user.Succeeded || user.Data == null)
        {
            return new()
            {
                Message = "Nie udało się odnaleźć użytkownika w bazie danych!",
                Data = new()
            };
        }

        var userDto = new UserVacationInfoDto()
        {
            VacationDaysInRequests = user.Data.VacationDaysInRequests,
            VacationDaysThisYear = user.Data.VacationDaysThisYear,
            VacationDaysLastYear = user.Data.VacationDaysLastYear,
            StartJobYear = user.Data.DateOfEmployment.Year
        };

        return new()
        {
            Succeeded = true,
            Data = userDto
        };
    }
}
