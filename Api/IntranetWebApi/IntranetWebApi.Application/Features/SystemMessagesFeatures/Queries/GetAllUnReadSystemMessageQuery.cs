using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.SystemMessagesFeatures.Queries;

public class GetAllUnReadSystemMessageQuery : IRequest<Response<SystemMessageListDto>>
{
}

public class SystemMessageListDto
{
    public List<SystemMessage> SystemMessages { get; set; } = new();
}
