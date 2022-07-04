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

namespace IntranetWebApi.Application.Features.TaskFeatures.Queries;

public class GetAllPriorityQuery : IRequest<Response<List<PriorityDto>>>
{
}

public class GetAllPriorityHandler : IRequestHandler<GetAllPriorityQuery, Response<List<PriorityDto>>>
{
    public async Task<Response<List<PriorityDto>>> Handle(GetAllPriorityQuery request, CancellationToken cancellationToken)
    {
        var priorityList = new List<PriorityDto>();

        foreach (PriorityEnum priority in (PriorityEnum[])Enum.GetValues(typeof(PriorityEnum)))
        {
            var priorityDto = new PriorityDto()
            {
                Id = (int)priority,
                Name = EnumHelper.GetEnumDescription(priority),
            };

            priorityList.Add(priorityDto);
        }

        return new Response<List<PriorityDto>>()
        {
            Succeeded = true,
            Data = priorityList
        };
    }
}


