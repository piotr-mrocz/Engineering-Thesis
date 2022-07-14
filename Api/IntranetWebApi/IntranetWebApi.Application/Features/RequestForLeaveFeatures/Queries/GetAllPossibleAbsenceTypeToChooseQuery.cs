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

namespace IntranetWebApi.Application.Features.RequestForLeaveFeatures.Queries;

public class GetAllPossibleAbsenceTypeToChooseQuery : IRequest<Response<List<PossibleAbsenceToChooseDto>>>
{
}

public class GetAllPossibleAbsenceTypeToChooseHandler : IRequestHandler<GetAllPossibleAbsenceTypeToChooseQuery, Response<List<PossibleAbsenceToChooseDto>>>
{
    public async Task<Response<List<PossibleAbsenceToChooseDto>>> Handle(GetAllPossibleAbsenceTypeToChooseQuery request, CancellationToken cancellationToken)
    {
        var absenceList = new List<PossibleAbsenceToChooseDto>()
        {
            new PossibleAbsenceToChooseDto()
            {
                Id = (int)AbsenceReasonsEnum.Holiday,
                AbsenceName = EnumHelper.GetEnumDescription(AbsenceReasonsEnum.Holiday)
            },

            new PossibleAbsenceToChooseDto()
            {
                Id = (int)AbsenceReasonsEnum.SpecialLeave,
                AbsenceName = EnumHelper.GetEnumDescription(AbsenceReasonsEnum.SpecialLeave)
            },

            new PossibleAbsenceToChooseDto()
            {
                Id = (int)AbsenceReasonsEnum.UnpaidLeave,
                AbsenceName = EnumHelper.GetEnumDescription(AbsenceReasonsEnum.UnpaidLeave)
            }
        };

        return new()
        {
            Succeeded = true,
            Data = absenceList
        };
    }
}
