using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Domain.Enums;
using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;
using Task = IntranetWebApi.Domain.Models.Entities.Task;

namespace IntranetWebApi.Application.Features.TaskFeatures;

    public class GetAllUserTasksQuery : IRequest<Response<List<Task>>>
    {
    public int IdUser { get; set; }
    public int Status { get; set; }
}

public class GetAllUserTaskHandler : IRequestHandler<GetAllUserTasksQuery, Response<List<Task>>>
{
    private readonly IGenericRepository<IntranetWebApi.Domain.Models.Entities.Task> _taskRepo;

    public GetAllUserTaskHandler(IGenericRepository<IntranetWebApi.Domain.Models.Entities.Task> taskRepo)
    {
        _taskRepo = taskRepo;
    }

    public async Task<Response<List<Task>>> Handle(GetAllUserTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _taskRepo.GetManyEntitiesByExpression(x => 
            x.IdUser == request.IdUser &&
            x.Status == request.Status, cancellationToken);

        var succeeded = tasks.Succeeded && tasks.Data != null;

        return new Response<List<Task>>()
        {
            Succeeded = succeeded,
            Message = succeeded ? "Ok" : tasks.Message,
            Data = succeeded ? tasks.Data.ToList() : new List<Task>()
        };
    }
}
