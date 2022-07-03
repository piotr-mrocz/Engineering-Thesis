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

namespace IntranetWebApi.Application.Features.TaskFeatures.Queries;

    public class GetAllUserTasksQuery : IRequest<Response<TasksListDto>>
    {
    public int IdUser { get; set; }
}

public class GetAllUserTaskHandler : IRequestHandler<GetAllUserTasksQuery, Response<TasksListDto>>
{
    private readonly IGenericRepository<IntranetWebApi.Domain.Models.Entities.Task> _taskRepo;

    public GetAllUserTaskHandler(IGenericRepository<IntranetWebApi.Domain.Models.Entities.Task> taskRepo)
    {
        _taskRepo = taskRepo;
    }

    public async Task<Response<TasksListDto>> Handle(GetAllUserTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _taskRepo.GetManyEntitiesByExpression(x => x.IdUser == request.IdUser, cancellationToken);

        if (!tasks.Succeeded)
        {
            return new()
            {
                Message = tasks.Message,
                Data = new TasksListDto()
            };
        }

        var tasksListDto = GetTasksListDto(tasks.Data);

        return new Response<TasksListDto>()
        {
            Succeeded = true,
            Data = tasksListDto
        };
    }

    private TasksListDto GetTasksListDto(IEnumerable<IntranetWebApi.Domain.Models.Entities.Task> tasks)
    {
        if (tasks == null || !tasks.Any())
            return new();

        var tasksToDo = new List<IntranetWebApi.Domain.Models.Entities.Task>();
        var tasksInProgress = new List<IntranetWebApi.Domain.Models.Entities.Task>();
        var tasksDone = new List<IntranetWebApi.Domain.Models.Entities.Task>();

        foreach (var task in tasks.OrderBy(x => x.AddedDate))
        {
            switch (task.Status)
            {
                case (int)TaskStatusEnum.ToDo:
                    tasksToDo.Add(task);
                    break;

                case (int)TaskStatusEnum.InProgress:
                    tasksInProgress.Add(task);
                    break;

                case (int)TaskStatusEnum.Done:
                    tasksDone.Add(task);
                    break;
            }
        }

        return new TasksListDto()
        {
            ToDoTasks = tasksToDo,
            InProgressTasks = tasksInProgress,
            DoneTasks = tasksDone
        };
    }
}
