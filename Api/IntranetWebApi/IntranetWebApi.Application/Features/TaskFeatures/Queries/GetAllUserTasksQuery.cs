using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Application.Helpers;
using IntranetWebApi.Domain.Enums;
using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;
using Task = IntranetWebApi.Domain.Models.Entities.Task;

namespace IntranetWebApi.Application.Features.TaskFeatures;

public class GetAllUserTasksQuery : IRequest<Response<List<TaskDto>>>
{
    public int IdUser { get; set; }
    public int Status { get; set; }
}

public class GetAllUserTaskHandler : IRequestHandler<GetAllUserTasksQuery, Response<List<TaskDto>>>
{
    private readonly IGenericRepository<Task> _taskRepo;

    public GetAllUserTaskHandler(IGenericRepository<Task> taskRepo)
    {
        _taskRepo = taskRepo;
    }

    public async Task<Response<List<TaskDto>>> Handle(GetAllUserTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _taskRepo.GetManyEntitiesByExpression(x =>
            x.IdUser == request.IdUser &&
            x.Status == request.Status, cancellationToken);

        var succeeded = tasks.Succeeded && tasks.Data != null;

        if (!succeeded)
        {
            return new Response<List<TaskDto>>()
            {
                Message = "Wystąpił błąd podczas wyszukiwania zadań!",
                Data = new List<TaskDto>()
            };
        }

        var tasksDto = PrepareReturnList(tasks.Data, request.Status);

        return new Response<List<TaskDto>>()
        {
            Succeeded = succeeded,
            Data = tasksDto
        };
    }

    private List<TaskDto> PrepareReturnList(IEnumerable<Task> tasks, int status)
    {
        if (!tasks.Any())
            return new List<TaskDto>();

        tasks = GetOrderByTasks(tasks, status);

        var returnList = new List<TaskDto>();

        foreach (var task in tasks)
        {
            var taskDetails = new TaskDto()
            {
                Id = task.Id,
                IdUser = task.IdUser,
                WhoAdded = task.WhoAdd,
                Title = task.Title,
                Description = task.Description,
                Deadline = task.Deadline.HasValue ? task.Deadline.Value.ToString("dd MMMM yyyy HH:mm") : string.Empty,
                AddedDate = task.AddedDate.ToString("dd MMMM yyyy HH:mm"),
                ProgressDate = task.ProgressDate.HasValue ? task.ProgressDate.Value.ToString("dd MMMM yyyy HH:mm") : string.Empty,
                FinishDate = task.FinishDate.HasValue ? task.FinishDate.Value.ToString("dd MMMM yyyy HH:mm") : string.Empty,
                Status = task.Status,
                Priority = task.Priority,
                PriorityDescription = EnumHelper.GetEnumDescription((PriorityEnum)task.Priority)
            };

            returnList.Add(taskDetails);
        }

        return returnList;
    }

    private List<Task> GetOrderByTasks(IEnumerable<Task> tasks, int status)
    {
        switch (status)
        {
            case (int)TaskStatusEnum.Done:
                return tasks.OrderBy(x => x.FinishDate).ThenBy(x => x.AddedDate).ToList();

            case (int)TaskStatusEnum.InProgress:
                return tasks.OrderBy(x => x.Priority).ThenBy(x => x.ProgressDate).ToList();

            default:
                return tasks.OrderBy(x => x.Priority).ThenBy(x => x.AddedDate).ToList();
        }
    }
}
