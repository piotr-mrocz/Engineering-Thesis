using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Application.Helpers;
using IntranetWebApi.Domain.Enums;
using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.PresenceFeatures.Queries;

public class GetAllPossibleAbsenceTypeToChooseQuery : IRequest<Response<List<PossibleAbsenceToChooseDto>>>
{
}

public class GetAllPossibleAbsenceTypeToChooseHandler : IRequestHandler<GetAllPossibleAbsenceTypeToChooseQuery, Response<List<PossibleAbsenceToChooseDto>>>
{
    public async Task<Response<List<PossibleAbsenceToChooseDto>>> Handle(GetAllPossibleAbsenceTypeToChooseQuery request, CancellationToken cancellationToken)
    {
        var absenceList = new List<PossibleAbsenceToChooseDto>();

        var enumAbsence = (AbsenceReasonsEnum[])Enum.GetValues(typeof(AbsenceReasonsEnum));
        var enumAbsenceArray = enumAbsence.Where(x => 
                x != AbsenceReasonsEnum.FreeDay && 
                x != AbsenceReasonsEnum.Weekend &&
                x != AbsenceReasonsEnum.UnauthorizedAbsence);

        foreach (AbsenceReasonsEnum absence in enumAbsenceArray.OrderByDescending(x => x))
        {
            absenceList.Add(
                new PossibleAbsenceToChooseDto()
                {
                    Id = (int)absence,
                    AbsenceName = EnumHelper.GetEnumDescription((AbsenceReasonsEnum) absence)
                });
        }

        return new()
        {
            Succeeded = true,
            Data = absenceList
        };
    }
}
